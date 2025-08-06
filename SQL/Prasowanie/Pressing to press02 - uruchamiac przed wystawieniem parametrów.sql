-- Odpalaæ procedur¹ 
-- kiedy
INSERT INTO [metal].[dbo].[press02] (
    [f1],
    [f2],
    [f3], -- Dodano kolumnê f3
    [f4],
    [f7],
    [f8],
    [f9],
    [f10],
    [f11],
    [f12],
    [f13],
    [f14],
    [f15],
    [f16],
    [f17],
    [f18],
    [f19],
    [f20],
    [f21],
    [f22],
    [f23],
    [f24],
    [f25],
    [f26],
    [f27],
    [f28],
    [f30],
    [f33],
    [f34],
    [f35],
    [f36]
)
SELECT 
    FORMAT(p.[RowDateTime], 'dd.MM.yyyy HH:mm:ss') AS [f1],
    p.[OrderKey] AS [f2],
    pr.[Description] AS [f3], -- Pobranie opisu produktu
    p.[PDMNo] AS [f4],
    p.[Height1] AS [f7],
    p.[Height2] AS [f8],
    p.[Height3] AS [f9],
    p.[Height4] AS [f10],
    p.[Weight] AS [f11],
    p.[Force] AS [f12],
    p.[UCSB] AS [f13],
    p.[UPS] AS [f14],
    p.[PrecompactingA] AS [f15],
    p.[PrecompactingB] AS [f16],
    p.[PressStrokeRelation] AS [f17],
    p.[Decopression1A] AS [f18],
    p.[Decopression1B] AS [f19],
    p.[DecopressionV1] AS [f20],
    p.[Decopression2A] AS [f21],
    p.[Decopression2B] AS [f22],
    p.[DecopressionV2] AS [f23],
    p.[UnderfillStrokeB] AS [f24],
    CASE WHEN p.[SuctionFill] = 1 THEN '1' ELSE '0' END AS [f25],
    CASE WHEN p.[CounturFilling] = 1 THEN '1' ELSE '0' END AS [f26],
    p.[TrayQty] AS [f27],
    p.[BaloonNo] AS [f28],
    p.[RobotProgam] AS [f30],
    p.[BurringPrassuereCloseValve] AS [f33],
    p.[BurringPrassuereOpenValve] AS [f34],
    p.[Comment] AS [f35],
    p.[Powder] AS [f36]
FROM [QA].[dbo].[Pressing] p
JOIN [QA].[dbo].[Orders] o ON p.OrderId = o.Id
JOIN [QA].[dbo].[Products] pr ON o.[ProductId] = pr.[Id]
--Where p.RowDatetime  > '25.01.2025' --nie usuwaæ dat poprzednich pobrañ
--Where p.RowDatetime  > '06.02.2025' --wpisywaæ date najm³odszego wpisu z press02
--Where p.RowDatetime  > '20.02.2025' --wpisywaæ date najm³odszego wpisu z press02
--Where p.RowDatetime  > '20.03.2025' --wpisywaæ date najm³odszego wpisu z press02
--Where p.RowDatetime  > '02.04.2025' --wpisywaæ date najm³odszego wpisu z press02
--Where p.RowDatetime  > '11.04.2025' --wpisywaæ date najm³odszego wpisu z press02
--Where p.RowDatetime  > '25.04.2025' --wpisywaæ date najm³odszego wpisu z press02
--Where p.RowDatetime  > '26.05.2025' --wpisywaæ date najm³odszego wpisu z press02
Where p.RowDatetime  > '16.06.2025 ' --wpisywaæ date najm³odszego wpisu z press02


