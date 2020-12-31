USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[PickListForNonEnvelope]    Script Date: 06/07/2017 09:18:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[PickListForNonEnvelope] As 
SELECT DISTINCT 
                      b.[Date], b.ID, b.OrderID, b.DateCreated, b.DateReceived, b.OrderTypeCode, b.CampaignID, f.FMID, f.FirstName, f.LastName, a.Id AS BillToAccountId, 
                      a.Name AS BillToAccount, Addr.street1 AS AccountAddress1, Addr.street2 AS AccountAddress2, Addr.city AS AccountCity, 
                      Addr.stateProvince AS AccountProvince, Addr.postal_code AS AccountPCode, shipa.Id AS ShipAccountId, shipa.Name AS ShiptoAccount, 
                      ShipAdr.street1 AS ShipAccountAddress1, ShipAdr.street2 AS ShipAccountAddress2, ShipAdr.city AS ShipAccountCity, 
                      ShipAdr.stateProvince AS ShipAccountProvince, ShipAdr.postal_code AS ShipAccountPcode, FMAddr.street1 AS FMAddress1, 
                      FMAddr.street2 AS FMAddress2, FMAddr.city AS FmCity, FMAddr.stateProvince AS FMProvince, FMAddr.postal_code AS FMPCode, 
                      b.ContactFirstName AS AccountContactFname, b.ContactLastName AS AccountContactLname, b.ContactPhone AS AccountContactPhone, b.BillToFMID, 
                      b.ShipToFMID, bShip.ContactFirstName AS ShipContactFname, bShip.ContactLastName AS ShipContactLname, 
                      bShip.ContactPhone AS ShipContactPhone, pr.Product_Code, ISNULL(pd.PRODUCT_DESCRIPTION_ALT, od.ProductName) 
                      AS PRODUCT_DESCRIPTION_ALT, pr.OracleCode, pr.Product_Code CatalogProductCode, pd.LANGUAGE_CODE, od.Quantity, od.CatalogPrice, od.QuantityShipped, 
                      od.StatusInstance, od.ReplacedProductCode, od.ReplacedProductQty, pn.PickNumber, od.ShipmentID, s.ShipmentDate, a.Lang AS AccountLang, 
                      c.Lang AS CALang, ph.PhoneNumber AS FMContact, od.Recipient, od.SupporterName, EmpBilltoAddr.street1 AS QSPAccBillToAddress1, 
                      EmpBilltoAddr.street2 AS QSPAccBillToAddress2, EmpBilltoAddr.city AS QSPAccBillToCity, EmpBilltoAddr.stateProvince AS QSPAccBillToProvince, 
                      EmpBilltoAddr.postal_code AS QSPAccBillToPcode, EmpShiptoAddr.street1 AS QSPAccShipToAddress1, 
                      EmpShiptoAddr.street2 AS QSPAccShipToAddress2, EmpShiptoAddr.city AS QSPAccShiptoCity, EmpShiptoAddr.stateProvince AS QSPAccShiptoProvince,
                       EmpShiptoAddr.postal_code AS QSPAccShiptoPcode, od.CustomerOrderHeaderInstance, od.TransID
FROM         dbo.Batch b INNER JOIN
                      dbo.CustomerOrderHeader oh ON b.ID = oh.OrderBatchID AND b.[Date] = oh.OrderBatchDate INNER JOIN
                      dbo.CustomerOrderDetail od ON oh.Instance = od.CustomerOrderHeaderInstance INNER JOIN
                      QSPCanadaProduct.dbo.PRICING_DETAILS pr ON od.PricingDetailsID = pr.MagPrice_Instance LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Address EmpBilltoAddr ON b.BillToAddressID = EmpBilltoAddr.address_id LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Address EmpShiptoAddr ON b.ShipToAddressID = EmpShiptoAddr.address_id LEFT OUTER JOIN
                      dbo.Batch bShip ON b.ID = bShip.ID AND b.[Date] = bShip.[Date] AND b.ShipToAccountID = bShip.AccountID LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Address ShipAdr INNER JOIN
                      QSPCanadaCommon.dbo.CAccount shipa ON ShipAdr.AddressListID = shipa.AddressListID AND ShipAdr.address_type = 54001 ON 			    --ShipTo
                      b.ShipToAccountID = shipa.Id LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Address Addr INNER JOIN
                      QSPCanadaCommon.dbo.CAccount a ON Addr.AddressListID = a.AddressListID AND Addr.address_type = 54002 ON b.AccountID = a.Id LEFT OUTER JOIN --BillTo
                      dbo.Shipment s ON od.ShipmentID = s.ID LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Phone ph INNER JOIN
                      QSPCanadaProduct.dbo.ProductDescription pd INNER JOIN
                      QSPCanadaCommon.dbo.Address FMAddr INNER JOIN
                      QSPCanadaCommon.dbo.FieldManager f INNER JOIN
                      QSPCanadaCommon.dbo.Campaign c ON f.FMID = c.FMID ON FMAddr.AddressListID = f.AddressListID ON ISNULL(pd.LANGUAGE_CODE, 'EN') 
                      = ISNULL(c.Lang, 'EN') ON ph.PhoneListID = f.PhoneListID AND ph.Type = 30502 ON b.CampaignID = c.ID AND 
                      pr.OracleCode = pd.PRODUCT_CODE LEFT OUTER JOIN
                      dbo.PickNumber pn ON pd.CatalogProductCode = pn.CatalogProductCode
WHERE    (
		 (b.OrderTypeCode IN (41002, 41005, 41006, 41007, 41010) AND (od.ProductType Not In (  Cast(46001 as Varchar) ,      Cast(46006 as Varchar)  )) )
	OR
                            (b.OrderTypeCode = 41008  AND (od.ProductType Not In ( Cast(46001 as Varchar) , Cast(46006 as Varchar)  ) AND (ISNULL(b.CampaignID, 0) = 0) ))
	     )
And b.StatusInstance in (40010 , 40013 ) --Picked and fulfilled Added Nov 5, 04 MS
And od.DelFlag = 0

/*SELECT DISTINCT 
                      b.[Date], b.ID, b.OrderID, b.DateCreated, b.DateReceived, b.OrderTypeCode, b.CampaignID, f.FMID, f.FirstName, f.LastName, a.Id AS BillToAccountId, 
                      a.Name AS BillToAccount, Addr.street1 AS AccountAddress1, Addr.street2 AS AccountAddress2, Addr.city AS AccountCity, 
                      Addr.stateProvince AS AccountProvince, Addr.postal_code AS AccountPCode, shipa.Id AS ShipAccountId, shipa.Name AS ShiptoAccount, 
                      ShipAdr.street1 AS ShipAccountAddress1, ShipAdr.street2 AS ShipAccountAddress2, ShipAdr.city AS ShipAccountCity, 
                      ShipAdr.stateProvince AS ShipAccountProvince, ShipAdr.postal_code AS ShipAccountPcode, FMAddr.street1 AS FMAddress1, 
                      FMAddr.street2 AS FMAddress2, FMAddr.city AS FmCity, FMAddr.stateProvince AS FMProvince, FMAddr.postal_code AS FMPCode, 
                      b.ContactFirstName AS AccountContactFname, b.ContactLastName AS AccountContactLname, b.ContactPhone AS AccountContactPhone, b.BillToFMID, 
                      b.ShipToFMID, bShip.ContactFirstName AS ShipContactFname, bShip.ContactLastName AS ShipContactLname, 
                      bShip.ContactPhone AS ShipContactPhone, pr.Product_Code, IsNull(pd.PRODUCT_DESCRIPTION_ALT,od.productname) PRODUCT_DESCRIPTION_ALT, pr.OracleCode, pd.CatalogProductCode, 
                      pd.LANGUAGE_CODE, od.Quantity, od.CatalogPrice, od.QuantityShipped, od.StatusInstance, od.ReplacedProductCode, od.ReplacedProductQty, 
                      pn.PickNumber, od.ShipmentID, s.ShipmentDate, a.Lang AS AccountLang, c.Lang AS CALang, ph.PhoneNumber AS FMContact, od.Recipient, 
                      od.SupporterName, EmpBilltoAddr.street1 AS QSPAccBillToAddress1, EmpBilltoAddr.street2 AS QSPAccBillToAddress2, 
                      EmpBilltoAddr.city AS QSPAccBillToCity, EmpBilltoAddr.stateProvince AS QSPAccBillToProvince, EmpBilltoAddr.postal_code AS QSPAccBillToPcode, 
                      EmpShiptoAddr.street1 AS QSPAccShipToAddress1, EmpShiptoAddr.street2 AS QSPAccShipToAddress2, EmpShiptoAddr.city AS QSPAccShiptoCity, 
                      EmpShiptoAddr.stateProvince AS QSPAccShiptoProvince, EmpShiptoAddr.postal_code AS QSPAccShiptoPcode
FROM         dbo.Batch b INNER JOIN
                      dbo.CustomerOrderHeader oh ON b.ID = oh.OrderBatchID AND b.[Date] = oh.OrderBatchDate INNER JOIN
                      dbo.CustomerOrderDetail od ON oh.Instance = od.CustomerOrderHeaderInstance INNER JOIN
                      QSPCanadaProduct.dbo.PRICING_DETAILS pr ON od.PricingDetailsID = pr.MagPrice_Instance LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Address EmpBilltoAddr ON b.BillToAddressID = EmpBilltoAddr.address_id LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Address EmpShiptoAddr ON b.ShipToAddressID = EmpShiptoAddr.address_id LEFT OUTER JOIN
                      dbo.Batch bShip ON b.ID = bShip.ID AND b.[Date] = bShip.[Date] AND b.ShipToAccountID = bShip.AccountID LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Address ShipAdr INNER JOIN
                      QSPCanadaCommon.dbo.CAccount shipa ON ShipAdr.AddressListID = shipa.AddressListID ON b.ShipToAccountID = shipa.Id LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Address Addr INNER JOIN
                      QSPCanadaCommon.dbo.CAccount a ON Addr.AddressListID = a.AddressListID ON b.AccountID = a.Id LEFT OUTER JOIN
                      dbo.Shipment s ON od.ShipmentID = s.ID LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.Phone ph INNER JOIN
                      QSPCanadaProduct.dbo.ProductDescription pd INNER JOIN
                      QSPCanadaCommon.dbo.Address FMAddr INNER JOIN
                      QSPCanadaCommon.dbo.FieldManager f INNER JOIN
                      QSPCanadaCommon.dbo.Campaign c ON f.FMID = c.FMID ON FMAddr.AddressListID = f.AddressListID ON ISNULL(pd.LANGUAGE_CODE, 'EN') 
                      = ISNULL(c.Lang, 'EN') ON ( ph.PhoneListID = f.PhoneListID AND ph.Type = 30502) ON b.CampaignID = c.ID AND pr.OracleCode = pd.PRODUCT_CODE LEFT OUTER JOIN
                      dbo.PickNumber pn ON pd.CatalogProductCode = pn.CatalogProductCode
WHERE    (
		 (b.OrderTypeCode IN (41002, 41005, 41006, 41007, 41010) AND (od.ProductType Not In (  Cast(46001 as Varchar) ,      Cast(46006 as Varchar)  )) )
	OR
                            (b.OrderTypeCode = 41008  AND (od.ProductType Not In ( Cast(46001 as Varchar) , Cast(46006 as Varchar)  ) AND (ISNULL(b.CampaignID, 0) = 0) ))
	     )
--And b.StatusInstance =40010  --Picked
*/
GO
