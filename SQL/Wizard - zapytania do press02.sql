--refOrders
SELECT distinct SUBSTRING(f36,CHARINDEX('/',f36)+1,5) as s,f2 FROM press02

--step1

SELECT distinct rtrim(f4) FROM press02 where f3 like '" & Product & "' 
select top(7) f2,s,dd from (SELECT distinct f2,SUBSTRING(f36,CHARINDEX('/',f36)+1,5) as s, '' as dd FROM press02 where f3 like '" & Product & "' and f2 <> ' ') as aaa order by replace(f2,'ta','') desc
select distinct case  left(f36,charindex('/',f36)-1) when 'k15eco' then 'XK15' else left(f36,charindex('/',f36)-1) end from press02 where f3 like '" & Item & "' and f36 <>'' )
SELECT distinct f2 FROM press02 where f3 like '" & Product & "' 

--moreData
SELECT distinct f2 FROM press02 where f3 like '" & Product & "' 