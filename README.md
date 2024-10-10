Esercizio C#
============

Scrivere un'applicazione a console in C# che reperisca dal sito dell'ISTAT
la lista dei nomi di bambini registrati all'ufficio anagrafe nell'anno 2022.

Il programma deve stampare i nomi ordinati in ordine alfabetico, stampando per
ciascun nome il genere ed il numero di bambini registrati con quel nome.

Usare `async` ed `await` dove necessario, usare Linq dove necessario.


## Chiamata web

Scrivere un programma da linea di comando in C# che reperisca dal sito
dell'ISTAT la lista dei nomi di bambini registrati all'ufficio anagrafe
nell'anno 2022.

https://www.istat.it/wp-content/themes/EGPbs5-child/contanomi/nati/index2022.php?type=list&limit=200&year=2022

Salvare il risultato su un file _nascite2022.js_.

Se il file è già presente in quanto è già stato scaricato e salvato in
precedenza, il programma non deve scaricarlo nuovamente ad ogni esecuzione.

> Nota: per comodità, una copia di questo file è già presente in questo
> repository. Può essere usato per fare test nel caso in cui il sito dell'ISTAT
> sia temporaneamente non raggiungibile o se il formato dei dati è cambiato.

Il file è in formato [JSONP](https://wikipedia.org/wiki/JSONP) equivalente al
seguente (esclusi ritorni a capo e spaziature):
```js
callback({
  "years": [2022],
  "0": [
    { "year": 2022, "name": "LEONARDO", "count": 7888, "gender": "m", "percent": 3.8960782376766 },
    { "year": 2022, "name": "FRANCESCO", "count": 4823, "gender": "m", "percent": 2.3821989528796 },
    { "year": 2022, "name": "TOMMASO", "count": 4795, "gender": "m", "percent": 2.3683690605552 }
  ],
  "1": [
    { "year": 2022, "name": "SOFIA", "count": 5465, "gender": "f", "percent": 2.8746212758795 },
    { "year": 2022, "name": "AURORA", "count": 4900, "gender": "f", "percent": 2.5774280424171 },
    { "year": 2022, "name": "GIULIA", "count": 4198, "gender": "f", "percent": 2.2081720249116 }
  ]
});
```


## Formato JSON

Convertire la risposta in un file JSON e salvare il risultato su un file
_nascite2022.json_.

Se il file è già presente in quanto è già stato processato e salvato in
precedenza, il programma non deve generarlo nuovamente ad ogni esecuzione.

Per convertirlo in JSON è sufficiente rimuovere la parte iniziale fino alla
prima `(`, e la parte finale dall'ultima `)` in poi.

Il file finale dovrebbe avere un formato simile al seguente:
```json
{
  "years": [2022],
  "0": [
    { "year": 2022, "name": "LEONARDO", "count": 7888, "gender": "m", "percent": 3.8960782376766 },
    { "year": 2022, "name": "FRANCESCO", "count": 4823, "gender": "m", "percent": 2.3821989528796 },
    { "year": 2022, "name": "TOMMASO", "count": 4795, "gender": "m", "percent": 2.3683690605552 }
  ],
  "1": [
    { "year": 2022, "name": "SOFIA", "count": 5465, "gender": "f", "percent": 2.8746212758795 },
    { "year": 2022, "name": "AURORA", "count": 4900, "gender": "f", "percent": 2.5774280424171 },
    { "year": 2022, "name": "GIULIA", "count": 4198, "gender": "f", "percent": 2.2081720249116 }
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
AURORA  f  4900
FRANCESCO  m  4823
GIULIA  f  4198
LEONARDO  m  7888
SOFIA  f  5465
TOMMASO  m  4795
```


## Note legali

Le statistiche sui nomi sono estratte dal sito dell'ISTAT e sono soggette a
[Licenza CC-by Creative Commons 4.0](https://www.istat.it/note-legali/).
