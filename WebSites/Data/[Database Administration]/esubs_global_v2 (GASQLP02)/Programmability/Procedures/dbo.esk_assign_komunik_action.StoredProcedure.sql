USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[esk_assign_komunik_action]    Script Date: 02/14/2014 13:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: July 26th, 2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[esk_assign_komunik_action]
AS
BEGIN
	SET NOCOUNT ON;
    
    CREATE TABLE #action (
        [touch_id] [varchar](255),
	    [project_id] [varchar](255) NULL,
	    [email_template_id] [varchar](255) NULL,
	    [date] [varchar](255) NULL,
	    [action_id] [varchar](255) NULL,
	    [action_desc] [varchar](255) NULL
    )
    
    /*
    INSERT INTO #action
    SELECT 
    */
    
END
GO
