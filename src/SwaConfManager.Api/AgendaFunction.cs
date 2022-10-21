using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SwaConfManager.Api.Services;
using System.Net;

namespace SwaConfManager.Api
{
    public class AgendaFunction
    {
        private readonly ILogger _logger;

        public AgendaFunction(ILoggerFactory loggerFactory, AgendaServices agenda)
        {
            _logger = loggerFactory.CreateLogger<AgendaFunction>();
            Agenda = agenda ?? throw new ArgumentNullException(nameof(agenda));
        }

        public AgendaServices Agenda { get; }

        [Function("AgendaFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "agenda")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var model = Agenda.GetAllTalks();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(model);

            return response;
        }
    }
}
