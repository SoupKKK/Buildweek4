# Spice Girls Shop

** Spice Girls Shop** è una semplice applicazione ASP.NET Core integrata per gestire pagamenti online.
L'applicazione permette di memorizzare nel database ogni utente registrato (users table) e il rispettivo carello (carello table).
L admin è gestito nella classe globale Admin e può essere cambiato oppure si possono aggiungere più admin.
Per sbloccare le funzioni admin creare l'utente "peppino impastato" e fare il login.

## Requisiti

- [.NET Core SDK](https://dotnet.microsoft.com/download) installato


## Configurazione
Per configurare SQL server aprire la cartella DBSql e copiare le linea di comando SQL per generare le tabelle e popolarle, inoltre aggiornare la <connectionstring> in web.confing
