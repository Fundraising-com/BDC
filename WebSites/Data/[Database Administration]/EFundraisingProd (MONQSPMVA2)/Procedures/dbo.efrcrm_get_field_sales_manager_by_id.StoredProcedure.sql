USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_field_sales_manager_by_id]    Script Date: 02/14/2014 13:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Field_Sales_Manager
CREATE PROCEDURE [dbo].[efrcrm_get_field_sales_manager_by_id] @FSM_ID int AS
begin

select FSM_ID, QSP_ID, Area_Manager_ID, First_Name, Password, Last_Name, Email, Home_Phone, Work_Phone, Fax_Number, Toll_Free_Phone, Mobile_Phone, Pager_Phone, Region from Field_Sales_Manager where FSM_ID=@FSM_ID

end
GO
