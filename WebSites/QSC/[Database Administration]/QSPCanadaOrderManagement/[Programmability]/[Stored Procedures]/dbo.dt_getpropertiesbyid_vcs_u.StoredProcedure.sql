USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[dt_getpropertiesbyid_vcs_u]    Script Date: 06/07/2017 09:19:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[dt_getpropertiesbyid_vcs_u]
    @id       int,
    @property varchar(64),
    @value    nvarchar(255) = NULL OUT

as

    -- This procedure should no longer be called;  dt_getpropertiesbyid_vcsshould be called instead.
	-- Calls are forwarded to dt_getpropertiesbyid_vcs to maintain backward compatibility.
	set nocount on
    exec dbo.dt_getpropertiesbyid_vcs
		@id,
		@property,
		@value output
GO
