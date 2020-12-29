USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_sum_qty_cost_from_ffcart_by_cookieid]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_store_get_sum_qty_cost_from_ffcart_by_cookieid]
	@cookieid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select coalesce(sum(qty),0) as totalitems, cast(coalesce(sum(totalcost),0) as decimal(6,2)) as totalamount from ffcart where cookieid=@cookieid

END
GO
