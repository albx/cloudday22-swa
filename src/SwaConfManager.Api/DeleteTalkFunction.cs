using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SwaConfManager.Api.Extensions;
using SwaConfManager.Api.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SwaConfManager.Api
{
    public class DeleteTalkFunction
    {
        private readonly ILogger _logger;

        public DeleteTalkFunction(ILoggerFactory loggerFactory, AgendaServices agenda)
        {
            _logger = loggerFactory.CreateLogger<DeleteTalkFunction>();
            Agenda = agenda ?? throw new ArgumentNullException(nameof(agenda));
        }

        public AgendaServices Agenda { get; }

        [Function("DeleteTalkFunction")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "talk/{id}")] HttpRequestData req,
            [Required] Guid id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (req.GetClientPrincipal() is null)
            {
                return req.CreateResponse(HttpStatusCode.Unauthorized);
            }

            if (id == Guid.Empty)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                Agenda.DeleteTalk(id);

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");

                return response;
            }
            catch (ArgumentOutOfRangeException)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
