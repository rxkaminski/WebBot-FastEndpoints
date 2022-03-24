using FastEndpoints;
using WebBotFastEndpoints.Requests;
using WebBotFastEndpoints.Responses;
using WebBotCore.WebConnection;
using WebBotCore.WebSite.FilmWeb.Search.Details;

namespace WebBotFastEndpoints.Endpoints
{
    public class SearchFilmsEndpoint : Endpoint<SearchRequest, SearchRowsResponse>
    {
        public IHttpClientFactory httpClientFactory { get; set; }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("api/filmweb/search/films/{title}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(SearchRequest req, CancellationToken ct)
        {
            var httpClient = httpClientFactory.CreateClient("WebBotHttpClient");
            var webResponse = WebResponseFactory.Create(httpClient);

            var filmSearch = new FilmsSearch(req.Title, webResponse);
            await filmSearch.DownloadAsync();

            var searchRows = filmSearch.SearchRows.Select(r => new SearchRowResponse()
            {
                Title = r.Title,
                Duration = r.Duration,
                Link = r.Link,
                Release =r.Release,
                Year = r.Year,
            }).ToArray();

            var response = new SearchRowsResponse()
            {
                SearchRows = searchRows,
            };

            await SendAsync(response, cancellation: ct);
        }
    }
}
