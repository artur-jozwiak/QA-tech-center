select kp.Name, kp.LSL, kp.LowerTollerance, kp.Nominal, kp.UpperTollerance, kp.USL, kp.FileName, kp.Unit,  km.Value, km.FileModificationDate, km.OrderNo, km.ParameterId 
from KeyenceMeasurements km join KeyenceParameters kp On  km.ParameterId = kp.Id
where km.OrderNo like '%"&OrderNo&"%' and km.Value NOT LIKE 255.000