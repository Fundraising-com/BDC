USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllFulfillmentHouseContacts]    Script Date: 06/07/2017 09:18:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllFulfillmentHouseContacts]

	@iFulfillmentHouseID	int = 0

AS

DECLARE @sqlStatement nvarchar(4000)

SET @sqlStatement = 'SELECT	DISTINCT
		fhc.Instance,
		fhc.Ful_Nbr,
		fh.Ful_Name,
		coalesce(fhc.FirstName, '''') AS FirstName,
		coalesce(fhc.LastName, '''') AS LastName,
		coalesce(fhc.Title, '''') AS Title,
		coalesce(fhc.Email, '''') AS Email,
		coalesce(fhc.WorkPhone, '''') AS WorkPhone,
		coalesce(fhc.Fax, '''') AS Fax,
		coalesce(fhc.CustSvcContactQSPFirstName, '''') AS CustSvcContactQSPFirstName,
		coalesce(fhc.CustSvcContactQSPLastName, '''') AS CustSvcContactQSPLastName,
		coalesce(fhc.CustSvcContactQSPEmail, '''') AS CustSvcContactQSPEmail,
		coalesce(fhc.CustSvcContactQSPPhone, '''') AS CustSvcContactQSPPhone,
		CASE coalesce(fhcp.Product_Code, '''') WHEN '''' THEN ''Y'' ELSE ''N'' END AS IsMainContact

FROM		FULFILLMENT_HOUSE_CONTACTS fhc
LEFT JOIN	FulfillmentHouseContact_Product fhcp
			ON	fhcp.FulfillmentHouseContactID = fhc.Instance,
		FULFILLMENT_HOUSE fh

WHERE	fh.Ful_Nbr = fhc.Ful_Nbr '

if(@iFulfillmentHouseID <> 0)
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND fhc.Ful_Nbr = ' + convert(nvarchar, @iFulfillmentHouseID)
END

EXEC(@sqlStatement)
GO
