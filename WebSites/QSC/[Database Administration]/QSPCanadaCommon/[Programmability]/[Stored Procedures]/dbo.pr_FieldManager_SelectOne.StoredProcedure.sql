USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_FieldManager_SelectOne]    Script Date: 06/07/2017 09:33:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_FieldManager_SelectOne]
		@sFMID varchar(4)
AS
SET NOCOUNT ON

SELECT 
	[FMID],
	[Status],
	[Country],
	[PhoneListID],
	[AddressListID],
	[FirstName],
	[LastName],
	coalesce([MiddleInitial], '') AS MiddleInitial,
	coalesce([Email], '') AS Email,
	coalesce([DMID], 0) AS DMID,
	[UserIDModified],
	[DateModified],
	coalesce([Comment], '') AS Comment,
	[DMIndicator],
	[Lang],
	[DeletedTF]
FROM [QSPCanadaCommon]..[FieldManager]
WHERE [FMID] = @sFMID
ORDER BY
	[LastName],
	[FirstName],
	[MiddleInitial],
	[FMID]
GO
