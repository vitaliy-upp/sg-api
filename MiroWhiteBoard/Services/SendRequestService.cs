using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MiroWhiteBoard.Services
{
    public class SendRequestService
    {
        public SendRequestService()
        { }

        public async Task<string> SendPostRequest(string url, string access_token, string data)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");

                var responseMessage = await httpClient.PostAsync(url, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var stringResult = await responseMessage.Content.ReadAsStringAsync();
                    return stringResult;
                }
                return string.Empty;
            }
        }

        public async Task<string> SendGetRequest(string url, string access_token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var responseMessage = await httpClient.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var stringResult = await responseMessage.Content.ReadAsStringAsync();
                    return stringResult;
                }
                return string.Empty;
            }
        }

        public async Task SendDeleteRequest(string url, string access_token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var responseMessage = await httpClient.DeleteAsync(url);
            }
        }
    }
}
