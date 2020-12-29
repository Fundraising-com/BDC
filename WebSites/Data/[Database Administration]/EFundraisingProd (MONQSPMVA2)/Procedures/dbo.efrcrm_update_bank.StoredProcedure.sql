USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_bank]    Script Date: 02/14/2014 13:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Bank
CREATE PROCEDURE [dbo].[efrcrm_update_bank] @Bank_ID int, @Name varchar(50), @Contact varchar(50), @Street_Address varchar(100), @State_Code varchar(10), @City varchar(50), @Zip_Code varchar(10), @Country_Code varchar(10), @Telephone varchar(20), @Fax varchar(20) AS
begin

update Bank set Name=@Name, Contact=@Contact, Street_Address=@Street_Address, State_Code=@State_Code, City=@City, Zip_Code=@Zip_Code, Country_Code=@Country_Code, Telephone=@Telephone, Fax=@Fax where Bank_ID=@Bank_ID

end
GO
