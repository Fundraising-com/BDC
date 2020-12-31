USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Account_Update]    Script Date: 06/07/2017 09:19:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Account_Update]
 @MinAccountID int
,@MaxAccountID int

AS

UPDATE	a
SET		a.[Name] = CACCOUNT.[Name],
		a.[Address1] = ADDRESS.street1,
		a.[Address2] = ADDRESS.street2,
		a.[City] = ADDRESS.City,
		a.[State] = ADDRESS.stateProvince,
		a.[Zip] = Replace(ADDRESS.postal_code, ' ', ''),
		a.[ZipPlusFour] = ADDRESS.Zip4

FROM		QSPCanadaOrderManagement.dbo.Account a,
		QSPCanadaCommon.dbo.CAccount CACCOUNT
		left join QSPCanadaCommon.dbo.Address ADDRESS
			ON (CACCOUNT.AddressListID = ADDRESS.AddressListID AND ADDRESS.address_type = 54001)

WHERE	a.ID = CACCOUNT.[Id]
AND		CACCOUNT.[Id] BETWEEN @MinAccountID AND @MaxAccountID
AND		ADDRESS.address_type = 54001
GO
