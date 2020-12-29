USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[update_temp_lead]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Jp Lahaie
Created On:	May 25, 2004
Description:	This stored procedure updates the information for a temp_lead.
		The number of rows affected by the update is returned, if no 
		error occurs.
*/
CREATE PROCEDURE [dbo].[update_temp_lead]
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
UPDATE temp_lead 
SET	promotion_id = @intPromotionID
	, salutation = @strSalutation
	, first_name = @strFirstName
	, last_name = @strLastName
	, organization = @strOrganizationName
	, street_address = @strStreetAddress
	, city = @strCity
	, state_code = @strStateCode
	, country_code = @strCountryCode
	, Zip_Code = @strZipCode
	, day_phone = @strDayPhone
	, day_phone_ext = @strDayPhoneExt
	, day_time_call = @strDayTimeCall
	, evening_phone = @strEveningPhone
	, evening_phone_ext = @strEveningPhoneExt
	, other_phone = @strOtherPhone
	, other_phone_ext = @strOtherPhoneExt
	, fax = @strFax
	, email = @strEmail
	, group_type_id = @intGroupTypeID
	, participant_count = @intParticipantCount
	, fund_raising_goal = @intFundraisingGoal
	, decision_maker = @bitDecisionMaker
	, decision_date = @dteDecisionDate
	, fund_raiser_start_date = @dteFundraiserStartDate
	, onemaillist = @bitOnEmailList
	, comments = @strComments
	, hear_id = @intHearAboutUsID
	, organization_type_id = @intOrganizationTypeID
	, cookie_content = @strCookieContent
	, group_web_site = @strGroupWebsite
	, title_id = @intTitleID
	, campaign_reason_id = @CampaignReasonID
	, isnew = @intIsNew
WHERE
	temp_lead_id = @intTempLeadID

IF @@ERROR = 0
	RETURN( @@ROWCOUNT )
ELSE
	RETURN( -1 )
GO
