USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[PickListNonEnvelope1]    Script Date: 06/07/2017 09:18:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  VIEW [dbo].[PickListNonEnvelope1] AS 
SELECT DISTINCT 	
			cust.instance CustomerInstance, 
			cust.type CustomerType, 
			cust.firstname CustomerFirstName, 
			cust.lastname CustomerLastName, 
			cust.address1 CustomerAddress1, 
			cust.address2 CustomerAddress2, 
			cust.city CustomerCity,
			cust.state CustomerState, 
			cust.zip CustomerZip, 
			cust.phone CustomerPhone, 
			b.OrderID, b.DateCreated, 
			b.DateReceived, 
			b.OrderTypeCode, 
			b.StatusInstance BatchStatus,
			Case b.OrderQualifierId
			When 39006 Then b.comment		--  IsNull(b.comment,C.SpecialInstructions) MS Sept 12, 2006
			When 39007 Then  C.SpecialInstructions 	--IsNull(b.comment,C.SpecialInstructions)
			Else b.comment
			End Comment,
			b.CampaignID, 
			c.IsStaffOrder,
			f.FMID, 
			f.FirstName, 
			f.LastName, 
			a.Id BillToAccountId, 
			a.Name BillToAccount, 
			addr.Street1 AccountAddress1, 
			addr.Street2 AccountAddress2, 
			addr.City AccountCity, 
			addr.stateProvince AccountProvince, 
			addr.Postal_Code AccountPcode, 
			ISNULL(accSchool.ID, shipa.Id) ShipAccountId, 
			ISNULL(accSchool.Name, shipa.Name) ShiptoAccount, 
			b.ContactFirstName AccountContactFname, 
			b.ContactLastName AccountContactLname, 
			b.ContactPhone AccountContactPhone, 
			bShip.ContactFirstName ShipContactFname, 
			bShip.ContactLastName ShipContactLname, 
			bShip.ContactPhone ShipContactPhone, 
			pr.Product_Code AS OracleCode, 
			ISNULL(pd.Product_Description_Alt, od.ProductName) Product_Description_Alt, 
			pr.Product_Code, 
			pr.Product_Code CatalogProductCode,--pd.CatalogProductCode, 
			pd.Language_Code, 
			od.Quantity, 
			od.CatalogPrice, 
			od.QuantityShipped, 
			od.StatusInstance, 
			od.ReplacedProductCode, 
			od.ReplacedProductQty, 
			pn.PickNumber, 
			od.ShipmentID, 
			s.ShipmentDate, 
			a.Lang AccountLang, 
			c.Lang CALang, 
			ph.PhoneNumber FMContact, 
			od.Recipient, 
			od.SupporterName,
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

FROM            QSPCanadaCommon.dbo.Address addr INNER JOIN
                      QSPCanadaCommon.dbo.CAccount a ON addr.AddressListID = a.AddressListID AND addr.address_type = 54002 RIGHT OUTER JOIN
                      QSPCanadaOrderManagement.dbo.Batch b INNER JOIN 
                      QSPCanadaOrderManagement.dbo.CustomerOrderHeader oh ON b.ID = oh.OrderBatchID AND b.[Date] = oh.OrderBatchDate INNER JOIN
                      QSPCanadaOrderManagement.dbo.CustomerOrderDetail od ON oh.Instance = od.CustomerOrderHeaderInstance INNER JOIN
                      QSPCanadaOrderManagement.dbo.Customer cust ON cust.Instance =	CASE ISNULL(od.CustomerShipToInstance, 0) WHEN 0 THEN oh.CustomerBillToInstance ELSE od.CustomerShipToInstance END INNER JOIN
                      QSPCanadaProduct.dbo.PRICING_DETAILS pr ON od.PricingDetailsID = pr.MagPrice_Instance LEFT OUTER JOIN
                      QSPCanadaOrderManagement.dbo.Batch bShip ON b.ID = bShip.ID AND b.[Date] = bShip.[Date] AND b.ShipToAccountID = bShip.AccountID LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.CAccount shipa ON b.ShipToAccountID = shipa.Id ON a.Id = b.AccountID LEFT OUTER JOIN
                      QSPCanadaOrderManagement.dbo.Shipment s ON od.ShipmentID = s.ID LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Phone ph RIGHT OUTER JOIN
                      QSPCanadaProduct.dbo.ProductDescription pd INNER JOIN
                      QSPCanadaCommon.dbo.FieldManager f INNER JOIN
                      QSPCanadaCommon.dbo.Campaign c ON f.FMID = c.FMID ON ISNULL(pd.LANGUAGE_CODE, 'EN') = ISNULL(c.Lang, 'EN') ON 
                      ph.PhoneListID = f.PhoneListID AND ph.Type = 30502 ON b.CampaignID = c.ID AND pr.Product_Code = pd.PRODUCT_CODE LEFT OUTER JOIN   
				       --QSPCanadaOrderManagement.dbo.PickNumber pn ON pd.CatalogProductCode = pn.CatalogProductCode 
						QSPCanadaOrderManagement.dbo.PickNumber pn ON pd.PRODUCT_CODE = pn.CatalogProductCode  --Pick# CatalogProductCode column has Oracle code MS Jun17, 2005
	        		  JOIN		QSPCanadaCommon..Province prov ON prov.Province_Code = cust.[State]
					  LEFT JOIN	QSPCanadaCommon..CAccount accSchool ON accSchool.Id = dbo.UDF_GetSchoolAccountIDFromOrderInFMAccount(b.OrderID)
					  JOIN		QSPCanadaCommon..QSPProductLine pl ON pl.ID = od.ProductType

WHERE /* ( (b.OrderTypeCode IN (41002, 41005, 41006, 41007, 41010) AND
	   (od.ProductType NOT IN ( Cast(46001 AS Varchar) , Cast(46006 AS Varchar) )) ) OR 
	  (b.OrderTypeCode = 41008 AND 
	  (od.ProductType NOT IN ( Cast(46001 AS Varchar) , Cast(46006 AS Varchar) ) AND (ISNULL(b.CampaignID, 0) = 0) )) ) */ --Disabled by MS Jun30,2005, No staright orders Include Kanata

	  ( 
	      (b.OrderTypeCode IN (41002, 41005, 41006, 41007, 41010,41011) AND od.ProductType NOT IN (46001, 46017, 46021, 46023)) 
                   OR 
	      (b.OrderTypeCode = 41001 AND b.OrderQualifierId = 39006 AND od.ProductType NOT IN (46001, 46017, 46021, 46023))
      ) 
AND		b.StatusInstance IN (40010, 40013, 40014) -- Picked , Fulfilled, Partially Fulfilled
AND		od.DelFlag = 0
AND		od.StatusInstance NOT IN (500, 501, 506, 508, 513)
AND		od.DistributionCenterID = 1
GO
