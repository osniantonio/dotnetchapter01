using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace myapp.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly Tracer _tracer;

    public TestController(ILogger<TestController> logger, Tracer tracer)
    {
        _logger = logger;
        _tracer = tracer;
    }

    [HttpGet]
    [Route("test1")]
    public IActionResult GetTest1()
    {
        _logger.LogInformation("LogInformation (test1): GET request received on TestController");
        _logger.LogWarning("LogWarning: GET request received on TestController");
        _logger.LogError("LogError: GET request received on TestController", new Exception("Teste de mensagem de erro."));
        _logger.LogCritical("LogCritical: GET request received on TestController", new Exception("Teste de mensagem de erro em critical."));

        using (var activity = new Activity("Get"))
        {
            activity.Start();
            activity.AddTag("controller", "TestController");
            System.Threading.Thread.Sleep(5000);

            activity.AddEvent(new ActivityEvent("End of Get method"));
            activity.Stop();
        }

        return Ok("Teste completo!");
    }

    [HttpGet]
    [Route("test2")]
    public IActionResult GetTest2()
    {
        _logger.LogInformation("LogInformation (test2): GET request received on TestController");

        using (var span = _tracer.StartActiveSpan("TestController/Get"))
        {
            span.SetAttribute("http.method", "GET");
            span.SetAttribute("http.host", "localhost");
            span.SetAttribute("http.target", "/test");
            span.SetAttribute("http.flavor", "1.1");
            span.SetAttribute("net.peer.name", "localhost");
            span.SetAttribute("net.peer.port", 5000);
            span.SetAttribute("http.status_code", 200);

            return Ok("Hello, World!");
        }
    }
}
