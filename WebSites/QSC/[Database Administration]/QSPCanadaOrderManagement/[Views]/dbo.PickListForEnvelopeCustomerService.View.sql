USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[PickListForEnvelopeCustomerService]    Script Date: 06/07/2017 09:18:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  VIEW [dbo].[PickListForEnvelopeCustomerService] AS
SELECT        Distinct
                      b.[Date], b.ID, b.OrderID, b.StatusInstance AS BatchStatus, b.Comment,b.DateCreated, b.DateReceived, b.OrderTypeCode, b.CampaignID, f.FMID, f.FirstName, f.LastName, a.Id AS BillToAccountId, 
                      a.Name AS BillToAccount, Addr.street1 AS AccountAddress1, Addr.street2 AS AccountAddress2, Addr.city AS AccountCity, 
                      Addr.stateProvince AS AccountProvince, Addr.postal_code AS AccountPCode, shipa.Id AS ShipAccountId, shipa.Name AS ShiptoAccount, 
                      ShipAdr.street1 AS ShipAccountAddress1, ShipAdr.street2 AS ShipAccountAddress2, ShipAdr.city AS ShipAccountCity, 
                      ShipAdr.stateProvince AS ShipAccountProvince, ShipAdr.postal_code AS ShipAccountPcode, b.ContactFirstName AS AccountContactFname, 
                      b.ContactLastName AS AccountContactLname, b.ContactPhone AS AccountContactPhone, b.BillToFMID, b.ShipToFMID, 
                      bShip.ContactFirstName AS ShipContactFname, bShip.ContactLastName AS ShipContactLname, bShip.ContactPhone AS ShipContactPhone, 
                      pr.Product_Code, ISNULL(pd.PRODUCT_DESCRIPTION_ALT, od.ProductName) AS PRODUCT_DESCRIPTION_ALT, pr.OracleCode, 
                      pr.Product_Code CatalogProductCode, pd.LANGUAGE_CODE, od.Quantity, od.CatalogPrice, od.QuantityShipped, od.StatusInstance, od.ReplacedProductCode, 
                      od.ReplacedProductQty, pn.PickNumber, od.ShipmentID, s.ShipmentDate, a.Lang AS AccountLang, c.Lang AS CALang, 
                      IsNull(Upper(RTrim(LTrim(dbo.[UDF_RemoveTitle](t.LastName)))), 'UNKNOWN') AS TeacherLastName, UPPER(RTRIM(LTRIM(t.FirstName))) AS TeacherFirstName, t.Classroom AS Class, 
                      p.FirstName AS ParticipantFirstName, p.LastName AS ParticipantLastName,p.Instance AS ParticipantInstance, od.Recipient,
	         CASE ISNULL(C.SuppliesDeliveryDate, '01/01/1995')
			WHEN  '01/01/1995' THEN Null
			ELSE 	CONVERT(VARCHAR(10),C.SuppliesDeliveryDate,101) 
			END AS SuppliesDeliveryDate,
			CASE
				WHEN b.OrderDeliveryDate IS NULL THEN	GETDATE()
				ELSE b.OrderDeliveryDate - (prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep)
			END AS RequestedShipDate,
			od.CustomerOrderHeaderInstance,
			od.TransID,
			c.CarrierID,
			c.SpecialInstructions,
			pl.ShipmentGroupID

FROM		Batch b 
JOIN		CustomerOrderHeader oh ON b.ID = oh.OrderBatchID AND b.[Date] = oh.OrderBatchDate
JOIN		CustomerOrderDetail od ON oh.Instance = od.CustomerOrderHeaderInstance
LEFT JOIN	Student p ON p.Instance = oh.StudentInstance 
LEFT JOIN	Teacher t ON p.TeacherInstance = t.Instance
JOIN		QSPCanadaProduct..PRICING_DETAILS pr ON od.PricingDetailsID = pr.MagPrice_Instance 
LEFT JOIN	OrderInEnvelopeMap m ON oh.Instance = m.CustomerOrderHeaderInstance
LEFT JOIN	Envelope e ON e.Instance = m.EnvelopeID
LEFT JOIN	Batch bShip ON b.ID = bShip.ID AND b.[Date] = bShip.[Date] AND b.ShipToAccountID = bShip.ID 
LEFT JOIN	QSPCanadaCommon..CAccount shipa ON b.ShipToAccountID = shipa.Id
LEFT JOIN	QSPCanadaCommon..Address ShipAdr ON ShipAdr.AddressListID = shipa.AddressListID AND ShipAdr.address_type = 54001
JOIN		QSPCanadaCommon..CAccount a ON b.AccountID = a.Id
LEFT JOIN	QSPCanadaCommon..Address Addr ON Addr.AddressListID = a.AddressListID AND Addr.address_type = 54002  
LEFT JOIN   Shipment s ON od.ShipmentID = s.ID
JOIN		QSPCanadaCommon..Campaign c ON b.CampaignID = c.ID
LEFT JOIN	QSPCanadaCommon..FieldManager f ON f.FMID = c.FMID
LEFT JOIN	QSPCanadaProduct..ProductDescription pd ON pd.PRODUCT_CODE = pr.OracleCode AND ISNULL(pd.LANGUAGE_CODE, 'EN') = ISNULL(c.Lang, 'EN')
LEFT JOIN	PickNumber pn ON pd.CatalogProductCode = pn.CatalogProductCode 
JOIN		QSPCanadaCommon..Province prov ON prov.Province_Code = Addr.stateProvince
JOIN		QSPCanadaCommon..QSPProductLine pl ON pl.ID = od.ProductType

WHERE   od.ProductType NOT IN (46001, 46017, 46021, 46023)
AND		(b.orderTypeCode in (41001,41009) OR (b.orderTypeCode=41008 and IsNull(b.CampaignId,0)>0))	
AND		b.StatusInstance in (40010, 40013) --Picked and fulfilled Added Nov 5, 04 MS
AND		od.DelFlag = 0
AND		od.StatusInstance NOT IN (500, 501, 506, 508, 513)
AND		od.DistributionCenterID = 1
GO
