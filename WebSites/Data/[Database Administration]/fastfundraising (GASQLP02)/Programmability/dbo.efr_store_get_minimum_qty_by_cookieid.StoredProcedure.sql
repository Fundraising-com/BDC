USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_minimum_qty_by_cookieid]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efr_store_get_minimum_qty_by_cookieid]
@cookieid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  select c.*, i.minimumqty 
  from ffcart c inner join ffitems i on c.itemid=i.itemid 
  where cookieid= @cookieid

END
GO
