USE [firma_mc]
GO

/****** Object:  View [dbo].[QA_Technologie_Wzorcowe]    Script Date: 11.12.2023 13:42:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[QA_Technologie_Wzorcowe] AS
SELECT  Id, symbol_tec, nazwa , symbol_wyr
from TECHNOLOG technologia
  where (SYMBOL_TEC = 'WFG' Or SYMBOL_TEC = 'WBL' Or SYMBOL_TEC like 'M94-0%')
    --where (SYMBOL_TEC = 'WFG' Or SYMBOL_TEC = 'WBL' Or SYMBOL_TEC like 'M94-0%' Or SYMBOL_TEC like  'M34-0%' Or SYMBOL_TEC like  'M44-0%')
  And Status = 11
GO


