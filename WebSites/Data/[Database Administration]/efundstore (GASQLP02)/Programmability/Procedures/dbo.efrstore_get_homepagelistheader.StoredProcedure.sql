USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_homepagelistheader]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  jason Farrell
-- Create date: November 11, 2010
-- Description: Get catagory header info for special products section on hompage
-- =============================================
CREATE PROCEDURE [dbo].[efrstore_get_homepagelistheader]
-- Add the parameters for the stored procedure here

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT 
   image_url
   ,package_category_title
   ,package_category_desc
   ,package_category_id
   ,product_url
   
FROM efundstore.dbo.package_category with(nolock)
END
GO
