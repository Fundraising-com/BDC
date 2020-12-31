SELECT	acc.ID, s.Instance, s.LastName, s.FirstName, count(*)
FROM	CustomerOrderHeader coh
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
			AND	cod.ProductType = 46001
JOIN	Student s
			ON	s.Instance = coh.StudentInstance
			AND	(s.LastName <> 'ZZ' AND s.FirstName <> 'ZZ' AND s.LastName <> 'UNKNOWN')
JOIN	QSPCanadaCommon..Campaign c
			ON	c.ID = coh.CampaignID
JOIN	QSPCanadaCommon..CAccount acc
			ON	acc.ID = c.ShipToAccountID
WHERE	coh.CreationDate >= '2007-07-01'
GROUP BY acc.ID, acc.Name, s.Instance, s.FirstName, s.LastName
HAVING	count(*) >= 5
ORDER BY acc.ID, count(*) DESC, s.LastName, s.FirstName