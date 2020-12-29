USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_decision_by_id]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Req_Decision
CREATE PROCEDURE [dbo].[efrcrm_get_req_decision_by_id] @Decision_Id int AS
begin

select Decision_Id, Language_Id, Description from Req_Decision where Decision_Id=@Decision_Id

end
GO
