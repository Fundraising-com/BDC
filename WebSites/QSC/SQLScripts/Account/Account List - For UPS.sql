USE QSPCanadaCommon
GO

SELECT		DISTINCT
			acc.ID AccountID, 
			acc.Name AccountName,
			adShip.Street1 ShippingAddress,
			adShip.Street2 ShippingAddress2,
			adShip.City ShippingCity,
			adShip.StateProvince ShippingProvince,
			adShip.Postal_Code ShippingPostalCode
FROM		CAccount acc
JOIN		Campaign camp ON camp.ShipToAccountID = acc.Id
LEFT JOIN	Address adShip on adShip.AddressListID = acc.AddressListID AND adShip.Address_Type = 54001 --Ship To
WHERE		camp.StartDate >= '2016-07-01'
AND			camp.Status = 37002
ORDER BY	acc.Id
