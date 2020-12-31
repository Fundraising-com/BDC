USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_Batch_Contact_Info]    Script Date: 06/07/2017 09:20:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_upd_Batch_Contact_Info] 
 @OrderID int ,
 @Status int out
AS

-- Author   : Joshua Caesar (jcaesar@rd.com)
-- Date : November 15, 2004
-- Purpose  : mess with a Batch record to fix its contact info
--		according to the account and campaign info on it .

DECLARE 
	  @Id int
	, @AccountID int
	, @CampaignID int
	, @fname varchar(50)
	, @lname varchar(50)
	, @email varchar(50)
	, @phone varchar(50)
select 
	  @Id                  = [ID]
	, @AccountID    = [AccountID]
	, @CampaignID =  [CampaignID]
  from 
	QSPCanadaOrderManagement.dbo.Batch
 where 
	OrderID = @OrderID

--clear items
SELECT @fname = '', @lname = '', @email = '', @phone = ''

--GET THE INFO
select 
	top 1
	  @fname = Contact.[FirstName]
	, @lname = Contact.[LastName]
	, @email = Contact.[Email]
	, @phone = PH.[PhoneNumber]
 from 
	QSPCanadaCommon.dbo.Campaign Campaign
	LEFT JOIN QSPCanadaCommon.dbo.Contact Contact
	ON Campaign.ShipToCampaignContactID = Contact.[Id]
	LEFT JOIN QSPCanadaCommon.dbo.Phone PH
	ON (
		Contact.[PhoneListID] = PH.[PhoneListID] 
		AND PH.[Type] <> 30503 --not fax
		AND PH.Type is not null
	)
where 
	Contact.[CAccountID] = @AccountID
	and Campaign.id      = @CampaignID
order by 
	  Contact.[ID] desc
	, PH.Type ASC

if(@@ROWCOUNT > 0)
begin
	--print it
	print 
		'AccountID: '       + cast(@AccountId as varchar) 
		+ ' : CampaignID: ' + cast(@CampaignID as varchar)
		+ ' : @OrderID: '   + cast(@OrderID as varchar) 
		+ ' : @Id: '        + cast(@Id as varchar) 
		+ ' : @fname: '     + cast(isnull(@fname, '') as varchar) 
		+ ' : @lname: '     + cast(isnull(@lname, '') as varchar) 
		+ ' : @email: '     + cast(isnull(@email, '') as varchar) 
		+ ' : @phone: '     + cast(isnull(@phone, '') as varchar) 

	--run it
	UPDATE QSPCanadaOrderManagement.dbo.Batch
	   SET 
		  ContactFirstName = @fname
		, ContactLastName  = @lname
		, ContactEmail     = @email
		, ContactPhone     = @phone
	where
		[ID] = @Id
		AND [OrderID]    = @OrderID
		AND [AccountID]  = @AccountId
		AND [CampaignID] = @CampaignID

	select @Status = 1
end
else
begin
	--print it, no row, so no update 
	print 
		'BAD AccountID: '       + cast(@AccountId as varchar) 
		+ ' : CampaignID: ' + cast(@CampaignID as varchar)
		+ ' : @OrderID: '   + cast(@OrderID as varchar) 
		+ ' : @Id: '        + cast(@Id as varchar) 
		+ ' : @fname: '     + cast(isnull(@fname, '') as varchar) 
		+ ' : @lname: '     + cast(isnull(@lname, '') as varchar) 
		+ ' : @email: '     + cast(isnull(@email, '') as varchar) 
		+ ' : @phone: '     + cast(isnull(@phone, '') as varchar) 

	select @Status = -1
end
GO
