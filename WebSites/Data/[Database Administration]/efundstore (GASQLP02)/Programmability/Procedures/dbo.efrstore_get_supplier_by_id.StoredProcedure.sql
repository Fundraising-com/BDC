USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_supplier_by_id]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Supplier
CREATE PROCEDURE [dbo].[efrstore_get_supplier_by_id] @Supplier_id int AS
begin

select Supplier_id, Name, Street_adress, City, Zip, Contact_name, Phone, Fax, Account_no, Credit_margin, State_code, Country_code, Comment from Supplier where Supplier_id=@Supplier_id

end
GO
