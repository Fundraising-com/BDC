USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Address_UpdateForAddressHygiene]    Script Date: 06/07/2017 09:19:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Address_UpdateForAddressHygiene]

	@customerID		int,
	@address1		varchar(50),
	@address2		varchar(50) = null,
	@city			varchar(50),
	@county			varchar(31) = null,
	@region			varchar(10),
	@postCode		varchar(10),
	@postCode2		varchar(4) = null

AS

UPDATE	Customer
SET		Address1 = CASE @address1 WHEN '' THEN null ELSE @address1 END,
		Address2 = CASE @address2 WHEN '' THEN null ELSE @address2 END,
		City = CASE @city WHEN '' THEN null ELSE @city END,
		County = CASE @county WHEN '' THEN null ELSE @county END,
		State = CASE @region WHEN '' THEN null ELSE @region END,
		Zip = CASE @postCode WHEN '' THEN null ELSE @postCode END,
		ZipPlusFour = CASE @postCode2 WHEN '' THEN null ELSE @postCode2 END,
		StatusInstance = 300,
		ChangeUserID = 'ADDH',
		ChangeDate = GETDATE()
WHERE	Instance = @customerID


SET ANSI_NULLS ON
GO
