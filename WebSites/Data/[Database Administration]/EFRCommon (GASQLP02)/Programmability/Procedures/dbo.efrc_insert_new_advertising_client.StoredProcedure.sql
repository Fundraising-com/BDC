USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_insert_new_advertising_client]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Farrell
-- Create date: April 23, 2010
-- Description:	Insert client info into advertising
-- =============================================

CREATE procedure [dbo].[efrc_insert_new_advertising_client]
@advertising_id int Output	
	,@lead_id as int
	,@org_promotion_id as int
	,@advertising_type_id as int
	,@first_name as varchar(20)
	,@last_name as varchar(20)
	,@phone as varchar(20)
	,@email as varchar (50)
	,@compagnie_name as varchar(45)
	,@compagnie_url as varchar(150)
	,@display_url as varchar(100)
	,@listing_text as varchar(355)
	,@image_type varchar(100)
	,@is_visible as varchar(20)
	,@start_date as datetime
	,@end_date as datetime

as 
begin

  INSERT INTO advertising
  (
	lead_id
	,org_promotion_id 
	,advertising_type_id
	,first_name
	,last_name
	,phone
	,email
	,compagnie_name
	,compagnie_url
	,display_url
	,listing_text
	,image_type
	,is_visible
	,start_date
	,end_date

  )
  VALUES
  (
   	@lead_id
	,@org_promotion_id
	,@advertising_type_id
	,@first_name
	,@last_name
	,@phone
	,@email
	,@compagnie_name
	,@compagnie_url
	,@display_url
	,@listing_text
	,@image_type
	,@is_visible
	,@start_date
	,@end_date
  )

select @advertising_id = SCOPE_IDENTITY()

 end
GO
