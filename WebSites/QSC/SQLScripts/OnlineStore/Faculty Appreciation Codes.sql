use GA;
GO

SELECT CAST(c.ContractID as varchar) + CAST(dbo.CalcMod10Checksum(c.ContractID) as varchar) as [Faculty Appreciation Code], 
ca.Name1 AS FM
FROM [core].[Contract] as c 
JOIN core.ContractAddress as ca on c.ContractID = ca.ContractID
LEFT JOIN Store.ContractOnlineBlock cob on cob.ContractID = c.ContractID 
Where c.ContractTypeID = 4 
AND ca.IsSalesPerson = 1 
AND c.SAPContractNo IS NOT NULL 
AND c.[DivisionCode] = 40 --40: QSP Canada
AND C.ProgramEndDate >= 20140101
AND cob.ContractID IS NULL
ORDER BY c.ContractID DESC
