USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_item_by_itemid]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efr_store_get_item_by_itemid]
	-- Add the parameters for the stored procedure here
	@itemid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   select *, 'packingaaa ' as packing from ffitems where itemid=@itemid

END
GO
