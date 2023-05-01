using Microsoft.AspNetCore.Mvc;

namespace myapp.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        Console.WriteLine("Testando a aplicação - método Get em TestController...");
        _logger.LogInformation("LogInformation: GET request received on TestController");
        _logger.LogWarning("LogWarning: GET request received on TestController");
        _logger.LogError("LogError: GET request received on TestController", new Exception("Teste de mensagem de erro."));
        _logger.LogCritical("LogCritical: GET request received on TestController", new Exception("Teste de mensagem de erro em critical."));
        return Ok("Hello, World!");
    }
}
