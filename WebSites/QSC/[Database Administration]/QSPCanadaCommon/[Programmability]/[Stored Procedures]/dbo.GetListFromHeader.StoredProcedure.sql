USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetListFromHeader]    Script Date: 06/07/2017 09:33:09 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetListFromHeader]
	@CodeHeaderInstance int
as
	select * from codedetail where codeheaderinstance=@CodeHeaderInstance
GO
