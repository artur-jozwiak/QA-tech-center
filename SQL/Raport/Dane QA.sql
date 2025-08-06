Select m.ParameterId, m.Date, m.Value, o.OrderKey, pr.Description, pr.PdmNo, p.Name, p.Unit, p.LowerLimit, p.NominalValue, p.UpperLimit
from Measurements m
join Orders o ON o.Id = m.OrderId
join Parameters p ON p.Id = m.ParameterId
join Products pr ON pr.Id = o.ProductId
where o.ShortenedKey like  '%"&OrderNo&"%'
--where o.ShortenedKey like  '%23001477%'