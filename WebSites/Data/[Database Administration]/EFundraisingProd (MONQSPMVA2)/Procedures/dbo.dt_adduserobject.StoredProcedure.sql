USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[dt_adduserobject]    Script Date: 02/14/2014 13:03:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
**	Add an object to the dtproperties table
*/
create procedure [dbo].[dt_adduserobject]
as
	set nocount on
	/*
	** Create the user object if it does not exist already
	*/
	begin transaction
		insert dbo.dtproperties (property) VALUES ('DtgSchemaOBJECT')
		update dbo.dtproperties set objectid=@@identity 
			where id=@@identity and property='DtgSchemaOBJECT'
	commit
	return @@identity
GO
