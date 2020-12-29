USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_ar_consultant]    Script Date: 02/14/2014 13:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for AR_Consultant
CREATE PROCEDURE [dbo].[efrcrm_update_ar_consultant] @AR_Consultant_ID int, @Name varchar(50), @Email varchar(50), @Phone_Ext varchar(3), @Is_Active bit, @Nt_Login varchar(50) AS
begin

update AR_Consultant set Name=@Name, Email=@Email, Phone_Ext=@Phone_Ext, Is_Active=@Is_Active, Nt_Login=@Nt_Login where AR_Consultant_ID=@AR_Consultant_ID

end
GO
