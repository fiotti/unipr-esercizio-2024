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

        BufferedStream bufferedSource = new(source);
        BufferedStream bufferedTarget = new(target);

        // Salta tutti i caratteri fino alla prima '('.
        while (bufferedSource.ReadByte() is not ('(' or -1))
        {
            // Skip.
        }

        // Copia tutti i caratteri diversi da ')'.
        int b;
        while ((b = bufferedSource.ReadByte()) is not (')' or -1))
        {
            bufferedTarget.WriteByte((byte)b);
        }

        Span<byte> buff = stackalloc byte[RightParenLookahead];
        while (true)
        {
            // Ha trovato una ')', legge fino a `RightParenLookahead` caratteri,
            // verificando che siano tutti caratteri di spaziatura o ';'.
            int buffIndex = 0;

            while (buffIndex < buff.Length && (b = bufferedSource.ReadByte()) != -1)
            {
                buff[buffIndex++] = (byte)b;

                if (b != ';' && !char.IsWhiteSpace((char)b))
                    break;
            }

            // Se ha raggiunto la fine dello stream, fine.
            if (b == -1)
                break;

            // Se ha trovato almeno un carattere non di spaziatura o ';',
            // travasa tutto ciò che ha nel buffer e procede.
            if (buffIndex < RightParenLookahead)
            {
                bufferedTarget.WriteByte((byte)')');
                bufferedTarget.Write(buff[..buffIndex]);
            }

            // Se non ha trovato altri caratteri, fine.
            else
            {
                break;
            }
        }

        bufferedTarget.Flush();
    }
}
