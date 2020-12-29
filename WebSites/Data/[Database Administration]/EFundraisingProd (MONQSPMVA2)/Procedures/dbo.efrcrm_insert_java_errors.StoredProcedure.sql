USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_java_errors]    Script Date: 02/14/2014 13:06:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Java_Errors
CREATE PROCEDURE [dbo].[efrcrm_insert_java_errors] @Error_ID int OUTPUT, @Class_Name varchar(255), @Error_Message text, @Error_Date datetime AS
begin

insert into Java_Errors(Class_Name, Error_Message, Error_Date) values(@Class_Name, @Error_Message, @Error_Date)

select @Error_ID = SCOPE_IDENTITY()

end
GO
