USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_log_harmony_transfer_leads_by_id]    Script Date: 02/14/2014 13:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Log_harmony_transfer_leads
CREATE PROCEDURE [dbo].[efrcrm_get_log_harmony_transfer_leads_by_id] @ID int AS
begin

select ID, List_name, List_desc, Old_consultant_id, New_consultant_id, Transferer_id, Transfer_date, Lead_id from Log_harmony_transfer_leads where ID=@ID

end
GO
