USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_FieldManager_SelectAll]    Script Date: 06/07/2017 09:19:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_FieldManager_SelectAll] AS
SET NOCOUNT ON


SELECT 
	[FMID],
	[Status],
	[Country],
	[PhoneListID],
	[AddressListID],
	[FirstName],
	[LastName],
	[MiddleInitial],
	[Email],
	[DMID],
	[UserIDModified],
	[DateModified],
	[Comment],
	[DMIndicator],
	[Lang],
	[DeletedTF]
FROM [QSPCanadaCommon]..[FieldManager]
ORDER BY
	[LastName],
	[FirstName],
	[MiddleInitial],
	[FMID]
GO
