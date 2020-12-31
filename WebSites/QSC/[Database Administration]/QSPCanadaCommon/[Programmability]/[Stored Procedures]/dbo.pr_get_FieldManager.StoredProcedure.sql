USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_FieldManager]    Script Date: 06/07/2017 09:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_FieldManager]
	@FMID varchar(4)
AS

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
  FROM 
	[QSPCanadaCommon].[dbo].[FieldManager]
 WHERE
	[FMID] = @FMID
GO
