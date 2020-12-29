USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_valid_efrecommerce_order_status]    Script Date: 02/14/2014 13:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: August 29, 2011
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[es_get_valid_efrecommerce_order_status] ()
RETURNS @ValidOrderStatus TABLE (status_id int)
AS
BEGIN
	INSERT INTO @ValidOrderStatus
	VALUES(1)
	INSERT INTO @ValidOrderStatus
	VALUES(2)
	INSERT INTO @ValidOrderStatus
	VALUES(3)
	INSERT INTO @ValidOrderStatus
	VALUES(7)

	RETURN
END
GO
