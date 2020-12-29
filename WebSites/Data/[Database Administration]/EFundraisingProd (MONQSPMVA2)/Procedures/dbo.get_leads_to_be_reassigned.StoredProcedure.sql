USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[get_leads_to_be_reassigned]    Script Date: 02/14/2014 13:08:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
Created By:	Paolo De Rosa
Created On:	March 18, 2004
Description:	This stored procedure returns a recordset of leads that meet the passed criteria.  The 
		dbo.leads_to_be_reassigned stored procedure will re-assignment the leads to a consultant or NULL.
*/
CREATE PROCEDURE [dbo].[get_leads_to_be_reassigned]
	@intConsultantID INT = NULL
	, @strStateCode VARCHAR(10) = NULL
	, @strCoast CHAR(10) = NULL
	, @intActivityTypeID INT = NULL
	, @strPromotionCode VARCHAR(4) = NULL
	, @intGroupTypeID INT = NULL
	, @strChannelCode VARCHAR(4) = NULL
	, @dteActivityBeginDate DATETIME = NULL
	, @dteActivityEndDate DATETIME = NULL
	, @intActivityNumber INT = NULL
	, @intDaysLate INT = NULL
	, @dteLeadEntryBeginDate DATETIME = NULL
	, @dteLeadEntryEndDate DATETIME = NULL
	, @strLeadIDs VARCHAR(200) = NULL
	, @blnBeenContacted BIT = NULL
	, @blnIsClient BIT = NULL
	, @blnIsCompleted BIT = NULL
	, @intReturn INT = NULL
AS
DECLARE @strSQL VARCHAR(8000)
IF @intReturn IS NOT NULL
	SET @strSQL = 'SELECT DISTINCT TOP ' + CONVERT( VARCHAR(4), @intReturn )
ELSE
	SET @strSQL = 'SELECT DISTINCT'
SET @strSQL = @strSQL + '
	l.Lead_ID
	, lc.[Description] AS Channel_Desc 
	, c.[Name] AS Consultant_Name
	, ls.[Description] AS Lead_Status_Desc
	, sc.State_Code + '' ('' + RTRIM( s.Country_Code ) + '')'' AS State_Code
	, lat.[Description] AS Lead_Activity_Type_Desc
	, pt.[Description] AS Promotion_Type_Desc
	, p.[Description] AS Promotion_Desc
	, ISNULL( l.Participant_Count, 0 ) AS Participant_Count
	, w.Web_Site_Name 
	, gt.[Description] AS Group_Type_Desc
	, ot.Organization_Type_Desc
FROM	dbo.Lead l
	INNER JOIN dbo.Lead_Status ls
		ON l.Lead_Status_ID = ls.Lead_Status_ID 
	INNER JOIN dbo.state s
		ON l.State_Code = s.State_Code 
	INNER JOIN dbo.view_list_state_code_coast sc
		ON l.State_Code = sc.State_Code 
	INNER JOIN dbo.Consultant c
		ON l.Consultant_Id = c.Consultant_Id
	LEFT OUTER JOIN Lead_Activity la
		ON l.Lead_ID = la.Lead_ID
	INNER JOIN Lead_Activity_Type lat
		ON la.Lead_Activity_Type_ID = lat.Lead_Activity_Type_ID
	INNER JOIN Lead_Channel lc
		ON l.Channel_Code = lc.Channel_Code
	INNER JOIN Promotion p
		ON l.Promotion_ID = p.Promotion_ID
	INNER JOIN Promotion_Type pt
		ON p.Promotion_Type_Code = pt.Promotion_Type_Code
	INNER JOIN Group_Type gt
		ON l.Group_Type_ID = gt.Group_Type_ID 
	INNER JOIN Organization_Type ot
		ON l.Organization_Type_ID = ot.Organization_Type_ID 
	INNER JOIN Web_Site w
		ON l.Web_Site_ID = w.Web_Site_ID
	INNER JOIN (
		SELECT 
			Lead_ID
			, MAX( lead_activity_date ) AS LatestActivityDate 
		FROM 	Lead_Activity 
		GROUP BY 
			Lead_ID 
	) mat
		ON l.lead_id = mat.lead_id
		 AND la.Lead_Activity_Date = mat.LatestActivityDate
	LEFT OUTER JOIN (
		SELECT 
			Lead_ID
			, COUNT( lead_activity_id ) AS ActivityCount 
		FROM 	Lead_Activity 
		GROUP BY 
			Lead_ID 
	) lac
		ON l.lead_id = lac.lead_id 
WHERE 1 = 1'
IF @intConsultantID IS NOT NULL
	SET @strSQL = @strSQL + ' AND c.Consultant_ID = ' + CONVERT( VARCHAR(6), @intConsultantID )
IF @strStateCode IS NOT NULL
	SET @strSQL = @strSQL + ' AND l.State_Code = ''' + @strStateCode + ''''
IF @strCoast IS NOT NULL
	SET @strSQL = @strSQL + ' AND sc.Coast = ''' + @strCoast + ''''
IF @intActivityTypeID IS NOT NULL
	SET @strSQL = @strSQL + ' AND la.Lead_Activity_Type_ID = ' + CONVERT( VARCHAR(3), @intActivityTypeID )
IF @strPromotionCode IS NOT NULL
	SET @strSQL = @strSQL + ' AND p.Promotion_Type_Code = ''' + @strPromotionCode + ''''
IF @intGroupTypeID IS NOT NULL
	SET @strSQL = @strSQL + ' AND l.Group_Type_ID = ' + CONVERT( VARCHAR(3), @intGroupTypeID )
IF @strChannelCode IS NOT NULL
	SET @strSQL = @strSQL + ' AND l.Channel_Code = ''' + @strChannelCode + ''''
IF ( @dteActivityBeginDate IS NOT NULL ) AND ( @dteActivityEndDate IS NOT NULL )
	SET @strSQL = @strSQL + ' AND la.Lead_Activity_Date BETWEEN ''' + CONVERT( VARCHAR(23), @dteActivityBeginDate ) + ''' AND ''' + CONVERT( VARCHAR(23), @dteActivityEndDate ) + ''''
ELSE 
	IF ( @dteActivityBeginDate IS NOT NULL ) AND ( @dteActivityEndDate IS NULL )
        	SET @strSQL = @strSQL + ' AND la.Lead_Activity_Date >= ''' + CONVERT( VARCHAR(23), @dteActivityBeginDate ) + ''''
	ELSE 
		IF ( @dteActivityBeginDate IS NULL ) AND ( @dteActivityEndDate IS NOT NULL )
			SET @strSQL = @strSQL + ' AND la.Lead_Activity_Date <= ''' + CONVERT( VARCHAR(23), @dteActivityEndDate ) + ''''
/*
IF @dteActivityDate IS NOT NULL
	SET @strSQL = @strSQL + ' AND CONVERT( CHAR(10), la.Lead_Activity_Date, 110 ) = ''' + CONVERT( CHAR(10), @dteActivityDate , 110 ) + ''''
*/
IF @intActivityNumber IS NOT NULL
	SET @strSQL = @strSQL + ' AND ISNULL( lac.ActivityCount, 0 ) = ' + CONVERT( VARCHAR(3), @intActivityNumber )
IF @intDaysLate IS NOT NULL
	SET @strSQL = @strSQL + ' AND DATEDIFF( d, la.Lead_Activity_Date, GETDATE() ) = ' + CONVERT( VARCHAR(3), @intDaysLate ) + ' AND la.Completed_Date IS NULL'
IF ( @dteLeadEntryBeginDate IS NOT NULL ) AND ( @dteLeadEntryEndDate IS NOT NULL )
	SET @strSQL = @strSQL + ' AND l.Lead_Entry_Date BETWEEN ''' + CONVERT( VARCHAR(23), @dteLeadEntryBeginDate ) + ''' AND ''' + CONVERT( VARCHAR(23), @dteLeadEntryEndDate ) + ''''
ELSE 
	IF ( @dteLeadEntryBeginDate IS NOT NULL ) AND ( @dteLeadEntryEndDate IS NULL )
        	SET @strSQL = @strSQL + ' AND l.Lead_Entry_Date >= ''' + CONVERT( VARCHAR(23), @dteLeadEntryBeginDate ) + ''''
	ELSE 
		IF ( @dteLeadEntryBeginDate IS NULL ) AND ( @dteLeadEntryEndDate IS NOT NULL )
			SET @strSQL = @strSQL + ' AND l.Lead_Entry_Date <= ''' + CONVERT( VARCHAR(23), @dteLeadEntryEndDate ) + ''''
/*
IF @dteLeadEntryDate IS NOT NULL
	SET @strSQL = @strSQL + ' AND CONVERT( CHAR(10), l.Lead_Entry_Date, 110 ) = ''' + CONVERT( CHAR(10), @dteLeadEntryDate, 110 ) + ''''
*/
IF @strLeadIDs IS NOT NULL
	SET @strSQL = @strSQL + ' AND l.Lead_ID IN ( ' + @strLeadIDs + ' )'
IF @blnBeenContacted IS NOT NULL
	SET @strSQL = @strSQL + ' AND l.Has_Been_Contacted = 1'
IF @blnIsClient IS NOT NULL
	SET @strSQL = @strSQL + ' AND l.Lead_ID IN ( SELECT Lead_ID FROM Client )'
IF @blnIsCompleted IS NOT NULL
	SET @strSQL = @strSQL + ' AND la.Completed_Date IS NOT NULL'
SET @strSQL = @strSQL + ' ORDER BY c.[Name] ASC, s.State_Code ASC'

EXEC( @strSQL )
--PRINT @strSQL
GO
