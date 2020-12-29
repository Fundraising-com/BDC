USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_conditions_by_id]    Script Date: 02/14/2014 13:04:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Conditions
CREATE PROCEDURE [dbo].[efrcrm_get_lead_conditions_by_id] @Condition_ID int AS
begin

select Condition_ID, Description from Lead_Conditions where Condition_ID=@Condition_ID

end
GO
