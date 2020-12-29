USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_log_harmony_transfer_leads]    Script Date: 02/14/2014 13:08:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Log_harmony_transfer_leads
CREATE PROCEDURE [dbo].[efrcrm_update_log_harmony_transfer_leads] @ID int, @List_name varchar(100), @List_desc varchar(100), @Old_consultant_id int, @New_consultant_id int, @Transferer_id int, @Transfer_date datetime, @Lead_id int AS
begin

update Log_harmony_transfer_leads set List_name=@List_name, List_desc=@List_desc, Old_consultant_id=@Old_consultant_id, New_consultant_id=@New_consultant_id, Transferer_id=@Transferer_id, Transfer_date=@Transfer_date, Lead_id=@Lead_id where ID=@ID

end
GO
