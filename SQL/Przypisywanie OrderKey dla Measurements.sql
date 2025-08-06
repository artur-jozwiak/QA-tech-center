

  SELECT COUNT(*)
FROM [QA].[dbo].[Measurements]
WHERE OrderKey IS NULL;


UPDATE M
SET M.OrderKey = O.ShortenedKey
FROM [QA].[dbo].[Measurements] M
JOIN [QA].[dbo].[Orders] O ON M.OrderId = O.Id
WHERE M.OrderKey IS NULL;