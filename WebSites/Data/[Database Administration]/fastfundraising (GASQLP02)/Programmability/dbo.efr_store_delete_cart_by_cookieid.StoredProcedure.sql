USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_delete_cart_by_cookieid]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efr_store_delete_cart_by_cookieid]
	@cookieid int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from ffcart where cookieid=@cookieid

END
GO
