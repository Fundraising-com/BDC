USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_update_cart_by_cookieid_and_itemid]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efr_store_update_cart_by_cookieid_and_itemid]
	@newqty int,
	@price float,
	@totalcost float,
	@cookieid int,
	@itemid int	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   update ffcart 
   set qty=@newqty,itempriceapplied=@price,totalcost=@totalcost
   where cookieid=@cookieid and itemid=@itemid

END
GO
