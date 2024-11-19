namespace NextStop.Core.Tools;

public static class ClientTools {
    public static async Task<string> Get(this HttpClient client, string url) {
        using HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}