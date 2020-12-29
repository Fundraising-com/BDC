USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_email_template_tag]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 9/13/2011
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_email_template_tag] 
	@email_template_id int,
    @product_offer_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [email_template_id]
		  ,[product_offer_id]
		  ,[tag_id]
	FROM   [esubs_global_v2].[dbo].[email_template_tag]
    WHERE  email_template_id = @email_template_id AND product_offer_id = @product_offer_id
END
GO
