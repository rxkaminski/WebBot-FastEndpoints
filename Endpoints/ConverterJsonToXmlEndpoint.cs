using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using WebBotCore.Translate;
using WebBotCore.WebConnection;
using WebBotCore.WebSite.Converters;
using WebBotFastEndpoints.Requests;

namespace WebBotFastEndpoints.Endpoints
{
    public class ConverterJsonToXmlEndpoint : Endpoint<ConverterJsonToXmlRequest, ContentResult>
    {
        public IHttpClientFactory httpClientFactory { get; set; }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("api/converter/jsontoxml/{jsonEndPoint}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ConverterJsonToXmlRequest req, CancellationToken ct)
        {
            var httpClient = httpClientFactory.CreateClient("WebBotHttpClient");
            var webResponse = WebResponseFactory.Create(httpClient, new JsonToXmlTranslateResponse());

            var jsonToXmlConvert = new JsonToXmlConvert(req.JsonEndPoint, webResponse);
            await jsonToXmlConvert.DownloadAsync();

            if (jsonToXmlConvert.Xml == null)
            {
                await SendErrorsAsync(404);
                return;
            }

            var contentResult = new ContentResult()
            {
                Content = jsonToXmlConvert.Xml.OuterXml,
                ContentType = "text/xml"
            };

            await SendAsync(contentResult, cancellation: ct);
        }
    }
}
