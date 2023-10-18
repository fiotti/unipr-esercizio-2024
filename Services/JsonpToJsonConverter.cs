using Esercizio.Abstractions.Services;

namespace Esercizio.Services;

class JsonpToJsonConverter : IJsonpToJsonConverter
{
    const int RightParenLookahead = 1024;

    public void Convert(Stream source, Stream target)
    {
        // Ignora tutti i byte fino a '(' e copia i seguenti fino all'ultima ')'.
        // Dopo l'ultima ')' sono consentito solo caratteri di spaziatura o ';'.
        //
        // Esempio:
        // source: callback({ "Hello": "(World)" });
        //                 ^                      ^
        //               start                   end
        // target: { "Hello": "(World)" }

        List<byte> bytes = new();

        Span<byte> buff = stackalloc byte[4096];
        int bytesRead;
        while ((bytesRead = source.Read(buff)) > 0)
        {
            for (int i = 0; i < bytesRead; i++)
                bytes.Add(buff[i]);
        }

        int leftParenIndex = bytes.IndexOf((byte)'(');
        int rightParenIndex = bytes.LastIndexOf((byte)')');

        if (leftParenIndex == -1 || rightParenIndex == -1)
            throw new FormatException("Lo stream non contiene dati in formato JSONP.");

        for (int i = leftParenIndex + 1; i < rightParenIndex; i++)
            target.WriteByte(bytes[i]);
    }
}
