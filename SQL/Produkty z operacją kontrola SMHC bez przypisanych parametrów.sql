
  SELECT TOP (1000) o.Id
      ,o.Name
      ,pr.Symbol
	  ,pr.PdmNo
      ,[ProductId]
  FROM [QA].[dbo].[Operations] o
  join Products pr ON pr.Id = o.ProductId

  LEFT JOIN Parameters p ON p.OperationId = o.Id
  WHERE o.Name LIKE '%SMS/HC%'
    AND p.OperationId IS NULL;
