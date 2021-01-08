using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace TestProject1
{
    public static class HtmlExtensions
    {
        public static async Task<IDocument> GetHtmlAsync(this HttpClient client, string urlPath )
        {
            var response = await client.GetStringAsync(urlPath);
            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(response));
            return document;
        }
    }
}