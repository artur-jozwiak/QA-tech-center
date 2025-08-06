/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) 
przych.NR_PARTII
,[NAZWA_ART]
,art.[SYMBOL_ART]
,art.ID
,art.NAZWA_ART
  FROM [firma_mc].[dbo].[ARTYKULY] art
  join PRZYCH przych ON przych.SYMBOL_ART = art.SYMBOL_ART
  where NAZWA_ART like'%rtp%'

  -- wydanie surowca z PR_WYD_SUR
  -- poz_dok  jest numer partii