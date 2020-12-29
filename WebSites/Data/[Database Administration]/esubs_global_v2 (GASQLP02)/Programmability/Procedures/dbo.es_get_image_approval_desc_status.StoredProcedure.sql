USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_image_approval_desc_status]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jason Farrell
-- Create date: Sept 10 2010
-- Description:	Get image_approval_status_description
-- =============================================
Create PROCEDURE [dbo].[es_get_image_approval_desc_status]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT image_approval_status_description
			,image_approval_status_id
	FROM dbo.image_approval_status with (nolock)
END
GO
