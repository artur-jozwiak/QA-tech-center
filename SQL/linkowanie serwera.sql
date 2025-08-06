--EXEC sp_addlinkedserver 
--    @server = N'DEVELOP_SERVER', 
--    @srvproduct = N'SQL Server', 
--    @provider = N'SQLNCLI', 
--    @datasrc = N'BIURO-5A\DEVELOP';

---- Zak�adamy autoryzacj� Windows. Je�li potrzebujesz logowania SQL Server:
---- EXEC sp_addlinkedsrvlogin 
----     @rmtsrvname = N'DEVELOP_SERVER', 
----     @useself = N'False', 
----     @locallogin = NULL, 
----     @rmtuser = N'uzytkownik', 
----     @rmtpassword = N'haslo';

---- Je�li u�ywasz Windows Authentication:
--EXEC sp_addlinkedsrvlogin 
--    @rmtsrvname = N'DEVELOP_SERVER', 
--    @useself = N'True', 
--    @locallogin = NULL, 
--    @rmtuser = NULL, 
--    @rmtpassword = NULL;


-- KROK 1: Dodaj linked server (bez @provider!)
EXEC sp_addlinkedserver 
    @server = N'DEVELOP_SERVER', 
    @srvproduct = N'', 
	@provider = N'SQLNCLI', 
    @datasrc = N'BIURO-5A\DEVELOP';

-- KROK 2: Autoryzacja � zak�adam Windows Authentication
EXEC sp_addlinkedsrvlogin 
    @rmtsrvname = N'DEVELOP_SERVER', 
    @useself = N'True', 
    @locallogin = NULL;

