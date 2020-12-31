USE [GA]
GO

SELECT	 onlineContract.DivisionCode, CAST(onlineContract.ContractID as varchar) + CAST(dbo.CalcMod10Checksum(onlineContract.ContractID) as varchar) as OnlineID, it.ItemTypeDesc, i.ItemDescShort,*
FROM Core.Contract onlineContract
JOIN Core.ContractAddress onlineAddress ON (onlineContract.ContractID = onlineAddress.ContractID) AND (onlineAddress.IsSoldTo = 1)
JOIN Core.ContractAddress landedAddress ON (onlineAddress.SAPAcctNo = landedAddress.SAPAcctNo) AND (landedAddress.IsSoldTo = 1)
JOIN Core.Contract landedContract ON (landedAddress.ContractID = landedContract.ContractID) AND (landedContract.ContractTypeID NOT IN (3)) AND (landedContract.DivisionCode <> 40 OR landedContract.SAPContractNo = onlineContract.SAPContractNo)
JOIN core.ContractBrochure onlinecb ON (onlinecb.ContractID = onlineContract.ContractID)
JOIN core.Brochure onlineb ON (onlineb.BrochureID = onlinecb.BrochureID)
JOIN core.BrochureOffer onlinebo ON (onlinebo.BrochureID = onlineb.BrochureID)
JOIN core.Item i ON (i.ItemID = onlinebo.ItemID)
JOIN core.BrochureOffer landedbo ON (landedbo.ItemID = i.ItemID)
JOIN core.Brochure landedb ON (landedb.BrochureID = landedbo.BrochureID)
JOIN core.ContractBrochure landedcb ON (landedcb.BrochureID = landedb.BrochureID) AND (landedcb.ContractID = landedContract.ContractID)
LEFT JOIN sales.ProgramAgreement pa ON pa.[Key] = landedContract.SAPContractNo
LEFT JOIN sales.ProductServicesSelection pss ON pss.ProgramAgreementID = pa.ID
LEFT JOIN	core.Tote t ON t.ContractID = landedContract.ContractID AND t.LastWorkflowStepTypeID IN (6, 26, 27, 28)-- AND t.WorkflowID = 16
LEFT JOIN	GA_Aplus..tblorderform tof ON tof.FocusToteID = t.ToteID
JOIN core.ItemType it ON it.ItemTypeID = i.ItemTypeID
WHERE onlineContract.ContractTypeID = 3 --Online
AND landedContract.ContractStateID IN (2)
AND (landedContract.ShippingLevel IS NULL OR landedContract.ShippingLevel NOT IN ('L1', 'L4'))
AND (pss.AplusServiceTypeKey IS NULL OR pss.AplusServiceTypeKey NOT IN ('WO'))
AND	(landedContract.DivisionCode = 40 OR tof.OrderFormID IS NULL)
AND	it.IsEligibleForShippingToAccount = 1
AND	onlineContract.IsShippingToAccountAllowed = 1
AND t.ToteID IS NULL
AND GETDATE() BETWEEN CONVERT(DATE, CONVERT(VARCHAR(8), landedb.EffectiveBegin), 112) AND CONVERT(DATE, CONVERT(VARCHAR(8), landedb.EffectiveEnd), 112)
AND GETDATE() BETWEEN CONVERT(DATE, CONVERT(VARCHAR(8), landedbo.EffectiveBegin), 112) AND CONVERT(DATE, CONVERT(VARCHAR(8), landedbo.EffectiveEnd), 112)
AND GETDATE() BETWEEN CONVERT(DATE, CONVERT(VARCHAR(8), onlineb.EffectiveBegin), 112) AND CONVERT(DATE, CONVERT(VARCHAR(8), onlineb.EffectiveEnd), 112)
AND GETDATE() BETWEEN CONVERT(DATE, CONVERT(VARCHAR(8), onlinecb.EffectiveBegin), 112) AND CONVERT(DATE, CONVERT(VARCHAR(8), onlinecb.EffectiveEnd), 112)
AND GETDATE() BETWEEN CONVERT(DATE, CONVERT(VARCHAR(8), landedContract.ProgramStartDate), 112) AND CONVERT(DATE, CONVERT(VARCHAR(8), landedContract.ProgramEndDate), 112)
and landedContract.DivisionCode = 40
--and it.ItemTypeid = 36
--and onlineContract.contractid = 21860
order by onlinecontract.contractid