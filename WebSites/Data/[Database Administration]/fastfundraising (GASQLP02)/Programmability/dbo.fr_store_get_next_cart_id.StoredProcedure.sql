USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[fr_store_get_next_cart_id]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pavel Tarassov	>
-- Create date: <26--03-2012,>
-- Description:	<Gets next cartID (should eliminate race condition>
-- =============================================
CREATE PROCEDURE [dbo].[fr_store_get_next_cart_id]
@cartid int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @V1 table (c1 int);
	
	
	update ffcookies
	SET cookieid = (select MAX(cookieid) + 1 from ffcookies)output inserted.cookieid
	into @V1
	SET @cartid = (select * from @V1)

END
GO
