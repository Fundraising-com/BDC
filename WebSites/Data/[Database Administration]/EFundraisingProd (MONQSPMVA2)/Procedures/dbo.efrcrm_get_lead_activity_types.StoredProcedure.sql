USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_activity_types]    Script Date: 02/14/2014 13:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_get_lead_activity_types] AS
begin

select Lead_Activity_Type_Id, Description, Priority from Lead_Activity_Type

end
GO
