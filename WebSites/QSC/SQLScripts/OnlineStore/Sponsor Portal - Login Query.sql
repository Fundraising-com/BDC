use GA;
GO
select @@servername
GO

SELECT TOP 50 CAST(c.ContractID as varchar) + CAST(dbo.CalcMod10Checksum(c.ContractID) as varchar) as OnlineID, 
ca.SAPAcctNo AS [Password], 
ca.Name1 AS [Group Name],
c.[DivisionCode] 
FROM [core].[Contract] as c 
JOIN core.ContractAddress as ca on c.ContractID = ca.ContractID 
Where c.ContractTypeID = 3 
AND ca.IsSoldTo = 1 
AND c.SAPContractNo IS NOT NULL 
AND c.[DivisionCode] = 40 --40: QSP Canada
--and (ca.name1 like '%Percy%' )
--and c.ContractID = 530533
ORDER BY c.ContractID DESC
