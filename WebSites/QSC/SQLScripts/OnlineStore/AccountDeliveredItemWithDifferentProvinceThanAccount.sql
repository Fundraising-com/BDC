SELECT		DISTINCT
			co.CustomerOrderID, c.SAPContractNo CampaignID, ca.SAPAcctNo AccountID, ca.Name1 AccountName, ca.Address AccountAddress, ca.City AccountCity, ca.StateProvinceAbbr AccountStateProvince, ca.PostalCode AccountPostalCode,
			cod.OfferCode, cod.OfferQuantity, cod.OfferValue, i.ItemDescShort ProductName, co.Created OrderDate, CASE ISNULL(tL.ToteID, 0) WHEN 0 THEN 'No' ELSE 'Yes' END LandedOrderInFFS,
			cso.SendToName Recipient, custAdd.Address1 CustomerAddress1, custAdd.Address2 CustomerAddress2,
			custAdd.UnitNumber CustomerUnitNumber, custAdd.City CustomerCity, custAdd.StateProvinceAbbr CustomerStateProvince, custAdd.PostalCode CustomerPostalCode, custAdd.PhoneNumber CustomerPhoneNumber
FROM		core.CustomerOrderDetail cod WITH (NOLOCK)
JOIN		core.CustomerSubOrder cso WITH (NOLOCK) ON cso.CustomerSubOrderID = cod.CustomerSubOrderID
JOIN		core.customerAddress custAdd WITH (NOLOCK) ON custAdd.CustomerAddressid = cso.CustomerAddressID
JOIN		core.CustomerOrder co WITH (NOLOCK) ON co.CustomerOrderID = cso.CustomerOrderID
JOIN		core.Tote t WITH (NOLOCK) ON t.ToteID = co.ToteIDContract
JOIN		core.Contract c WITH (NOLOCK) ON c.ContractID = t.ContractID
JOIN		core.ContractAddress ca WITH (NOLOCK) ON ca.ContractID = c.ContractID AND ca.IsSoldTo = 1
LEFT JOIN	(core.ContractAddress caL WITH (NOLOCK) 
		JOIN	core.Contract cL WITH (NOLOCK) ON cL.ContractID = caL.ContractID 
		JOIN	core.Tote tL WITh (NOLOCK) ON tL.ContractID = cL.ContractID)
		ON caL.SAPAcctNo = ca.SAPAcctNo AND caL.IsSoldTo = 1 AND cL.DivisionCode in (40, 41) AND tl.LastWorkflowStepTypeID = 6 AND cL.ContractTypeID = 2 AND cL.ProgramStartDate >= 20160701
LEFT JOIN	core.Item i WITH (NOLOCK) ON i.UPCCode = cod.OfferCode AND SUBSTRING(SAPID, 1, 5) = 'QSPCA'
WHERE		ca.StateProvinceAbbr <> custAdd.StateProvinceAbbr
AND			cod.IsShippedToAccount = 1
AND			co.CustomerOrderStateID NOT IN (22)
AND			c.DivisionCode IN (40, 41)
ORDER BY	co.CustomerOrderID