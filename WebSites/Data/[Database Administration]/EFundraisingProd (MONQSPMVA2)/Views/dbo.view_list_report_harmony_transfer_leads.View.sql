USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_report_harmony_transfer_leads]    Script Date: 02/14/2014 13:02:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_report_harmony_transfer_leads]
AS
SELECT     TOP 100 PERCENT dbo.log_harmony_transfer_leads.list_desc, Consultant_3.Name AS old_consultant_name, 
                      Consultant_1.Name AS new_consultant_name, Consultant_2.Name AS transferer, dbo.log_harmony_transfer_leads.transfer_date, 
                      dbo.log_harmony_transfer_leads.lead_id
FROM         dbo.Consultant Consultant_2 INNER JOIN
                      dbo.Consultant Consultant_1 INNER JOIN
                      dbo.log_harmony_transfer_leads INNER JOIN
                      dbo.Consultant Consultant_3 ON dbo.log_harmony_transfer_leads.old_consultant_id = Consultant_3.Consultant_ID ON 
                      Consultant_1.Consultant_ID = dbo.log_harmony_transfer_leads.new_consultant_id ON 
                      Consultant_2.Consultant_ID = dbo.log_harmony_transfer_leads.transferer_id
ORDER BY dbo.log_harmony_transfer_leads.transfer_date DESC
GO
