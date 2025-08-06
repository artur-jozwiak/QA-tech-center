/****** Script for SelectTopNRows command from SSMS  ******/
SELECT Distinct Nazwa_op
from QA_Technologie_Wzorcowe
join QA_Operacje operacje on operacje.Id_technolog = id
