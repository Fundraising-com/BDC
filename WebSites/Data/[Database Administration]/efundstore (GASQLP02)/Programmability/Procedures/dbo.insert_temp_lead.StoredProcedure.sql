USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[insert_temp_lead]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Jp Lahaie
Created On:	May 13, 2004
Description:	This stored procedure ?
*/
CREATE PROCEDURE [dbo].[insert_temp_lead]
	@intVisitorsLogID INT
	, @intTempLeadID INT = NULL
	, @intDivisionID INT = 1
	, @intPromotionID INT = NULL
	, @strChannelCode VARCHAR(4) = 'INT'
	, @intLeadStatusID INT = 1
	, @strSalutation VARCHAR(10) = NULL
	, @strFirstName VARCHAR (50) = NULL
	, @strLastName VARCHAR (50) = NULL
	, @strOrganizationName VARCHAR (100) = NULL
	, @strStreetAddress VARCHAR (100) = NULL
	, @strCity VARCHAR (50) = NULL
	, @strStateCode VARCHAR(10) = NULL
	, @strCountryCode VARCHAR(10) = NULL
	, @strZipCode VARCHAR(10) = NULL
	, @strDayPhone VARCHAR(20) = NULL
	, @strDayPhoneExt VARCHAR(10) = NULL
	, @strDayTimeCall VARCHAR(20) = NULL
	, @strEveningPhone VARCHAR(20) = NULL
	, @strEveningPhoneExt VARCHAR(10) = NULL
	, @strOtherPhone VARCHAR(20) = NULL
	, @strOtherPhoneExt VARCHAR(10) = NULL
	, @strFax VARCHAR(20) = NULL
	, @strEmail VARCHAR (50) = NULL
	, @intGroupTypeID INT = NULL
	, @intGroupSize INT = NULL
	, @intParticipantCount INT = 0
	, @intFundraisingGoal INT = 0
	, @bitDecisionMaker BIT = 0
	, @dteDecisionDate  DATETIME = NULL
	, @dteFundraiserStartDate DATETIME = NULL
	, @bitOnEmailList BIT = 0
	, @strComments VARCHAR(2000) = NULL
	, @intHearAboutUsID INT = 0
	, @intOrganizationTypeID INT = 99
	, @strCookieContent VARCHAR (255) = NULL
	, @strGroupWebsite VARCHAR (50) = NULL
	, @intTitleID INT = NULL
	, @CampaignReasonID INT = 99
	, @intWebsiteID INT = NULL
	, @intIsNew BIT = 1
AS
DECLARE @intErrorCode INT
SET @intErrorCode = @@ERROR

BEGIN TRANSACTION

IF @intErrorCode = 0
BEGIN
	INSERT INTO temp_lead (
		division_id
		, promotion_id
		, channel_code
		, lead_status_id
		, salutation
		, first_name
		, last_name
		, organization
		, street_address
		, city
		, state_code
		, country_code
		, zip_code
		, day_phone
		, day_phone_ext
		, day_time_call
		, evening_phone
		, evening_phone_ext
		, other_phone
		, other_phone_ext
		, fax
		, email
		, group_type_id
--		, member_count
		, participant_count
		, fund_raising_goal
		, decision_maker
		, decision_date
		, fund_raiser_start_date
		, onemaillist
		, comments
		, hear_id
		, organization_type_id
		, cookie_content
		, group_web_site
		, title_id
		, campaign_reason_id
		, web_site_id 
		, isnew
	) VALUES (
		@intDivisionID
		, @intPromotionID
		, @strChannelCode
		, @intLeadStatusID
		, @strSalutation
		, @strFirstName
		, @strLastName 
		, @strOrganizationName
		, @strStreetAddress
		, @strCity
		, @strStateCode
		, @strCountryCode
		, @strZipCode
		, @strDayPhone
		, @strDayPhoneExt
		, @strDayTimeCall
		, @strEveningPhone
		, @strEveningPhoneExt
		, @strOtherPhone
		, @strOtherPhoneExt
		, @strFax
		, @strEmail
		, @intGroupTypeID
--		, @intGroupSize
		, @intParticipantCount
		, @intFundraisingGoal
		, @bitDecisionMaker
		, @dteDecisionDate
		, @dteFundraiserStartDate
		, @bitOnEmailList
		, @strComments
		, @intHearAboutUsID
		, @intOrganizationTypeID
		, @strCookieContent
		, @strGroupWebsite
		, @intTitleID
		, @CampaignReasonID
		, @intWebsiteID
		, @intIsNew
	)
	SET @intErrorCode = @@ERROR
END

IF @intErrorCode = 0
BEGIN
	SET @intTempLeadID = @@IDENTITY
	EXEC @intErrorCode = web_tracking.dbo.identify_visitor @intVisitorsLogID, 4, @intTempLeadID
END

IF @intErrorCode = 0
BEGIN
	COMMIT TRANSACTION
	RETURN( @intTempLeadID )
END
ELSE
BEGIN
	ROLLBACK TRANSACTION
	RETURN( -1 )
END
GO
