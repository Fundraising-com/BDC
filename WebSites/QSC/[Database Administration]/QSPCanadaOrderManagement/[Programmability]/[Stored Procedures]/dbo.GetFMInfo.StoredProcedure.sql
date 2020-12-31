USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetFMInfo]    Script Date: 06/07/2017 09:19:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetFMInfo]
	@FMID	varchar(4)

AS

select top 1
	FirstName,
	LastName
 from 
	QSPCanadaCommon.dbo.FieldManager FM
where	FMID = @FMID
GO
