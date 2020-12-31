SELECT	so.OrderID,
	b.CampaignID,
	b.AccountID,
	ship.FMEmailNotificationSent
FROM	Shipment ship
JOIN	ShipmentOrder so
		ON	so.ShipmentID = ship.ID
JOIN	Batch b
		ON	b.OrderID = so.OrderID
JOIN	QSPCanadaCommon..Campaign camp
		ON	camp.ID = b.CampaignID
WHERE	ship.ShipmentDate > '8/29/05'
AND	camp.FMID = '0094'
ORDER BY ship.FMEmailNotificationSent