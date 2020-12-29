USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_profit_ranges_by_profit_id]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrc_get_profit_ranges_by_profit_id]
@profit_id int
AS
BEGIN
SELECT     
	   [profit_range_id]
      ,[profit_id]
      ,[profit_range_percentage]
      ,[min_sub]
      ,[min_amount]
      ,[operator]
      ,[disclaimer]
FROM [EFRCommon].[dbo].[profit_range]
WHERE profit_id = @profit_id
END
GO
