using OpenCalais.Converters;
using OpenCalais.Parsers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenCalais.Callers
{
    public class OCCaller : IDisposable
    {
        private readonly HttpClient client;

        public OCCaller(HttpClient client, string apiKey, string language)
        {
            this.client = client;
            client.DefaultRequestHeaders.Clear();
            this.client.DefaultRequestHeaders.TryAddWithoutValidation("x-ag-access-token", apiKey);
            this.client.DefaultRequestHeaders.TryAddWithoutValidation("outputFormat", "application/json");
            this.client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "text/raw");
            this.client.DefaultRequestHeaders.TryAddWithoutValidation("omitOutputtingOriginalText", "true");
            this.client.DefaultRequestHeaders.TryAddWithoutValidation("x-calais-language", language);
        }

        public async Task<TResult> TranformFromResult<TResult>(string source) where TResult : ITransform<TResult>
        {
            TResult item = Activator.CreateInstance<TResult>();

            var response = await this.client.PostAsync($"https://api.thomsonreuters.com/permid/calais", new StringContent(source));
            var result = OCParser.Parse(await response.Content.ReadAsStringAsync());
            
            return item.Transform(result);
        }

        public void Dispose()
        {
            if(this.client != null)
                this.client.Dispose();
        }
    }
}
