using System.Collections.Concurrent;
using System.Linq.Expressions;
using UltraSpeedBus.Abstractions.Message;
using UltraSpeedBus.Abstractions.Serializer;
using UltraSpeedBus.Abstractions.Transport;

namespace UltraSpeedBus.Transports.InMemory;

public class InMemoryTransport : ITransportProducer, ITransportConsumer
{
    private readonly IMessageSerializer _serializer;
    private readonly ConcurrentQueue<(MessageEnvelope envelope, TaskCompletionSource<bool> tcs)> _queue = new();
    private CancellationTokenSource? _cts;
    private Task? _consumerLoop;

    public InMemoryTransport(IMessageSerializer serializer)
    {
        _serializer = serializer;
    }

    public ValueTask DisposeAsync()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        return ValueTask.CompletedTask;
    }

    public Task PublishAsync(MessageEnvelope envelope, CancellationToken cancellationToken = default)
    {
        // For in-memory, Send & Publish same semantics
        return SendAsync(envelope, cancellationToken);
    }

    public async Task SendAsync(MessageEnvelope envelope, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        _queue.Enqueue((envelope, tcs));
        await tcs.Task.WaitAsync(cancellationToken);
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        return Task.CompletedTask;
    }

    public Task StartAsync(Func<ConsumerTransportContext, Task> onMessage, CancellationToken cancellationToken = default)
    {
        if (_cts == null) StartAsync(cancellationToken);

        var cts = _cts ?? throw new InvalidOperationException("CancellationTokenSource is not initialized.");

        _consumerLoop = Task.Run(async () =>
        {
            try { 
                while (!_cts!.Token.IsCancellationRequested)
                {
                    while (_queue.TryDequeue(out var item))
                    {
                        var envelope = item.envelope;
                        var tcs = item.tcs;

                        // Simulate delivery count = 1
                        var ctx = new ConsumerTransportContext(
                            envelope,
                            deliveryCount: 1,
                            completeAsync: async () => { tcs.SetResult(true); await Task.CompletedTask; },
                            abandonAsync: async () => { tcs.SetResult(false); await Task.CompletedTask; },
                            deadLetterAsync: async (r, d) => { tcs.SetResult(false); await Task.CompletedTask; },
                            cancellationToken: _cts.Token);

                        try
                        {
                            await onMessage(ctx);
                        }
                        catch
                        {
                            // If handler throws and didn't complete, mark abandoned
                            if (!tcs.Task.IsCompleted) tcs.SetResult(false);
                        }
                    }

                    await Task.Delay(10, _cts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                // Expected on cancellation
            }
        }, _cts.Token);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        _cts?.Cancel();
        return _consumerLoop ?? Task.CompletedTask;
    }
}
