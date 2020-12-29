USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_image_by_id]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Jason Farrell
-- Create date: April 28 2010
-- Description: Get image from db and display on webpage
-- =============================================
CREATE PROCEDURE [dbo].[efrc_get_image_by_id]
 -- Add the parameters for the stored procedure here
 @advertising_id int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;
 
    -- Insert statements for procedure here
 SELECT picture_url, image_type 
  from dbo.advertising
 where advertising_id = @advertising_id
END
GO
