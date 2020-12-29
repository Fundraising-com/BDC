USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_saleitemprice_by_itemid_saleid]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_store_get_saleitemprice_by_itemid_saleid]
	@itemid int,
	@saleid int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from saleitemprice where itemid=@itemid and saleid=@saleid order by tierid

END
GO
