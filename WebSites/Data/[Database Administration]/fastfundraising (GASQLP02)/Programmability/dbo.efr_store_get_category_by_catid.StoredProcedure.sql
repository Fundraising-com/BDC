USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_category_by_catid]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_store_get_category_by_catid]
	-- Add the parameters for the stored procedure here
	@catid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   select cat.categoryname from category cat  where status=1 and cat.categoryid=@catid

END
GO
