USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[wfc_closing_delay]    Script Date: 02/14/2014 13:09:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[wfc_closing_delay] 
		@VarBeginDate as varchar(100)
		, @VarEndDate as varchar(100)
		, @intConsultantID INT = NULL
as 

declare @dteBeginDate as datetime
declare @dteEndDate as datetime

set @dteBeginDate = cast(@VarBeginDate as datetime)
set @dteEndDate = cast (@VarEndDate as datetime)
	

DECLARE @intTotalNumberLeads INT
SET NOCOUNT ON

IF @dteEndDate IS NULL
	SET @dteEndDate = GETDATE()

IF @intConsultantID IS NULL
BEGIN
	SELECT 
		COUNT( Lead_ID ) AS Nb_Leads
		, Consultant_ID
	INTO 	#wfc
	FROM 	dbo.Lead
	WHERE
		Promotion_ID = 672 
	 AND	Channel_Code  = 'CI' 
	 AND	Lead_Entry_Date BETWEEN CONVERT( DATETIME, @dteBeginDate, 110 ) AND CONVERT( DATETIME, @dteEndDate, 110 )
	GROUP BY 
		Consultant_ID

	SET @intTotalNumberLeads = (
		SELECT 
			SUM( Nb_Leads ) AS Total_Nb_Leads
		FROM	#wfc
	)

	SELECT
		wc.Nb_Leads
		, @intTotalNumberLeads AS Total_Nb_Leads
		, COUNT( s.Confirmed_Date ) AS Nb_Confirmed
		, co.Consultant_ID
		, co.[Name]
		, CASE 
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) IS NULL THEN 'Not Closed'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 10 THEN '0-10 Days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 10 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 20 THEN '11-20 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 20 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 30 THEN '22-30 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 30 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 40 THEN '31-40 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 40 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 50 THEN '41-50 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 50 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 60 THEN '51-60 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 60 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 70 THEN '61-70 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 70 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 80 THEN '71-80 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 80 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 90 THEN '81-90 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 90 THEN '90+ days'
		  END AS ClosingDelay 
	FROM 	
		dbo.Lead l
		LEFT OUTER JOIN #wfc wc
			ON wc.Consultant_ID = l.Consultant_ID
		LEFT OUTER JOIN dbo.Consultant co 
			ON l.Consultant_ID = co.Consultant_ID
		LEFT OUTER JOIN dbo.Client c 
			ON l.Lead_ID = c.Lead_ID 
		LEFT OUTER JOIN dbo.Sale s 
			ON c.Client_ID = s.Client_ID
			 AND c.Client_Sequence_Code = s.Client_Sequence_Code
	WHERE 
		l.Promotion_ID = 672
	 AND	l.Channel_Code = 'CI'
	 AND 	co.Is_Fm = 0
	 AND	l.Lead_Entry_Date BETWEEN CONVERT( DATETIME, @dteBeginDate, 110 ) AND CONVERT( DATETIME, @dteEndDate, 110 )
	GROUP BY 
		wc.Nb_Leads
		, co.consultant_ID
		, co.[Name]
		, CASE 
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) IS NULL THEN 'Not Closed'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 10 THEN '0-10 Days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 10 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 20 THEN '11-20 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 20 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 30 THEN '22-30 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 30 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 40 THEN '31-40 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 40 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 50 THEN '41-50 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 50 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 60 THEN '51-60 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 60 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 70 THEN '61-70 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 70 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 80 THEN '71-80 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 80 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 90 THEN '81-90 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 90 THEN '90+ days'
		  END 

	DROP TABLE #wfc

END
ELSE
BEGIN
	SELECT 
		COUNT( Lead_ID ) AS Nb_Leads
		, Consultant_ID
	INTO 	#wfcc
	FROM 	dbo.Lead
	WHERE
		Promotion_ID = 672 
	 AND	Channel_Code  = 'CI' 
	 AND	Consultant_ID = @intConsultantID
	 AND	Lead_Entry_Date BETWEEN CONVERT( DATETIME, @dteBeginDate, 110 ) AND CONVERT( DATETIME, @dteEndDate, 110 )
	GROUP BY 
		Consultant_ID

	SET @intTotalNumberLeads = (
		SELECT 
			SUM( Nb_Leads ) AS Total_Nb_Leads
		FROM	#wfcc
	)

	SELECT
		wc.Nb_Leads
		, @intTotalNumberLeads AS Total_Nb_Leads
		, COUNT( s.Confirmed_Date ) AS Nb_Confirmed
		, co.Consultant_ID
		, co.[Name]
		, CASE 
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) IS NULL THEN 'Not Closed'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 10 THEN '0-10 Days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 10 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 20 THEN '11-20 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 20 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 30 THEN '22-30 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 30 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 40 THEN '31-40 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 40 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 50 THEN '41-50 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 50 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 60 THEN '51-60 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 60 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 70 THEN '61-70 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 70 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 80 THEN '71-80 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 80 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 90 THEN '81-90 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 90 THEN '90+ days'
		  END AS ClosingDelay 
	FROM 	
		dbo.Lead l
		LEFT OUTER JOIN #wfcc wc 
			ON wc.Consultant_ID = l.Consultant_ID
		LEFT OUTER JOIN dbo.Consultant co 
			ON l.Consultant_ID = co.Consultant_ID
		LEFT OUTER JOIN dbo.Client c 
			ON l.Lead_ID = c.Lead_ID 
		LEFT OUTER JOIN dbo.Sale s 
			ON c.Client_ID = s.Client_ID
			 AND c.Client_Sequence_Code = s.Client_Sequence_Code
	WHERE 
		l.Promotion_ID = 672
	 AND	l.Channel_Code = 'CI'
	 AND 	co.Is_Fm = 0
	 AND	co.Consultant_ID = @intConsultantID
	 AND	l.Lead_Entry_Date BETWEEN CONVERT( DATETIME, @dteBeginDate, 110 ) AND CONVERT( DATETIME, @dteEndDate, 110 )
	GROUP BY 
		wc.Nb_Leads
		, co.consultant_ID
		, co.[Name]
		, CASE 
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) IS NULL THEN 'Not Closed'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 10 THEN '0-10 Days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 10 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 20 THEN '11-20 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 20 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 30 THEN '22-30 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 30 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 40 THEN '31-40 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 40 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 50 THEN '41-50 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 50 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 60 THEN '51-60 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 60 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 70 THEN '61-70 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 70 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 80 THEN '71-80 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 80 AND DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) <= 90 THEN '81-90 days'
			WHEN DATEDIFF( d, l.Lead_Entry_Date, s.Confirmed_Date ) > 90 THEN '90+ days'
		  END 

	DROP TABLE #wfcc

END
GO
