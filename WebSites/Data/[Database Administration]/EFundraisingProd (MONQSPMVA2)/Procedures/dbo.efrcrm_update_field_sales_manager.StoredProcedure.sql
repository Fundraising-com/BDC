USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_field_sales_manager]    Script Date: 02/14/2014 13:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Field_Sales_Manager
CREATE PROCEDURE [dbo].[efrcrm_update_field_sales_manager] @FSM_ID int, @QSP_ID varchar(20), @Area_Manager_ID int, @First_Name varchar(50), @Password varchar(15), @Last_Name varchar(50), @Email varchar(50), @Home_Phone varchar(15), @Work_Phone varchar(15), @Fax_Number varchar(15), @Toll_Free_Phone varchar(15), @Mobile_Phone varchar(15), @Pager_Phone varchar(15), @Region varchar(30) AS
begin

update Field_Sales_Manager set QSP_ID=@QSP_ID, Area_Manager_ID=@Area_Manager_ID, First_Name=@First_Name, Password=@Password, Last_Name=@Last_Name, Email=@Email, Home_Phone=@Home_Phone, Work_Phone=@Work_Phone, Fax_Number=@Fax_Number, Toll_Free_Phone=@Toll_Free_Phone, Mobile_Phone=@Mobile_Phone, Pager_Phone=@Pager_Phone, Region=@Region where FSM_ID=@FSM_ID

end
GO
