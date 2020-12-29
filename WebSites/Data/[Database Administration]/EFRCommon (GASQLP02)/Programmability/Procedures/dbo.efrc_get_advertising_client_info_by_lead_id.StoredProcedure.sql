USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_advertising_client_info_by_lead_id]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jason Farrell
-- Create date: April 28 2010
-- Description:	Get client info for advertising admin page
-- =============================================
CREATE PROCEDURE [dbo].[efrc_get_advertising_client_info_by_lead_id]
	-- Add the parameters for the stored procedure here
	@lead_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT advertising_id, first_name ,last_name ,compagnie_name ,phone	,email	,listing_text ,compagnie_url, display_url,advertising_type_id ,picture_url, start_date, end_date, image_type 
		from dbo.advertising
	where lead_id = @lead_id

END
GO
