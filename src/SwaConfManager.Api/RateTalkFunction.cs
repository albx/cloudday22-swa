using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SwaConfManager.Api.Services;
using SwaConfManager.Shared;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SwaConfManager.Api
{
    public class RateTalkFunction
    {
        private readonly ILogger _logger;

        public RateTalkFunction(ILoggerFactory loggerFactory, AgendaServices agenda)
        {
            _logger = loggerFactory.CreateLogger<RateTalkFunction>();
            Agenda = agenda ?? throw new ArgumentNullException(nameof(agenda));
        }

        public AgendaServices Agenda { get; }

        [Function("RateTalkFunction")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "talk/{id}/rate")] HttpRequestData req,
            [Required] Guid id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (id == Guid.Empty)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var model = await req.ParseBodyAsync<TalkRateModel>();
                if (model is null)
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest);
                }

                Agenda.RateTalk(id, model);

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");

                return response;
            }
            catch (InvalidOperationException ex)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (ArgumentOutOfRangeException)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
