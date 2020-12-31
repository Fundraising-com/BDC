USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_CAccount_VendorInfo]    Script Date: 06/07/2017 09:33:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE [dbo].[pr_upd_CAccount_VendorInfo]
	@CAccountID int,
	@VendorNumber varchar(30),
	@VendorSiteName varchar(15),
	@VendorPayGroup varchar(25),
	@UserIDModified UserID_UDDT
AS

UPDATE 
	[dbo].[CAccount]
SET 
	[VendorNumber]		= @VendorNumber,
	[VendorSiteName]	= @VendorSiteName,
	[VendorPayGroup]	= @VendorPayGroup,
	[UserIDModified]	= @UserIDModified,
	[DateUpdated]		= getdate()
WHERE 
	[Id] = @CAccountID
GO
