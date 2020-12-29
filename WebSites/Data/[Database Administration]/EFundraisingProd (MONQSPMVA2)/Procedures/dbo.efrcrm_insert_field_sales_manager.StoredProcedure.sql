USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_field_sales_manager]    Script Date: 02/14/2014 13:06:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Field_Sales_Manager
CREATE PROCEDURE [dbo].[efrcrm_insert_field_sales_manager] @FSM_ID int OUTPUT, @QSP_ID varchar(20), @Area_Manager_ID int, @First_Name varchar(50), @Password varchar(15), @Last_Name varchar(50), @Email varchar(50), @Home_Phone varchar(15), @Work_Phone varchar(15), @Fax_Number varchar(15), @Toll_Free_Phone varchar(15), @Mobile_Phone varchar(15), @Pager_Phone varchar(15), @Region varchar(30) AS
begin

insert into Field_Sales_Manager(QSP_ID, Area_Manager_ID, First_Name, Password, Last_Name, Email, Home_Phone, Work_Phone, Fax_Number, Toll_Free_Phone, Mobile_Phone, Pager_Phone, Region) values(@QSP_ID, @Area_Manager_ID, @First_Name, @Password, @Last_Name, @Email, @Home_Phone, @Work_Phone, @Fax_Number, @Toll_Free_Phone, @Mobile_Phone, @Pager_Phone, @Region)

select @FSM_ID = SCOPE_IDENTITY()

end
GO
