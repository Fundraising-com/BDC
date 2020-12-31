USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_RptGetSummaryInfo]    Script Date: 06/07/2017 09:33:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_RptGetSummaryInfo]
	@ICampaignID	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Get Summary info for reports
--  KET  8/13/2004  - Inital Revision
--  JLC 01/20/2005  - Corrected joins, type values, to eliminate null columns within the tuples.
--  SSHAH 06/21/2005 - Added Company info (qsp, qspFR, courier)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON 

SELECT   
	  c.[ID] as CampaignID
	, C.FMID
	, FM.FirstName + ' ' + FM.LastName as FMName
	, FM.Email as FMEMail
	, qspcanadacommon.dbo.FormatPhoneUS_dash(FP.PhoneNumber) as PhoneNumber
	, qspcanadacommon.dbo.FormatPhoneUS_dash(FMFax.PhoneNumber) as Fax
	, IsNULL(BillCO.FirstName, '') + ' ' + IsNULL(BillCO.LastName, '') as BillContactName
	, IsNULL(ShipCO.FirstName, '') + ' ' + IsNULL(ShipCO.LastName, '') as ShipContactName
	,  BillContactPhone.PhoneNumber as BillContactPhone
	,  ShipContactPhone.PhoneNumber as ShipContactPhone
	, ShiptoAccountID
	, A.[Name] as ShipAccountName
	, qspcanadacommon.dbo.FormatPhoneUS_dash(IsNULL(P.PhoneNumber,''))  as ShipToPhone
	, AdShip.Street1            as ShippingAddress
	, IsNULL(AdShip.Street2,'') as ShippingAddress2
	, AdShip.City      	    as ShippingCity
	, AdShip.StateProvince 	    as ShippingState
	, AdShip.Postal_Code        as ShippingZip
	, C.BillToAccountID
	, A.[Name] as BillAccountName
	, qspcanadacommon.dbo.FormatPhoneUS_dash(IsNULL(P.PhoneNumber,''))  as BillToPhone
	, AdBill.Street1 	    as BillingAddress
	, IsNULL(AdBill.Street2,'') as BillingAddress2
	, AdBill.City      	    as BillingCity
	, AdBill.StateProvince	    as BillingState
	, AdBill.Postal_Code        as BillingZip

    , qsp.CompanyName 	as QSPCompanyName
    , qsp.ShipAddress1 	as QSPShipAddress1
    , qsp.ShipAddress2 	as QSPShipAddress2 
    , qsp.ShipCity   	as QSPShipCity
    , qsp.ShipProvince    	as QSPShipProvince
    , qsp.ShipPostalCode  as QSPShipPostalCode
    , qsp.ShipPhone1      	as QSPShipPhone1

    , QspFR.CompanyName 	as QspFRCompanyName
    , QspFR.ShipAddress1 	as QspFRShipAddress1
    , QspFR.ShipAddress2 	as QspFRShipAddress2 
    , QspFR.ShipCity   	  	as QspFRShipCity
    , QspFR.ShipProvince    	as QspFRShipProvince
    , QspFR.ShipPostalCode  	as QspFRShipPostalCode
    , QspFR.ShipPhone1      	as QspFRShipPhone1

    , courier.CompanyName 	as CourierCompanyName
    , courier.ShipAddress1 	as CourierShipAddress1
    , courier.ShipAddress2  	as CourierShipAddress2 
    , courier.ShipCity     		as CourierShipCity
    , courier.ShipProvince 		as CourierShipProvince
    , courier.ShipPostalCode 	as CourierShipPostalCode
    , courier.ShipPhone1  		as CourierShipPhone1

    , s.Season
     , CASE c.lang WHEN 'FR' THEN CASE s.Season 	WHEN 'F' THEN 'D''AUTOMNE ' + Substring(s.Name,CHARINDEX('2',s.Name,1),4)
					   		WHEN 'S' THEN 'DE PRINTEMPS ' + Substring(s.Name,CHARINDEX('2',s.Name,1),4)
					   		ELSE '' END
	      WHEN  'EN' THEN CASE s.Season 		WHEN 'F' THEN 'FALL ' + Substring(s.Name,CHARINDEX('2',s.Name,1),4)
					    		WHEN 'S' THEN 'SPRING ' + Substring(s.Name,CHARINDEX('2',s.Name,1),4)
					    		ELSE '' END
	      ELSE ''
    END SeasonName,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 47), CONVERT(BIT, 0)) AS RunsMagFS,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 1), CONVERT(BIT, 0)) AS RunsMag,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 2), CONVERT(BIT, 0)) AS RunsMagExpress,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 49), CONVERT(BIT, 0)) AS RunsCandle,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 50), CONVERT(BIT, 0)) AS RunsTRT,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 44), CONVERT(BIT, 0)) AS RunsCookieDough,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 52), CONVERT(BIT, 0)) AS RunsEntertainment,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 53), CONVERT(BIT, 0)) AS RunsEmbrace,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 54), CONVERT(BIT, 0)) AS RunsFestival,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 51), CONVERT(BIT, 0)) AS Runs59Minute,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 55), CONVERT(BIT, 0)) AS RunsOrganicEdibles,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 56), CONVERT(BIT, 0)) AS RunsKitchenCollection,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 57), CONVERT(BIT, 0)) AS RunsDonations,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 58), CONVERT(BIT, 0)) AS RunsNaturesHabit,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 59), CONVERT(BIT, 0)) AS RunsChocolateFlyer,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 61), CONVERT(BIT, 0)) AS RunsFFTTPopcorn,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 62), CONVERT(BIT, 0)) AS RunsStainlessSteelTravelCup,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 63), CONVERT(BIT, 0)) AS RunsDepositOnlyExtraService,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 64), CONVERT(BIT, 0)) AS RunsQSPSavingsPass,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 65), CONVERT(BIT, 0)) AS RunsGiftCard,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 66), CONVERT(BIT, 0)) AS RunsPapaJackPopcorn,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 67), CONVERT(BIT, 0)) AS RunsTervis,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 68), CONVERT(BIT, 0)) AS RunsPretzelRods40,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 69), CONVERT(BIT, 0)) AS RunsTheCureJewelry,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 70), CONVERT(BIT, 0)) AS RunsGourmetTastyTreats,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 71), CONVERT(BIT, 0)) AS RunsPretzelRods30,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 72), CONVERT(BIT, 0)) AS RunsLeapLabels,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 73), CONVERT(BIT, 0)) AS RunsCoolCards,
    ISNULL((SELECT CONVERT(BIT, 1) FROM Campaign C2 JOIN CampaignProgram cp ON cp.CampaignID = C2.ID WHERE C2.ID = C.ID AND cp.DeletedTF = 0 AND OnlineOnly = 0 AND cp.ProgramID = 74), CONVERT(BIT, 0)) AS RunsRally,
	fm.SAPAcctNo,
	--CAST(C.ContractID as varchar) + CAST(dbo.CalcMod10Checksum(C.ContractID) as varchar) AS GroupOnlineID
	dbo.UDF_Account_GetGroupOnlineID(A.Id) GroupOnlineID
FROM
	QSPCanadaCommon.dbo.Campaign C
	JOIN QSPCanadaCommon.dbo.Season s ON c.StartDate BETWEEN s.StartDate AND s.EndDate AND s.Season <> 'Y'
	LEFT  JOIN QSPCanadaCommon.dbo.Contact BillCO ON C.BillToCampaignContactID = BillCO.[Id]
	LEFT  JOIN QSPCanadaCommon.dbo.Contact ShipCO ON C.ShipToCampaignContactID = ShipCO.[Id]
	INNER JOIN QSPCanadaCommon.dbo.FieldManager FM ON C.FMID = FM.FMID 
	LEFT  JOIN QSPCanadaCommon.dbo.Phone FP on (FM.PhoneListID = FP.PhoneListID AND FP.Type=30505)-- fm use home phone
	LEFT  JOIN QSPCanadaCommon.dbo.Phone FMFax on (FMFax.PhoneListID = FM.PhoneListID AND FMFax.Type=30503)--fax
	INNER JOIN QSPCanadaCommon.dbo.CAccount A on (C.[BillToAccountID] = A.[ID])
	LEFT  JOIN QSPCanadaCommon.dbo.Address AdShip on (A.AddressListID = AdShip.AddressListID AND AdShip.Address_Type = 54001) --Ship To
	LEFT  JOIN QSPCanadaCommon.dbo.Address AdBill on (A.AddressListID = AdBill.AddressListID AND AdBill.Address_Type = 54002) --Use ship to for both.  Bill To=54002
	LEFT  JOIN QSPCanadaCommon.dbo.Phone P on (A.PhoneListID = P.PhoneListID and P.Type=30505)--Main
	LEFT  JOIN QSPCanadaCommon.dbo.Phone BillContactPhone on (BillCO.PhoneListID = BillContactPhone.PhoneListID and BillContactPhone.Type=30501)--Main
	LEFT  JOIN QSPCanadaCommon.dbo.Phone ShipContactPhone on (ShipCO.PhoneListID = ShipContactPhone.PhoneListID and ShipContactPhone.Type=30501)--Main
	LEFT  JOIN qspcanadacommon.dbo.CompanyInfo Qsp on qsp.CompanyID = 1 -- qsp toronto
	LEFT  JOIN qspcanadacommon.dbo.CompanyInfo QspFR on QspFR.CompanyID = 4 -- qsp quebec new address
	LEFT  JOIN QSPCanadaCommon.dbo.CompanyInfo courier on  courier.CompanyID = 3 -- resolve
WHERE 
	C.[ID] = @ICampaignID

SET NOCOUNT OFF
GO
