USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_call_qspecommerce_pr_SchoolLookup]    Script Date: 02/14/2014 13:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_call_qspecommerce_pr_SchoolLookup]
	 @school varchar(100),	
	 @state char(2),
	-- @country int,  -- 1 - US ,  2 - Canada
	 @iLanguage_Id int  --1: English, 2:French

AS

exec QSPEcommerce.dbo.pr_SchoolLookup @school, @state, @iLanguage_Id
GO
