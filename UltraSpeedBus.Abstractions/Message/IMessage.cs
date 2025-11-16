namespace UltraSpeedBus.Abstractions.Message;

// Message base interface
public interface IMessage
{
    Guid MessageId { get; }
    DateTime Timestamp { get; }

}

// Pipelines and middlewares
public interface IMessageMiddleware
{
}

public interface IPublishPipeline
{
}

public interface IConsumerPipeline
{
}

// Policies and erros
public interface IErrorHandler
{
}

public interface IRetryPolicy
{
}

public interface IExceptionFilter
{
    
}

// Telemetry - it will be integrated with OpenTelemetry
public interface ITracerAdapter
{
    
}