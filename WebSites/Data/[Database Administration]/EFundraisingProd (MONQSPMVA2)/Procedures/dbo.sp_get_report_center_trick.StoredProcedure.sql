USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_report_center_trick]    Script Date: 02/14/2014 13:08:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_get_report_center_trick] (@rand_id VARCHAR(20)) AS
BEGIN
select rand_id, partner_id, promotion_id, description, countofleadid, countofsalesid, brochures, candies, scratchcards from tmp_report_center_res where rand_id = @rand_id
END
GO
