USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_prioritys]    Script Date: 02/14/2014 13:05:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Priority
CREATE PROCEDURE [dbo].[efrcrm_get_lead_prioritys] AS
begin

select Lead_Priority_Id, Description from Lead_Priority

end
GO
