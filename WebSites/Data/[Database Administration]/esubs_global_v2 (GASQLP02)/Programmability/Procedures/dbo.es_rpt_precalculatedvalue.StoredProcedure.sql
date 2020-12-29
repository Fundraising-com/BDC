USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_precalculatedvalue]    Script Date: 02/14/2014 13:06:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_rpt_precalculatedvalue] 
AS
BEGIN
    SELECT top 1 sales_amount_grand_total AS grand_sales_total
    FROM precalculatedvalue
    ORDER BY update_date DESC

    RETURN 0
END
GO
