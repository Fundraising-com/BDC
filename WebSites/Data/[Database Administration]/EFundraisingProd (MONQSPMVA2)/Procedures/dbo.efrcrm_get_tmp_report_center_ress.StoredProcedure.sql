USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tmp_report_center_ress]    Script Date: 02/14/2014 13:06:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Tmp_report_center_res
CREATE PROCEDURE [dbo].[efrcrm_get_tmp_report_center_ress] AS
begin

select Rand_id, Partner_id, Promotion_id, Description, Countofleadid, Countofsalesid, Brochures, Candies, Scratchcards from Tmp_report_center_res

end
GO
