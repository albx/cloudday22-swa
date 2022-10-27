using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SwaConfManager.Api.Extensions;
using SwaConfManager.Api.Services;
using SwaConfManager.Shared;
using System.Net;

namespace SwaConfManager.Api
{
    public class CreateTalkFunction
    {
        private readonly ILogger _logger;

        public CreateTalkFunction(ILoggerFactory loggerFactory, AgendaServices agenda)
        {
            _logger = loggerFactory.CreateLogger<CreateTalkFunction>();
            Agenda = agenda ?? throw new ArgumentNullException(nameof(agenda));
        }

        public AgendaServices Agenda { get; }

        [Function("CreateTalkFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "talk")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var user = req.GetClientPrincipal();
            if (user is null)
            {
                return req.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var model = await req.ParseBodyAsync<CreateTalkModel>();
            if (model is null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            Agenda.CreateTalk(model, user.UserId);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");

            return response;
        }
    }
}
