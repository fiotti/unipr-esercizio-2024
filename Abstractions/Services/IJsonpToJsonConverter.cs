namespace Esercizio.Abstractions.Services;

/// <summary>
/// Permette di fare conversioni da JSONP a JSON.
/// </summary>
public interface IJsonpToJsonConverter
{
    /// <summary>
    /// Converte da uno stream JSONP ad uno stream JSON.
    /// </summary>
    /// <param name="source">Stream di origine, in JSONP.</param>
    /// <param name="target">Stream di destinazione, in JSON.</param>
    void Convert(Stream source, Stream target);
}
