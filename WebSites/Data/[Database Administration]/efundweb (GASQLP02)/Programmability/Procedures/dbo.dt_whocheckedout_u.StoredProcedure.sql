USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[dt_whocheckedout_u]    Script Date: 02/14/2014 13:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[dt_whocheckedout_u]
        @chObjectType  char(4),
        @vchObjectName nvarchar(255),
        @vchLoginName  nvarchar(255),
        @vchPassword   nvarchar(255)

as

	-- This procedure should no longer be called;  dt_whocheckedout should be called instead.
	-- Calls are forwarded to dt_whocheckedout to maintain backward compatibility.
	set nocount on
	exec dbo.dt_whocheckedout
		@chObjectType, 
		@vchObjectName,
		@vchLoginName, 
		@vchPassword
GO
