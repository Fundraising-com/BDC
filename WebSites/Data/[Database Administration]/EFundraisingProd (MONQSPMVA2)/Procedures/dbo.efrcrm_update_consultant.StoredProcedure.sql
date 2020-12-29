USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_consultant]    Script Date: 02/14/2014 13:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Consultant
CREATE PROCEDURE [dbo].[efrcrm_update_consultant] @Consultant_id int, @Division_id tinyint, @Client_id int, @Client_sequence_code varchar(4), @Department_id int, @Partner_id int, @Consultant_transfer_status_id tinyint, @Territory_id smallint, @Ext_consultant_id int, @Name varchar(50), @Is_agent bit, @Is_active bit, @Nt_login varchar(50), @Phone_extension varchar(50), @Email_address varchar(50), @Home_phone varchar(15), @Work_phone varchar(15), @Fax_number varchar(15), @Toll_free_phone varchar(15), @Mobile_phone varchar(15), @Pager_phone varchar(15), @Default_proposal_text text, @Csr_consultant bit, @Objectives float, @Is_available bit, @Password varchar(255), @Kit_paid bit, @Is_fm bit, @Create_date datetime AS
begin

update Consultant set Division_id=@Division_id, Client_id=@Client_id, Client_sequence_code=@Client_sequence_code, Department_id=@Department_id, Partner_id=@Partner_id, Consultant_transfer_status_id=@Consultant_transfer_status_id, Territory_id=@Territory_id, Ext_consultant_id=@Ext_consultant_id, Name=@Name, Is_agent=@Is_agent, Is_active=@Is_active, Nt_login=@Nt_login, Phone_extension=@Phone_extension, Email_address=@Email_address, Home_phone=@Home_phone, Work_phone=@Work_phone, Fax_number=@Fax_number, Toll_free_phone=@Toll_free_phone, Mobile_phone=@Mobile_phone, Pager_phone=@Pager_phone, Default_proposal_text=@Default_proposal_text, Csr_consultant=@Csr_consultant, Objectives=@Objectives, Is_available=@Is_available, Password=@Password, Kit_paid=@Kit_paid, Is_fm=@Is_fm, Create_date=@Create_date where Consultant_id=@Consultant_id

end
GO
