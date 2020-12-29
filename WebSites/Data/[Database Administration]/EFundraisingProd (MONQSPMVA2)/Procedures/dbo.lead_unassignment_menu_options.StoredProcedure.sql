USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[lead_unassignment_menu_options]    Script Date: 02/14/2014 13:08:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	March 18, 2004
Description:	This stored procedure returns several recordsets to fill combo boxes for the Leads 
		re-assignment tool menu options.  The selected menu options are passed to the 
		dbo.get_leads_to_be_reassigned stored procedure, which returns the leads that meet the passed
		criteria.  The dbo.leads_to_be_reassigned stored procedure will re-assignment the leads to a consultant.
*/
CREATE PROCEDURE [dbo].[lead_unassignment_menu_options]
AS
-- Return active consultants with the department they belong to
SELECT 
	c.Consultant_Id
	, c.[Name] + ' (' + d.Department_name + ')' AS Consultant_Name 
FROM
	Consultant c
	INNER JOIN Department d 
		ON c.Department_ID = d.Department_Id
WHERE 
	c.Is_Agent = 0
 AND 	c.Is_Fm = 0
 AND	c.Is_Active = 1
 AND	d.Department_Id = 7
 OR	d.Department_Id = 18
ORDER BY 
	c.[Name]
-- Returns all the applicable states
SELECT
	State_Code
	, State_Name
FROM 	State 
-- Returns the available coasts
SELECT DISTINCT
	Coast 
FROM 	view_list_state_code_coast
-- Returns all the available activities for a lead
SELECT 
	Lead_Activity_Type_Id
	, [Description] AS Lead_Activity_Type_Desc
FROM 	Lead_Activity_Type 
-- Returns all the available promotions
SELECT
	Promotion_Type_Code
	, [Description] AS Promotion_Type_Desc
FROM	Promotion_Type
-- Returns all the available group types
SELECT 
	Group_Type_ID
	, [Description] AS Group_Type_Desc
FROM Group_Type
-- Returns all the available channel codes
SELECT 
	Channel_Code
	, [Description] AS Channel_Desc 
FROM	lead_channel
GO
