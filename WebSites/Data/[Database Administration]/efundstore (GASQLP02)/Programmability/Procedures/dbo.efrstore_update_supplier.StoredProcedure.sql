USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_supplier]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Supplier
CREATE PROCEDURE [dbo].[efrstore_update_supplier] @Supplier_id int, @Name varchar(50), @Street_adress varchar(255), @City varchar(30), @Zip varchar(20), @Contact_name varchar(50), @Phone varchar(20), @Fax varchar(20), @Account_no varchar(20), @Credit_margin decimal(15, 4), @State_code varchar(10), @Country_code varchar(10), @Comment text AS
begin

update Supplier set Name=@Name, Street_adress=@Street_adress, City=@City, Zip=@Zip, Contact_name=@Contact_name, Phone=@Phone, Fax=@Fax, Account_no=@Account_no, Credit_margin=@Credit_margin, State_code=@State_code, Country_code=@Country_code, Comment=@Comment where Supplier_id=@Supplier_id

end
GO
