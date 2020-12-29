USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_insert_image_for_new_client]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Jason Farrell
-- Create date: April 23, 2010
-- Description: Updates advertising table with client image info
-- =============================================

CREATE procedure [dbo].[efrc_insert_image_for_new_client]
 @advertising_id int
 ,@picture_url image
 ,@image_type varchar(100)
as 
begin

update advertising 
 set picture_url = @picture_url,
        image_type = @image_type
where advertising_id = @advertising_id
end
GO
