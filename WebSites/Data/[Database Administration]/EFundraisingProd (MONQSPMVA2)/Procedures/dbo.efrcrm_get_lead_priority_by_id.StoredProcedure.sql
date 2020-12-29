USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_priority_by_id]    Script Date: 02/14/2014 13:05:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Priority
CREATE PROCEDURE [dbo].[efrcrm_get_lead_priority_by_id] @Lead_Priority_Id int AS
begin

select Lead_Priority_Id, Description from Lead_Priority where Lead_Priority_Id=@Lead_Priority_Id

end
GO
