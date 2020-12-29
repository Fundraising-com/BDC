USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_log_harmony_transfer_leads]    Script Date: 02/14/2014 13:07:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Log_harmony_transfer_leads
CREATE PROCEDURE [dbo].[efrcrm_insert_log_harmony_transfer_leads] @ID int OUTPUT, @List_name varchar(100), @List_desc varchar(100), @Old_consultant_id int, @New_consultant_id int, @Transferer_id int, @Transfer_date datetime, @Lead_id int AS
begin

insert into Log_harmony_transfer_leads(List_name, List_desc, Old_consultant_id, New_consultant_id, Transferer_id, Transfer_date, Lead_id) values(@List_name, @List_desc, @Old_consultant_id, @New_consultant_id, @Transferer_id, @Transfer_date, @Lead_id)

select @ID = SCOPE_IDENTITY()

end
GO
