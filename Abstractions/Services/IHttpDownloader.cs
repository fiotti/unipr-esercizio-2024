namespace Esercizio.Abstractions.Services;

/// <summary>
/// Permette di fare richieste HTTP.
/// </summary>
public interface IHttpDownloader
{
    /// <summary>
    /// Fa una richiesta HTTP GET all'URI indicato e scrive il risultato nello stream passato.
    /// </summary>
    /// <param name="requestUri">URI da chiamare.</param>
    /// <param name="target">Stream dove scrivere il risultato.</param>
    /// <param name="cancellationToken">Token per notificare la volontà di cancellare la richiesta.</param>
    /// <returns>Task concluso al termine del download.</returns>
    /// <exception cref="UriFormatException">L'URI di richiesta non è valido.</exception>
    /// <exception cref="HttpRequestException">La richiesta HTTP non ha avuto esito positivo.</exception>
    /// <exception cref="OperationCanceledException">La richiesta è stata cancellata.</exception>
    Task DownloadAsync(string requestUri, Stream target, CancellationToken cancellationToken = default);
}
