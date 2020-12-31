USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Account_updateAddress]    Script Date: 06/07/2017 09:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_Account_updateAddress]
 @MinAccountID int
,@MaxAccountID int
AS

UPDATE QSPCanadaOrderManagement.dbo.Account
SET
	  [Address1]	= ADDRESS.street1
	, [Address2]	= ADDRESS.street2
	, [City]	= ADDRESS.City 
	, [State]	= ADDRESS.stateProvince
	, [Zip]		= Replace(ADDRESS.postal_code, ' ', '') 
	, [ZipPlusFour]	= ADDRESS.Zip4

FROM 
	QSPCanadaOrderManagement.dbo.Account OMA
	inner join QSPCanadaCommon.dbo.CAccount CACCOUNT
		on OMA.Id = CACCOUNT.ID
	inner join QSPCanadaCommon.dbo.Address ADDRESS
		ON (CACCOUNT.AddressListID = ADDRESS.AddressListID AND ADDRESS.address_type = 54001)
where
	CACCOUNT.[Id] BETWEEN @MinAccountID and @MaxAccountID
	AND ADDRESS.address_type = 54001
GO
