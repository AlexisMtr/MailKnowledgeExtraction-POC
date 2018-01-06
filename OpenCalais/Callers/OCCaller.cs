using OpenCalais.Converters;
using OpenCalais.Parsers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenCalais.Callers
{
    public class OCCaller : IDisposable
    {
        private readonly HttpClient client;
        private readonly WebClient webClient;

        public OCCaller(HttpClient client, string apiKey, string language)
        {
            this.client = client;
            this.client.DefaultRequestHeaders.Clear();
            this.client.DefaultRequestHeaders.TryAddWithoutValidation("x-ag-access-token", apiKey);
            this.client.DefaultRequestHeaders.TryAddWithoutValidation("outputFormat", "application/json");
            this.client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "text/raw");
            this.client.DefaultRequestHeaders.TryAddWithoutValidation("omitOutputtingOriginalText", "true");
            if(!string.IsNullOrEmpty(language))
                this.client.DefaultRequestHeaders.TryAddWithoutValidation("x-calais-language", language);
        }

        public OCCaller(WebClient client, string apiKey, string language)
        {
            this.webClient = client;
            this.webClient.Headers.Clear();
            this.webClient.Headers.Add($"x-ag-access-token:{apiKey}");
            this.webClient.Headers.Add($"outputFormat:application/json");
            this.webClient.Headers.Add($"Content-Type:text/raw");
            this.webClient.Headers.Add($"omitOutputtingOriginalText:true");
            if (!string.IsNullOrEmpty(language))
                this.webClient.Headers.Add($"x-calais-language:{language}");
        }

        public async Task<TResult> TranformFromResult<TResult>(string source) where TResult : ITransform<TResult>
        {
            TResult item = Activator.CreateInstance<TResult>();

            Dictionary<string, Objects.OpenCalaisObject> result = null;
            if(this.client != null)
            {
                var response = await this.client.PostAsync($"https://api.thomsonreuters.com/permid/calais", new StringContent(source));
                result = OCParser.Parse(await response.Content.ReadAsStringAsync());
            }
            else if(this.webClient != null)
            {
                var response = await this.webClient.UploadStringTaskAsync($"https://api.thomsonreuters.com/permid/calais", source);
                result = OCParser.Parse(response);
            }
            
            return item.Transform(result);
        }

        public void Dispose()
        {
            if(this.client != null)
                this.client.Dispose();
            if (this.webClient != null)
                this.webClient.Dispose();
        }
    }
}
