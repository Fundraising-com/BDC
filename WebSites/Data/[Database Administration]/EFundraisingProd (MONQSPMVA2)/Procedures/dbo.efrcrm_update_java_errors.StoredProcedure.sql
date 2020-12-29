USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_java_errors]    Script Date: 02/14/2014 13:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Java_Errors
CREATE PROCEDURE [dbo].[efrcrm_update_java_errors] @Error_ID int, @Class_Name varchar(255), @Error_Message text, @Error_Date datetime AS
begin

update Java_Errors set Class_Name=@Class_Name, Error_Message=@Error_Message, Error_Date=@Error_Date where Error_ID=@Error_ID

end
GO
