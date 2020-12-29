USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_ar_consultant]    Script Date: 02/14/2014 13:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for AR_Consultant
CREATE PROCEDURE [dbo].[efrcrm_insert_ar_consultant] @AR_Consultant_ID int OUTPUT, @Name varchar(50), @Email varchar(50), @Phone_Ext varchar(3), @Is_Active bit, @Nt_Login varchar(50) AS
begin

insert into AR_Consultant(Name, Email, Phone_Ext, Is_Active, Nt_Login) values(@Name, @Email, @Phone_Ext, @Is_Active, @Nt_Login)

select @AR_Consultant_ID = SCOPE_IDENTITY()

end
GO
