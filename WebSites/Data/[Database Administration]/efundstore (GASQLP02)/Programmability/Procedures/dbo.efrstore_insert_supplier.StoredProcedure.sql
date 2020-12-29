USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_supplier]    Script Date: 02/14/2014 13:06:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Supplier
CREATE PROCEDURE [dbo].[efrstore_insert_supplier] @Supplier_id int OUTPUT, @Name varchar(50), @Street_adress varchar(255), @City varchar(30), @Zip varchar(20), @Contact_name varchar(50), @Phone varchar(20), @Fax varchar(20), @Account_no varchar(20), @Credit_margin decimal(15, 4), @State_code varchar(10), @Country_code varchar(10), @Comment text AS
begin

insert into Supplier(Name, Street_adress, City, Zip, Contact_name, Phone, Fax, Account_no, Credit_margin, State_code, Country_code, Comment) values(@Name, @Street_adress, @City, @Zip, @Contact_name, @Phone, @Fax, @Account_no, @Credit_margin, @State_code, @Country_code, @Comment)

select @Supplier_id = SCOPE_IDENTITY()

end
GO
