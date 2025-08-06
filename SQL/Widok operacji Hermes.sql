USE [firma_mc]
GO

/****** Object:  View [dbo].[QA_Operacje]    Script Date: 11.12.2023 13:16:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Alter VIEW [dbo].[QA_Operacje] AS
SELECT 
	operacje.Nazwa_Op,
	operacje.symbol_op,
	technolog.ID as Id_technolog
FROM OPERACJE operacje
join TECH_OPER tech_oper ON tech_oper.ID_OPER = operacje.Id
join TECHNOLOG technolog ON technolog.Id = tech_oper.ID_TECH
where operacje.status = 1
AND nazwa_op NOT LIKE 'Wydanie%'
AND nazwa_op NOT LIKE 'Przyjêcie%'
AND nazwa_op NOT LIKE 'Wysy³ka%'
AND nazwa_op NOT LIKE 'Znakowanie%'
AND nazwa_op NOT LIKE 'Weryfikacja%'
AND nazwa_op NOT LIKE 'Dostarczenie%'
AND nazwa_op NOT LIKE 'Lutowanie%'
AND nazwa_op NOT LIKE 'Monta¿%'
AND nazwa_op NOT LIKE 'Klejenie%'
AND nazwa_op NOT LIKE 'Wywa¿anie%'
AND nazwa_op NOT LIKE 'Mycie%'
AND nazwa_op NOT LIKE 'Klejenie%'
AND nazwa_op NOT LIKE 'Otwieranie%'
AND nazwa_op NOT LIKE 'Niklowanie'
AND nazwa_op NOT LIKE 'Toczenie%'
AND nazwa_op NOT LIKE 'Frezowanie%'
AND nazwa_op NOT LIKE 'Erodowanie%'
AND nazwa_op NOT LIKE 'Hartowanie%'
AND nazwa_op NOT LIKE 'Ciêcie%'
AND nazwa_op NOT LIKE 'Skracanie%'
AND nazwa_op NOT LIKE 'Wykonanie%'
AND nazwa_op NOT LIKE 'Przygotowanie%'
AND nazwa_op NOT LIKE 'Polerowanie%'

--AND nazwa_op NOT LIKE 'Przyjêcie pó³wyrobu na magazyn%'
--AND nazwa_op NOT LIKE 'Wydanie wyrobu z magazynu%'
AND nazwa_op NOT LIKE 'Pakowanie%'
AND nazwa_op NOT LIKE 'Odolejanie'
AND nazwa_op NOT LIKE 'Mycie zasadnicze'
AND nazwa_op NOT LIKE 'Roz³adunek z tac grafitowych'
AND nazwa_op NOT LIKE 'Gratowanie%'
AND nazwa_op NOT LIKE 'Polerowanie p³ytek'
AND nazwa_op NOT LIKE 'Wy¿arzanie korpusów pi³'
--AND nazwa_op NOT LIKE 'Gratowanie/czyszczenie p³ytek dwie strony';
GO


