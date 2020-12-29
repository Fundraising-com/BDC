USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_default_consultant_rates]    Script Date: 02/14/2014 13:04:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Default_Consultant_Rate
CREATE PROCEDURE [dbo].[efrcrm_get_default_consultant_rates] AS
begin

select Consultant_ID, Promotion_Type_Code, Default_Commission_Rate from Default_Consultant_Rate

end
GO
