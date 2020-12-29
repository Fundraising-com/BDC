USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_change_log_by_id]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Sales_Change_Log
CREATE PROCEDURE [dbo].[efrcrm_get_sales_change_log_by_id] @Sales_ID int AS
begin

select Sales_ID, Table_Name, Column_Name, Change_Date_Time, User_Name, From_Value, To_Value, Comment, Computer_Name, Cancelation_reason_Id, Other_Reason from Sales_Change_Log where Sales_ID=@Sales_ID

end
GO
