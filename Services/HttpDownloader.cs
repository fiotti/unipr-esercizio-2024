using Esercizio.Abstractions.Services;

namespace Esercizio.Services;

class HttpDownloader : IHttpDownloader
{
    public async Task DownloadAsync(string requestUri, Stream target, CancellationToken cancellationToken = default)
    {
        using HttpClient client = new();

        HttpResponseMessage response = await client.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        await response.Content.CopyToAsync(target);
    }
}
