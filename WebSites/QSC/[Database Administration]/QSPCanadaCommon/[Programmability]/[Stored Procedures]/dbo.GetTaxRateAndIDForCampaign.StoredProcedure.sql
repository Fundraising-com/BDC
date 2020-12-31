USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetTaxRateAndIDForCampaign]    Script Date: 06/07/2017 09:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTaxRateAndIDForCampaign]
	@CampaignID 	 int,
	@SectionTypeID int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/10/2004 
--   Get Tax Rate and Tax ID For a Given Campaign For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT DISTINCT T.Tax_ID, Tax_Rate
FROM QSPCanadaCommon..CAccount A  
INNER JOIN QSPCanadaCommon..Campaign C ON A.ID = C.ShipToAccountId
INNER JOIN QSPCanadaCommon..AddressList AL on A.AddressListID = AL.ID
INNER JOIN QSPCanadaCommon..Address AdShip on AL.ID = AdShip.AddressListID 
	AND AdShip.Address_Type = 54001 --Ship To
INNER JOIN QSPCanadaCommon..TaxProvince TP on TP.Province_Code = AdShip.StateProvince 
	AND TP.Country_Code = AdShip.Country 
INNER JOIN QSPCanadaCommon..TaxApplicableTax AppTax on AppTax.Country_Code = TP.Country_Code 
	AND AppTax.Province_Code =TP.Province_Code
INNER JOIN QSPCanadaCommon..Tax T on T.Tax_ID = TP.TAX_ID and AppTax.Tax_ID = T.Tax_ID
WHERE C.ID = @CampaignID
	AND Section_Type_ID = @SectionTypeID

SET NOCOUNT OFF
GO
