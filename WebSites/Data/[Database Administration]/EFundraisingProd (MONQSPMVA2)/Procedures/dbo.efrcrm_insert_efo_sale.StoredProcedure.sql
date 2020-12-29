USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_sale]    Script Date: 02/14/2014 13:06:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Sale
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_sale] @Sale_ID int OUTPUT, @Supporter_ID int, @Sale_Date smalldatetime, @Amount_To_Group smallmoney, @Amount_To_Supplier smallmoney, @Amount smallmoney, @Delivery_Address varchar(75), @State_Code varchar(10), @Country_Code varchar(10), @Delivery_City varchar(50), @Delivery_Zip_Code varchar(10), @Card_Name varchar(30), @Card_Address varchar(75), @Transaction_ID varchar(15) AS
begin

insert into EFO_Sale(Supporter_ID, Sale_Date, Amount_To_Group, Amount_To_Supplier, Amount, Delivery_Address, State_Code, Country_Code, Delivery_City, Delivery_Zip_Code, Card_Name, Card_Address, Transaction_ID) values(@Supporter_ID, @Sale_Date, @Amount_To_Group, @Amount_To_Supplier, @Amount, @Delivery_Address, @State_Code, @Country_Code, @Delivery_City, @Delivery_Zip_Code, @Card_Name, @Card_Address, @Transaction_ID)

select @Sale_ID = SCOPE_IDENTITY()

end
GO
