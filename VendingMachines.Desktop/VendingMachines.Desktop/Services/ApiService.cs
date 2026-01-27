using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace VendingMachines.Desktop.Services
{
    internal class ApiService
    {
        private static readonly HttpClient _http = new HttpClient();

        // TODO: поставь адрес своего API
        public static string BaseUrl = "https://localhost:7050";

        // JWT после логина
        public static string Token;

        public static T Get<T>(string path)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl + path);

            if (!string.IsNullOrWhiteSpace(Token))
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            // БЕЗ async: блокируем поток
            var res = _http.SendAsync(req).Result;
            res.EnsureSuccessStatusCode();

            var json = res.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public static T PostJson<T>(string path, object body)
        {
            var jsonBody = JsonSerializer.Serialize(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var req = new HttpRequestMessage(HttpMethod.Post, BaseUrl + path);
            if (!string.IsNullOrWhiteSpace(Token))
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            req.Content = content;

            var res = _http.SendAsync(req).Result;
            res.EnsureSuccessStatusCode();

            var json = res.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
