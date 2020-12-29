USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_promotion]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 02/10/2014
-- Description:	Missing proc
-- =============================================
CREATE PROCEDURE [dbo].[efr_get_promotion]
 @promotion_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT     
	pr.promotion_id
	, pr.promotion_type_code
	, pr.promotion_destination_id
	, pr.promotion_name
	, pr.script_name
	, pr.active
	, pr.create_date
	, pr.cookie_content
	, pr.keyword
	, pr.is_displayable
	FROM promotion pr where pr.promotion_id = @promotion_id
END
GO
