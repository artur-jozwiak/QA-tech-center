
  SELECT * 
FROM [QA].[dbo].[KeyenceMeasurements]
WHERE OrderNo like '%TA%' and OrderNo COLLATE Latin1_General_CS_AS <> UPPER(OrderNo)



UPDATE [QA].[dbo].[KeyenceMeasurements]
SET OrderNo = UPPER(OrderNo)
WHERE OrderNo like '%TA%' and OrderNo COLLATE Latin1_General_CS_AS <> UPPER(OrderNo)