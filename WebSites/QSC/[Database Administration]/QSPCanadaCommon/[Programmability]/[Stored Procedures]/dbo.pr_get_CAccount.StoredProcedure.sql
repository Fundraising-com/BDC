USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_CAccount]    Script Date: 06/07/2017 09:33:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE    PROCEDURE [dbo].[pr_get_CAccount]
	@AccountID int
AS
/******************************************************************************************************************************************************
  *
  * Please make sure any changes made here have 
  * matching changes in the QSPFulfillment Business Layer,
  * Otherwise, Business.Account may break.
  * 
  * SEPT  2004 - JCAESAR - Added vendor fields.
  * SEPT  2004 - JCAESAR - Slight Update
  * APRIL 2004 - JCAESAR - Original Version
************************************************************************************************************************************************/

SELECT     
	[Id]                    As [AccountID], 
	[Name], 
	[StatusID], 
	Isnull(Sponsor, '')     As Sponsor,
	Country, 
	Lang, 
	CAccountCodeClass,
	CAccountCodeGroup,
	isnull(PhoneListID,0)   As PhoneListID,
	isnull(AddressListID,0) As AddressListID,	
	Isnull(Comment, '')     As Comment,
	Enrollment,
	isnull(EMail,'')        As Email,
	case upper(IsPrivateOrg)
		when 'Y' then cast(1 as bit)
		when 'N' then cast(0 as bit)
		else IsPrivateOrg
	end AS IsPrivateOrg,
	case upper(IsAdultGroup)
		when 'Y' then cast(1 as bit)
		when 'N' then cast(0 as bit)
		else IsAdultGroup
	end AS IsAdultGroup,
--	UserIDCreated,
--	ISNULL(DateCreated, '1995-01-01 00:00:00.000') AS DateCreated,
	ISNULL(DateUpdated, '1995-01-01 00:00:00.000') AS DateUpdated,
	UserIDModified,
	ISNULL(ParentID, 0)     As ParentID,
	VendorNumber,
	VendorSiteName,
	VendorPayGroup
FROM
	CAccount
WHERE 
	[Id] = @AccountID
GO
