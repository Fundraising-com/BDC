USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_advertising_information_for_webpage]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Farrell
-- Create date: May 5, 2010
-- Description:	Get Information for Product and Services pages
-- =============================================
CREATE PROCEDURE [dbo].[efrc_get_advertising_information_for_webpage]
	-- Add the parameters for the stored procedure here
	@advertising_type_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT lead_id, compagnie_name, compagnie_url, display_url, listing_text, picture_url, image_type, start_date, end_date
		from advertising
	where advertising_type_id = @advertising_type_id 
		AND is_visible = '1' 
		AND start_date <= GETDATE()
        AND end_date >= GETDATE()
		order by newid()

END
GO
