USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_consultant]    Script Date: 02/14/2014 13:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Consultant
CREATE PROCEDURE [dbo].[efrcrm_insert_consultant] @Consultant_id int OUTPUT, @Division_id tinyint, @Client_id int, @Client_sequence_code varchar(4), @Department_id int, @Partner_id int, @Consultant_transfer_status_id tinyint, @Territory_id smallint, @Ext_consultant_id int, @Name varchar(50), @Is_agent bit, @Is_active bit, @Nt_login varchar(50), @Phone_extension varchar(50), @Email_address varchar(50), @Home_phone varchar(15), @Work_phone varchar(15), @Fax_number varchar(15), @Toll_free_phone varchar(15), @Mobile_phone varchar(15), @Pager_phone varchar(15), @Default_proposal_text text, @Csr_consultant bit, @Objectives float, @Is_available bit, @Password varchar(255), @Kit_paid bit, @Is_fm bit, @Create_date datetime AS
begin

insert into Consultant(Division_id, Client_id, Client_sequence_code, Department_id, Partner_id, Consultant_transfer_status_id, Territory_id, Ext_consultant_id, Name, Is_agent, Is_active, Nt_login, Phone_extension, Email_address, Home_phone, Work_phone, Fax_number, Toll_free_phone, Mobile_phone, Pager_phone, Default_proposal_text, Csr_consultant, Objectives, Is_available, Password, Kit_paid, Is_fm, Create_date) values(@Division_id, @Client_id, @Client_sequence_code, @Department_id, @Partner_id, @Consultant_transfer_status_id, @Territory_id, @Ext_consultant_id, @Name, @Is_agent, @Is_active, @Nt_login, @Phone_extension, @Email_address, @Home_phone, @Work_phone, @Fax_number, @Toll_free_phone, @Mobile_phone, @Pager_phone, @Default_proposal_text, @Csr_consultant, @Objectives, @Is_available, @Password, @Kit_paid, @Is_fm, @Create_date)

select @Consultant_id = SCOPE_IDENTITY()

end
GO
