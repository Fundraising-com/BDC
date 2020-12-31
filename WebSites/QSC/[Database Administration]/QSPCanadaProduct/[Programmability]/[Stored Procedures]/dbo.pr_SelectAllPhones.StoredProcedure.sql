USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllPhones]    Script Date: 06/07/2017 09:18:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllPhones]

	@iPhoneListID	int = 0

AS

declare @sqlStatement nvarchar(4000)

set @sqlStatement = 'SELECT	p.ID,
		p.Type + 30500 AS Type,
		p.PhoneListID,
		p.PhoneNumber,
		coalesce(p.BestTimeToCall, '''') AS BestTimeToCall

FROM		Phone p '

--if(@iPhoneListID <> 0)
--BEGIN
	set @sqlStatement = @sqlStatement + ' WHERE p.PhoneListID = ' + convert(varchar, @iPhoneListID)
--END


exec(@sqlStatement)
GO
