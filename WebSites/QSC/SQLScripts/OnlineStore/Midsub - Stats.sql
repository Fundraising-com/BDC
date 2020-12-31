--Orders Due to Midsub. However there was a bug between Nov.11/14 and Apr.1/15 causing the orders resulting from the midsub email to not show in the query (the Storefrontsession.EmailID wasn’t being populated)
--Division			Items	Sales
--BDC Canada		3		98.00
--BDC U.S.			59		1514.00
--Great American	15415	405169.66
--QSP Canada		1922	70784.00
SET TRANSACTION ISOLATION LEVEL SNAPSHOT  
SELECT		dt.Description Division,
			SUM(cod.Quantity) NumberLineItemsOrderedDueToMidsub,
			SUM(cod.Quantity * cod.OfferValue) SalesAmountOrderedDueToMidSub
FROM		Store.StorefrontSession ss 
JOIN		Core.CustomerOrder co ON co.CustomerOrderID = ss.CustomerOrderID 
JOIN		core.CustomerSubOrder cso ON cso.CustomerOrderID = co.CustomerOrderID 
JOIN		Core.CustomerOrderDetail cod ON cod.CustomerSubOrderID = cso.CustomerSubOrderID 
join		Core.Tote t on t.toteid = co.ToteIDContract 
JOIN		Core.Contract ct on ct.contractid = t.contractid
JOIN		Core.DivisionType dt ON dt.Code = ct.DivisionCode
WHERE		ss.EmailID IN (SELECT EmailID 
							FROM Messaging.CustomerOrderMidSubEmail)
AND			co.CustomerOrderStateID IN (11, 38, 39)
GROUP BY	dt.Description

--Number of Midsub Emails Sent
--Division			NumberMidsubEmailsSent
--BDC Canada		298
--BDC U.S.			3331
--Great American	663339
--QSP Canada		91826
SET TRANSACTION ISOLATION LEVEL SNAPSHOT  
SELECT		dt.Description Division,
			COUNT(*) NumberMidsubEmailsSent
from		core.CustomerOrder co
join		core.CustomerSubOrder cso on cso.customerorderid = co.customerorderid
join		core.CustomerOrderDetail cod on cod.customersuborderid = cso.customersuborderid
join		messaging.CustomerOrderMidSubEmail m on m.CustomerOrderDetailID = cod.customerorderdetailid
join		messaging.email e on e.EmailID = m.EmailID
join		core.Tote t on t.ToteID = co.ToteIDContract
join		core.Contract c on c.ContractID = t.ContractID
JOIN		Core.DivisionType dt ON dt.Code = c.DivisionCode
WHERE		e.emailstatecode IN (1, 7)
GROUP BY	dt.Description

--Number of FM's Opted Out = 10
SELECT	*
FROM	messaging.Emailsalespersonoptout espoo
JOIN	core.SalesPerson sp on sp.SalesPersonID = espoo.SalesPersonID
WHERE	espoo.EmailTemplateTypeCode = 2 -- Midsub