USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_statuss]    Script Date: 02/14/2014 13:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Status
CREATE PROCEDURE [dbo].[efrcrm_get_lead_statuss] AS
begin

select Lead_Status_ID, Description from Lead_Status

end
GO
