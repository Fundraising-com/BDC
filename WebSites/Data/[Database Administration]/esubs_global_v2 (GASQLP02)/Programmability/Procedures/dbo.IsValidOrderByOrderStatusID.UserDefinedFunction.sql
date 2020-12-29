USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[IsValidOrderByOrderStatusID]    Script Date: 02/14/2014 13:08:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JIRO HIDAKA
-- Create date: September 15 2009
-- Description:	Determines if order should be included in the
--              reports based on order status id
-- =============================================
CREATE FUNCTION [dbo].[IsValidOrderByOrderStatusID] (@order_status_id int)
RETURNS bit
AS
BEGIN
if (@order_status_id in ( 101, 110, 112, 120, 201, 301, 302, 304, 401, 501, 601, 701 ))
	return 1
else
	return 0

return 0
END
GO
