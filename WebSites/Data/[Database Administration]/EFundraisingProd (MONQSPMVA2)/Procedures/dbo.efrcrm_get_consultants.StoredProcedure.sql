USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_consultants]    Script Date: 02/14/2014 13:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Consultant
CREATE PROCEDURE [dbo].[efrcrm_get_consultants] AS
begin

select Consultant_id, Division_id, Client_id, Client_sequence_code, Department_id, Partner_id, Consultant_transfer_status_id, Territory_id, Ext_consultant_id, Name, Is_agent, Is_active, Nt_login, Phone_extension, Email_address, Home_phone, Work_phone, Fax_number, Toll_free_phone, Mobile_phone, Pager_phone, Default_proposal_text, Csr_consultant, Objectives, Is_available, Password, Kit_paid, Is_fm, Create_date from Consultant

end
GO
