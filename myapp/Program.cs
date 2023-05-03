using Serilog;
using OpenTelemetry.Trace;
using OpenTelemetry.Exporter;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
ElasticsearchSinkOptions options = new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
{
    IndexFormat = $"{Environment.GetEnvironmentVariable("ELASTIC_CONFIGURATION_INDEX_NAME")}-{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower()}-{DateTime.UtcNow:yyyy-MM-dd}",
    AutoRegisterTemplate = true,
    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog,
    MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information,
    DetectElasticsearchVersion = true
};

Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(options)
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            .CreateLogger();
builder.Host.UseSerilog();

// Configuração do OpenTelemetry para o Console Exporter
builder.Services.AddOpenTelemetry()
        .WithTracing(builder => builder
        .AddAspNetCoreInstrumentation()
        .AddJaegerExporter()
        .AddConsoleExporter());

// Configuração do OpenTelemetry com HttpClient
builder.Services.AddOpenTelemetry()
    .WithTracing(builder => builder
        .AddJaegerExporter(o =>
        {
            o.Protocol = JaegerExportProtocol.HttpBinaryThrift;
            o.HttpClientFactory = () =>
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-MyCustomHeader", "value");
                return client;
            };
        }));

// Configuração do OpenTelemetry com IHttpClientFactory        
builder.Services.AddHttpClient("JaegerExporter", configureClient: (client) => client.DefaultRequestHeaders.Add("X-MyCustomHeader", "value"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
