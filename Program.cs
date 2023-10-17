using System.Text.Json;
using Esercizio.Abstractions.Services;
using Esercizio.Data;
using Esercizio.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.TryAddSingleton<IHttpDownloader, HttpDownloader>();
builder.Services.TryAddSingleton<IJsonpToJsonConverter, JsonpToJsonConverter>();

using IHost host = builder.Build();

using (AsyncServiceScope scope = host.Services.CreateAsyncScope())
{
    IHostApplicationLifetime lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
    
    Executor executor = ActivatorUtilities.CreateInstance<Executor>(scope.ServiceProvider);
    await executor.PrintSortedNamesAsync(lifetime.ApplicationStopping);
}



class Executor
{
    const string RawUri = "https://www.istat.it/ws/nati/index2021.php?type=list&limit=137&year=2021";
    const string RawFile = "nascite2021.js";
    const string JsonFile = "nascite2021.json";

    readonly IHttpDownloader _downloader;
    readonly IJsonpToJsonConverter _converter;

    public Executor(
        IHttpDownloader downloader,
        IJsonpToJsonConverter converter)
    {
        _downloader = downloader;
        _converter = converter;
    }

    public async Task PrintSortedNamesAsync(CancellationToken cancellationToken = default)
    {
        // 1. Se non lo ha ancora fatto, scarica le statistiche dal sito dell'ISTAT.
        if (!File.Exists(RawFile))
        {
            using FileStream file = File.OpenWrite(RawFile);
            await _downloader.DownloadAsync(RawUri, file, cancellationToken);
        }

        // 2. Converte in formato JSON la risposta.
        if (!File.Exists(JsonFile))
        {
            using FileStream source = File.OpenRead(RawFile);
            using FileStream target = File.OpenWrite(JsonFile);
            _converter.Convert(source, target);
        }

        // 3. Deserializza il JSON di risposta.
        BirthStats stats;
        using (FileStream json = File.OpenRead(JsonFile))
        {
            stats = JsonSerializer.Deserialize<BirthStats>(json) ?? throw new InvalidOperationException();
        }

        IEnumerable<GenderBirthStats> statsOrdered = stats.Male
            .Concat(stats.Female)
            .OrderByDescending(s => s.Count);

        foreach (GenderBirthStats s in statsOrdered)
        {
            Console.WriteLine($"{s.Name}\t{s.Gender}\t{s.Count}");
        }
    }
}
