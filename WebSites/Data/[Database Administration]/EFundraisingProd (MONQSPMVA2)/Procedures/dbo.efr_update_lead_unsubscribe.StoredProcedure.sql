USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_update_lead_unsubscribe]    Script Date: 02/14/2014 13:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[efr_update_lead_unsubscribe] 
        @email as varchar(100)
	, @unsubscribe as bit
	, @valid as bit

as

if @valid = 0 
begin
	update lead set valid_email = 0 where email = @email
end

if @unsubscribe = 1
begin
	update lead set onemaillist = 0 where email = @email
	update newsLetter set unsubscribed = 1 where email = @email
end
GO
