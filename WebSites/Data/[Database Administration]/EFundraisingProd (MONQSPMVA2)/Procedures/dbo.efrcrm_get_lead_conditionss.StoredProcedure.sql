USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_conditionss]    Script Date: 02/14/2014 13:04:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Conditions
CREATE PROCEDURE [dbo].[efrcrm_get_lead_conditionss] AS
begin

select Condition_ID, Description from Lead_Conditions

end
GO
