USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tmp_lead_tests]    Script Date: 02/14/2014 13:06:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Tmp_lead_test
CREATE PROCEDURE [dbo].[efrcrm_get_tmp_lead_tests] AS
begin

select Lead_id from Tmp_lead_test

end
GO
