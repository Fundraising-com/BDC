USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_saleitems_by_itemid]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_store_get_saleitems_by_itemid]
	-- Add the parameters for the stored procedure here
	@itemid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   select * from saleitems where itemid=@itemid and status=1

END
GO
