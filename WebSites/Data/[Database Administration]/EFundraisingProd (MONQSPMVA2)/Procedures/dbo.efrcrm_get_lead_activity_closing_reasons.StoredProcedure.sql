USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_activity_closing_reasons]    Script Date: 02/14/2014 13:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Activity_Closing_Reason
CREATE PROCEDURE [dbo].[efrcrm_get_lead_activity_closing_reasons] AS
begin

select Activity_Closing_Reason_ID, Reason from Lead_Activity_Closing_Reason

end
GO
