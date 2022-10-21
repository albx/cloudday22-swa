using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SwaConfManager.Api.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SwaConfManager.Api
{
    public class TalkDetailFunction
    {
        private readonly ILogger _logger;

        public TalkDetailFunction(ILoggerFactory loggerFactory, AgendaServices agenda)
        {
            _logger = loggerFactory.CreateLogger<TalkDetailFunction>();
            Agenda = agenda ?? throw new ArgumentNullException(nameof(agenda));
        }

        public AgendaServices Agenda { get; }

        [Function("TalkDetailFunction")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "talk/{id}")] HttpRequestData req,
            [Required] Guid id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (id == Guid.Empty)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var model = Agenda.GetTalkDetails(id);
            if (model is null)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(model);

            return response;
        }
    }
}
