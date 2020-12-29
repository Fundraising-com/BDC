USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_featured_event_for_mainpage]    Script Date: 02/14/2014 13:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2006/03/01
-- Description:	Fetch all the data for the 
--              featured group page
--
-- Ex: EXEC [dbo].[es_rpt_featured_event]
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_featured_event_for_mainpage]
AS
BEGIN
	
    select event_id
        , event_name
        , state
        , nb_member
        , nb_sub
        , amount
    from featured_event_mainpage

END
GO
