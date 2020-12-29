USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_max_tier_from_ffitemprice_by_itemid_qty]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_store_get_max_tier_from_ffitemprice_by_itemid_qty]
	@itemid int,
	@qty int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  	select max(tierid) tcode from ffitemprice where itemid=@itemid and tierminval <= @qty

END
GO
