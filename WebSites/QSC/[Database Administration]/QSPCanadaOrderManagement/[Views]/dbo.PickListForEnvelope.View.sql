USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[PickListForEnvelope]    Script Date: 06/07/2017 09:18:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  VIEW [dbo].[PickListForEnvelope] AS
SELECT     Distinct  oh.customerbilltoinstance,od.customershiptoinstance	,
	     	 b.[Date], 
	     	 b.ID,
		b.StatusInstance AS BatchStatus,
		Case b.OrderQualifierId
			When 39006 Then  b.comment 		--IsNull(b.comment,C.SpecialInstructions) MS Sept 12, 2006
			When 39007 Then C.SpecialInstructions 	-- IsNull(b.comment,C.SpecialInstructions)
			Else b.comment
		End Comment,
		b.OrderID, 
		b.DateCreated, 
		b.DateReceived, 
		b.OrderTypeCode, 
		b.CampaignID, 
		c.IsStaffOrder,
		f.FMID, 
		f.FirstName, 
		f.LastName, 
		B.AccountId AS BillToAccountId,
		Case WHEN b.OrderQualifierID IN (39009) AND od.IsShippedToAccount = 1 THEN billa.Name
            ELSE Case IsNull(Cust.FirstName,'')
                  When '' Then Cust.LastName 
                              Else Cust.FirstName+' '+Cust.LastName 
                  End
            END AS BillToAccount,
		billAdr.street1 AS AccountAddress1, 
		billAdr.street2 AS AccountAddress2,
		billAdr.City AS AccountCity, 
		billAdr.stateProvince AS AccountProvince, 
		billAdr.postal_code AS AccountPCode, 
		Case b.OrderQualifierId
		When 39017 Then Null
		Else billa.Id 
		End AS ShipAccountId, 
		Case WHEN b.OrderQualifierID IN (39001,39002,39009) AND od.IsShippedToAccount = 1 THEN billa.Name
            ELSE Case IsNull(Cust.FirstName,'')
                  When '' Then Cust.LastName 
                              Else Cust.FirstName+' '+Cust.LastName 
                  End
            END AS ShiptoAccount, 
		Case WHEN b.OrderQualifierID IN (39001,39002,39009) AND od.IsShippedToAccount = 1 THEN shipAdr.street1
			ELSE Cust.Address1
		END AS ShipAccountAddress1,
		Case WHEN b.OrderQualifierID IN (39001,39002,39009) AND od.IsShippedToAccount = 1 THEN shipAdr.street2
			ELSE Cust.Address2
		END AS ShipAccountAddress2, 
		Case WHEN b.OrderQualifierID IN (39001,39002,39009) AND od.IsShippedToAccount = 1 THEN shipAdr.City
			ELSE Cust.City
		END AS ShipAccountCity, 
		Case WHEN b.OrderQualifierID IN (39001,39002,39009) AND od.IsShippedToAccount = 1 THEN shipAdr.stateProvince
			ELSE Cust.State
		END AS ShipAccountProvince, 
		Case WHEN b.OrderQualifierID IN (39001,39002,39009) AND od.IsShippedToAccount = 1 THEN shipAdr.postal_code
			ELSE Cust.Zip
		END AS ShipAccountPcode, 
		b.ContactFirstName AS AccountContactFname, 
                	b.ContactLastName AS AccountContactLname, 
		b.ContactPhone AS AccountContactPhone, 
		b.BillToFMID, 
		b.ShipToFMID,
		ISNULL(bShip.ContactFirstName, campCont.FirstName) AS ShipContactFname,
		ISNULL(bShip.ContactLastName, campCont.LastName) AS ShipContactLname,
		Case WHEN b.OrderQualifierID IN (39009) AND od.IsShippedToAccount = 0 THEN cust.Phone
			ELSE ISNULL(bShip.ContactPhone, campContPhone.PhoneNumber)
		END AS ShipContactPhone,		
       	pr.Product_Code, ISNULL(pd.PRODUCT_DESCRIPTION_ALT, od.ProductName) AS PRODUCT_DESCRIPTION_ALT, 
		pr.OracleCode, 
		pr.Product_Code CatalogProductCode,--pd.CatalogProductCode, 
		pd.LANGUAGE_CODE, 
		od.Quantity, 
		od.CatalogPrice, 
		od.QuantityShipped, 
		od.StatusInstance, 
		od.ReplacedProductCode, 
                	od.ReplacedProductQty, 
		pn.PickNumber, 
		od.ShipmentID, 
		s.ShipmentDate, 
		billa.Lang AS AccountLang, 
		c.Lang AS CALang,
		IsNull(Upper(RTrim(LTrim(dbo.[UDF_RemoveTitle](t.LastName)))), 'UNKNOWN') as TeacherLastName,
		Upper(RTrim(LTrim(t.FirstName))) AS TeacherFirstName, 
		t.Classroom AS Class, 
                	p.FirstName AS ParticipantFirstName, 
		p.LastName AS ParticipantLastName,
		p.Instance AS ParticipantInstance, 
		od.Recipient,
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
		od.IsShippedToAccount,
		b.OrderQualifierID
			
FROM   QSPCanadaCommon.dbo.FieldManager f INNER JOIN
             QSPCanadaCommon.dbo.Campaign c ON f.FMID = c.FMID RIGHT OUTER JOIN
             QSPCanadaOrderManagement.dbo.Batch b INNER JOIN
             QSPCanadaOrderManagement.dbo.CustomerOrderHeader oh ON b.ID = oh.OrderBatchID AND b.[Date] = oh.OrderBatchDate INNER JOIN
             QSPCanadaOrderManagement.dbo.CustomerOrderDetail od ON oh.Instance = od.CustomerOrderHeaderInstance INNER JOIN
             QSPCanadaProduct.dbo.PRICING_DETAILS pr ON od.PricingDetailsID = pr.MagPrice_Instance INNER JOIN
             dbo.Batch bship ON b.ID = bship.ID AND b.[Date] = bship.[Date] AND b.ShipToAccountID = bship.AccountID LEFT OUTER JOIN
             QSPCanadaOrderManagement.dbo.Student p INNER JOIN
             QSPCanadaOrderManagement.dbo.Teacher t ON p.TeacherInstance = t.Instance ON oh.StudentInstance = p.Instance LEFT OUTER JOIN
             QSPCanadaOrderManagement.dbo.Envelope e INNER JOIN QSPCanadaOrderManagement.dbo.OrderInEnvelopeMap ON e.Instance = QSPCanadaOrderManagement.dbo.OrderInEnvelopeMap.EnvelopeID ON 
             oh.Instance = QSPCanadaOrderManagement.dbo.OrderInEnvelopeMap.CustomerOrderHeaderInstance LEFT OUTER JOIN
             QSPCanadaCommon.dbo.Address BillAdr INNER JOIN
             QSPCanadaCommon.dbo.CAccount billa ON BillAdr.AddressListID = billa.AddressListID AND BillAdr.address_type = 54002 ON 
                    b.AccountID = billa.Id LEFT OUTER JOIN QSPCanadaOrderManagement.dbo.Shipment s ON od.ShipmentID = s.ID ON c.ID = b.CampaignID LEFT OUTER JOIN
             QSPCanadaProduct.dbo.ProductDescription pd LEFT OUTER JOIN QSPCanadaOrderManagement.dbo.PickNumber pn ON pd.CatalogProductCode = pn.CatalogProductCode ON pr.OracleCode = pd.PRODUCT_CODE 
							AND ISNULL(c.Lang, 'EN')  = ISNULL(pd.LANGUAGE_CODE, 'EN')
			LEFT JOIN	QSPCanadaCommon..Contact campCont (NOLOCK)
							ON 	campCont.ID = c.ShipToCampaignContactID
			LEFT JOIN	QSPCanadaCommon..Phone campContPhone
							ON	campContPhone.PhoneListID = campCont.PhoneListID
							AND	campContPhone.Type = 30501 --30501: Phone Type - Work
			JOIN		QSPCanadaOrderManagement.dbo.Customer cust
							ON	cust.Instance =	CASE ISNULL(od.CustomerShipToInstance, 0)
													WHEN 0 THEN oh.CustomerBillToInstance
													ELSE		od.CustomerShipToInstance
												END

			JOIN		QSPCanadaCommon..Province prov
							ON	prov.Province_Code = cust.[State]
			LEFT JOIN	QSPCanadaCommon..Address shipAdr
							ON	shipAdr.AddressListID = billa.AddressListID AND shipAdr.address_type = 54001
WHERE   
 (	 (od.ProductType NOT IN (46001, 46017, 46021, 46023) And b.orderTypeCode in (41001,41009)) 	--Campaign , Magnet
        OR 
	(od.ProductType NOT IN (46001, 46017, 46021, 46023) And (b.orderTypeCode=41008  And (IsNull(b.CampaignId,0)>0)) )	--Group Straight order
        OR 
	(od.ProductType NOT IN (46001, 46017, 46021, 46023) And (b.orderTypeCode=41008  And (IsNull(b.OrderQualifierId,0)=39017)) )	--Kanata /WFC
     )
And b.StatusInstance in (40010 , 40013,40014 ) --Picked and fulfilled, ParTially Fulfilled Added Nov 5, 04 MS
And od.DelFlag = 0
And od.StatusInstance NOT IN (500, 501, 506, 508, 513)
And od.DistributionCenterID = 1

GO
