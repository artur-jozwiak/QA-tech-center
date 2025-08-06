select n.KLUCZ_ZP, m.ILOSC, dodkol.ME_NRPRPO
from NAGL_ZP n
left join POZ_ZP pz on n.id = pz.ID_NAGL
left join MELDUNEK m on pz.id = m.ID_POZ_ZL 
left join OPERACJE o on o.id = m.ID_OPER
INNER JOIN dbo.DODKOL_MLD dodkol ON M.ID = dodkol.ID_MELD
where o.nazwa_op = 'Spiekanie' and dodkol.ME_NRPRPO != 0
order by dodkol.ME_NRPRPO desc