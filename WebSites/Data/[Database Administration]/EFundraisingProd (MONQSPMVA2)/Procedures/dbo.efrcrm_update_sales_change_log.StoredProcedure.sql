USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sales_change_log]    Script Date: 02/14/2014 13:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sales_Change_Log
CREATE PROCEDURE [dbo].[efrcrm_update_sales_change_log] @Sales_ID int, @Table_Name varchar(50), @Column_Name varchar(50), @Change_Date_Time datetime, @User_Name varchar(50), @From_Value varchar(255), @To_Value varchar(255), @Comment varchar(255), @Computer_Name varchar(50), @Cancelation_reason_Id int, @Other_Reason varchar(255) AS
begin

update Sales_Change_Log set Table_Name=@Table_Name, Column_Name=@Column_Name, Change_Date_Time=@Change_Date_Time, User_Name=@User_Name, From_Value=@From_Value, To_Value=@To_Value, Comment=@Comment, Computer_Name=@Computer_Name, Cancelation_reason_Id=@Cancelation_reason_Id, Other_Reason=@Other_Reason where Sales_ID=@Sales_ID

end
GO
