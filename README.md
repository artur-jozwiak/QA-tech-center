Ustawienia
Ustawienia portu, źródeł oraz połączenia z bazami danych  znajdują się w pliku appsetings.Production.json
- Baza danych aplikacji:
   "QAConnection": "Server=srv3\\mssql;Database=QA;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False",
    Logowanie: sekcja Serilog {... "connectionString":j.w. }

- Baza danych ERP:
 "HermesConnection": "Server=srv5\\mssql;Database=firma_p10;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False"

- Ścieżka zapisu/odczytu rysunków:
  "ImagesPath": "P:\\dokumenty-metal\\QA",

- Ścieżka odczytu danych Keyence:  
  "KeyencePath": "P:\\wspolne\\keyence\\Raporty CSV\\q-das" 

- Port
   "Url": "http://0.0.0.0:5555"

