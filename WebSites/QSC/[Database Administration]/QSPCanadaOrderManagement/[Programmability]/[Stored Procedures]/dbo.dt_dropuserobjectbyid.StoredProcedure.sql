USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[dt_dropuserobjectbyid]    Script Date: 06/07/2017 09:19:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
**	Drop an object from the dbo.dtproperties table
*/
create procedure [dbo].[dt_dropuserobjectbyid]
	@id int
as
	set nocount on
	delete from dbo.dtproperties where objectid=@id
GO
