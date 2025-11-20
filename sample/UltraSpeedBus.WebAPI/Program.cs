using UltraSpeedBus.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFooService();
builder.Services.AddSingleton<IFeatureManager, FeatureManager>();


var spo = builder.Services.BuildServiceProvider();
var featureManager = spo.GetRequiredService<IFeatureManager>();

if (featureManager.IsEnabled("teste"))
{
    builder.Services.AddFooService();
}


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/foo", (IFoo foo) =>
{
    return foo.Does();
})
.WithName("GetFoo")
.WithOpenApi();

app.Run();
