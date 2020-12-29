USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_item_category_by_itemid]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efr_store_get_item_category_by_itemid]
	-- Add the parameters for the stored procedure here
@itemid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select i.itemid, c.categoryname, i.itemname, i.status FROM fastfundraising..ffitems i INNER JOIN fastfundraising..category c ON c.categoryid = i.categoryid WHERE i.itemid = @itemid

END
GO
