USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_ffitemprice_ffitems_by_itemid]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_store_get_ffitemprice_ffitems_by_itemid]
	@itemid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ffitemprice.*, ffitems.categoryid from ffitemprice inner join ffitems on ffitems.itemid = ffitemprice.itemid where ffitemprice.itemid= @itemid order by tierid


END
GO
