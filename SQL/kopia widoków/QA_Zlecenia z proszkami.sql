USE [firma_mc]
GO

/****** Object:  View [dbo].[QA_Zlecenia]    Script Date: 02.10.2024 08:40:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[QA_Zlecenia] AS
SELECT  Distinct
nagl_zp.Id,
	nagl_zp.KLUCZ_ZP,
	nazwy_skrocone.nr_zp_skr as klucz_skrocony,
	poz_zp.SYMBOL_WYR,
	artykuly.NAZWA_ART,
	artykuly.QASYMB_WND,
	poz_zp.ILOSC,
	poz_zp.Id_tech,
	poz_dok.SYMBOL_ART SYMBOL_PROSZKU,
	poz_dok.NR_PARTII NR_PARTII_PROSZKU
FROM NAGL_ZP nagl_zp
join Q_nr_skr nazwy_skrocone on nagl_zp.KLUCZ_ZP = nazwy_skrocone.KLUCZ_ZP
join POZ_ZP  poz_zp on poz_zp.ID_NAGL = nagl_zp.ID
JOIN ARTYKULY artykuly ON artykuly.SYMBOL_ART = poz_zp.SYMBOL_WYR
LEFT JOIN POZ_DOK poz_dok ON poz_dok.ID_ZPR = nagl_zp.ID AND poz_dok.SYMBOL_MAG = 'M00013'

WHERE (nagl_zp.KLUCZ_ZP LIKE 'ZPR/2P%' OR nagl_zp.KLUCZ_ZP LIKE 'ZPR/2W%' OR nagl_zp.KLUCZ_ZP LIKE 'ZPR/2I%' OR nagl_zp.KLUCZ_ZP LIKE 'ZPR/2T%')
GO


