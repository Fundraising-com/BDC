USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_bank]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Bank
CREATE PROCEDURE [dbo].[efrcrm_insert_bank] @Bank_ID int OUTPUT, @Name varchar(50), @Contact varchar(50), @Street_Address varchar(100), @State_Code varchar(10), @City varchar(50), @Zip_Code varchar(10), @Country_Code varchar(10), @Telephone varchar(20), @Fax varchar(20) AS
begin

insert into Bank(Name, Contact, Street_Address, State_Code, City, Zip_Code, Country_Code, Telephone, Fax) values(@Name, @Contact, @Street_Address, @State_Code, @City, @Zip_Code, @Country_Code, @Telephone, @Fax)

select @Bank_ID = SCOPE_IDENTITY()

end
GO
