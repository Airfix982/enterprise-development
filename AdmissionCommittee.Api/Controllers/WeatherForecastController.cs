using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {// Каждый контроллер содержит ссылочку на репозиторий через DI
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        // Репозитории () ин мемори, Дб,реализовать методы Адд, РЕмув, Апдейт, Гет, модифицировать тесты
        // Контроллеры
       // ActionResult<T>  // GET когда помимо тела еще и код надо вернуть    
       // DEPENDENCY INJECION основные вопросы будут
        //почитай DWH
        // Медиатор - паттерн программирования, большая библиотека



    }
}
