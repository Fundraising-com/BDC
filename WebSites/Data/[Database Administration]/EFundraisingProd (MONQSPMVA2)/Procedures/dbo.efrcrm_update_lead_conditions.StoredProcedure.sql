USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_conditions]    Script Date: 02/14/2014 13:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Conditions
CREATE PROCEDURE [dbo].[efrcrm_update_lead_conditions] @Condition_ID int, @Description varchar(2000) AS
begin

update Lead_Conditions set Description=@Description where Condition_ID=@Condition_ID

end
GO
