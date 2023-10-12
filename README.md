Esercizio C#
============

Scrivere un'applicazione a console in C# che reperisca dal sito dell'ISTAT
la lista dei nomi di bambini registrati all'ufficio anagrafe nell'anno 2021.

Il programma deve stampare i nomi ordinati in ordine alfabetico, stampando per
ciascun nome il genere ed il numero di bambini registrati con quel nome.

Usare `async` ed `await` dove necessario, usare Linq dove necessario.


## Chiamata web

Scrivere un programma da linea di comando in C# che reperisca dal sito
dell'ISTAT la lista dei nomi di bambini registrati all'ufficio anagrafe
nell'anno 2021.

https://www.istat.it/ws/nati/index2021.php?type=list&limit=137&year=2021

Salvare il risultato su un file _nascite2021.js_.

Se il file è già presente in quanto è già stato scaricato e salvato in
precedenza, il programma non deve scaricarlo nuovamente ad ogni esecuzione.

> Nota: per comodità, una copia di questo file è già presente in questo
> repository. Può essere usato per fare test nel caso in cui il sito dell'ISTAT
> sia temporaneamente non raggiungibile o se il formato dei dati è cambiato.

Il file è in formato [JSONP](https://wikipedia.org/wiki/JSONP) equivalente al
seguente (esclusi ritorni a capo e spaziature):
```js
callback({
  "years": [2021],
  "0": [
    { "year": "2021", "name": "LEONARDO", "count": "8448", "gender": "m", "percent": "4.1237113402062" },
    { "year": "2021", "name": "ALESSANDRO", "count": "4975", "gender": "m", "percent": "2.4284403311465" },
    { "year": "2021", "name": "TOMMASO", "count": "4973", "gender": "m", "percent": "2.427464073727" }
  ],
  "1": [
    { "year": "2021", "name": "SOFIA", "count": "5578", "gender": "f", "percent": "2.8642583891756" },
    { "year": "2021", "name": "AURORA", "count": "4991", "gender": "f", "percent": "2.5628385837891" },
    { "year": "2021", "name": "GIULIA", "count": "4616", "gender": "f", "percent": "2.3702790829033" }
  ]
});
```


## Formato JSON

Convertire la risposta in un file JSON e salvare il risultato su un file
_nascite2021.json_.

Se il file è già presente in quanto è già stato processato e salvato in
precedenza, il programma non deve generarlo nuovamente ad ogni esecuzione.

Per convertirlo in JSON è sufficiente rimuovere la parte iniziale fino alla
prima `(`, e la parte finale dall'ultima `)` in poi.

Il file finale dovrebbe avere un formato simile al seguente:
```json
{
  "years": [2021],
  "0": [
    { "year": "2021", "name": "LEONARDO", "count": "8448", "gender": "m", "percent": "4.1237113402062" },
    { "year": "2021", "name": "ALESSANDRO", "count": "4975", "gender": "m", "percent": "2.4284403311465" },
    { "year": "2021", "name": "TOMMASO", "count": "4973", "gender": "m", "percent": "2.427464073727" }
  ],
  "1": [
    { "year": "2021", "name": "SOFIA", "count": "5578", "gender": "f", "percent": "2.8642583891756" },
    { "year": "2021", "name": "AURORA", "count": "4991", "gender": "f", "percent": "2.5628385837891" },
    { "year": "2021", "name": "GIULIA", "count": "4616", "gender": "f", "percent": "2.3702790829033" }
  ]
}
```

> Questa operazione può essere fatta sia leggendo il file come stringa ed
> utilizzando le funzioni di manipolazione delle stringhe, che direttamente
> come `Stream` lavorando sui byte e supponendo che il file sia in ASCII.


## Output

Utilizzare `System.Text.Json` per leggere il file e stampare i nomi ordinati
in ordine alfabetico, stampando per ciascun nome il genere ed il numero di
bambini registrati con quel nome.

```plaintext
ALESSANDRO  m  4975
AURORA  f  4991
GIULIA  f  4616
LEONARDO  m  8448
SOFIA  f  5578
TOMMASO  m  4973
```


## Note legali

Le statistiche sui nomi sono estratte dal sito dell'ISTAT e sono soggette a licenza
[Creative Commons – Attribuzione – versione 3.0](https://www.istat.it/it/note-legali).
