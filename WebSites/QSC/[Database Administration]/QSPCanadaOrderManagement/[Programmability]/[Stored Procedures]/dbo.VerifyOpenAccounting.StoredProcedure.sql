USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[VerifyOpenAccounting]    Script Date: 06/07/2017 09:20:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[VerifyOpenAccounting]
	@dummyid int
as

	select   1 as updatestatus
GO
