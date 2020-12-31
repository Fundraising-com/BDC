SELECT		*
FROM		Incident i
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = i.CustomerOrderHeaderInstance
				AND	cod.TransID = i.TransID
				AND	cod.ProductCode in (SELECT	p.Product_Code
										FROM	QSPCanadaProduct..Product p
										WHERE	p.Fulfill_House_Nbr in (29, 107) )
WHERE		i.DateCreated >= '2006-07-01'
AND			i.Comments not like '%automat%'
ORDER BY	i.DateCreated

select * from problemcode
select * from incidentaction where incidentinstance in (390904, 390905)
select * from action
select * from qspcanadaproduct..fulfillment_house order by ful_name