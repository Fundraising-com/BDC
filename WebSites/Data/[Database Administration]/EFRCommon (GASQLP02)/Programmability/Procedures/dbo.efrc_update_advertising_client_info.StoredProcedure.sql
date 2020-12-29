USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_update_advertising_client_info]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jason Farrell
-- Create date: April 28, 2010
-- Description:	Update client info and start date/end date for advertising page
-- =============================================
CREATE PROCEDURE [dbo].[efrc_update_advertising_client_info] 
	-- Add the parameters for the stored procedure here
	@lead_id as int 
	,@advertising_type_id as int
	,@first_name as varchar(20)
	,@last_name as varchar(20)
	,@phone as varchar(20)
	,@email as varchar (50)
	,@compagnie_name as varchar(45)
	,@compagnie_url as varchar(150)
	,@display_url as varchar(100)
	,@listing_text as varchar(355)
	,@is_visible as varchar(20)
	,@start_date as Datetime
	,@end_date as Datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update dbo.advertising 
		set advertising_type_id=@advertising_type_id
		,first_name=@first_name
		,last_name=@last_name
		,phone=@phone
		,email=@email
		,compagnie_name=@compagnie_name
		,compagnie_url=@compagnie_url
		,display_url=@display_url
		,listing_text=@listing_text
		,is_visible=@is_visible
		,start_date=@start_date
		,end_date=@end_date
	where lead_id=@lead_id


END
GO
