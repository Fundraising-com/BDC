USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sales_change_log]    Script Date: 02/14/2014 13:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sales_Change_Log
CREATE PROCEDURE [dbo].[efrcrm_insert_sales_change_log] @Sales_ID int OUTPUT, @Table_Name varchar(50), @Column_Name varchar(50), @Change_Date_Time datetime, @User_Name varchar(50), @From_Value varchar(255), @To_Value varchar(255), @Comment varchar(255), @Computer_Name varchar(50), @Cancelation_reason_Id int, @Other_Reason varchar(255) AS
begin

insert into Sales_Change_Log(Table_Name, Column_Name, Change_Date_Time, User_Name, From_Value, To_Value, Comment, Computer_Name, Cancelation_reason_Id, Other_Reason) values(@Table_Name, @Column_Name, @Change_Date_Time, @User_Name, @From_Value, @To_Value, @Comment, @Computer_Name, @Cancelation_reason_Id, @Other_Reason)

select @Sales_ID = SCOPE_IDENTITY()

end
GO
