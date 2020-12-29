USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_insert_advertising_listing]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Jason Farrell
-- Create date: may 4, 2010
-- Description: Insert client info into advertising
-- =============================================

CREATE procedure [dbo].[efrc_insert_advertising_listing]
 @listing_id as int
 ,@advertising_id as int
 ,@start_date Datetime
 ,@end_date Datetime
 
as 
begin

  INSERT INTO advertising_listing
  (
 listing_id
 ,advertising_id
 ,start_date
 ,end_date

  )
  VALUES
  (
    @listing_id 
 ,@advertising_id 
 ,@start_date
 ,@end_date
  )

 end
GO
