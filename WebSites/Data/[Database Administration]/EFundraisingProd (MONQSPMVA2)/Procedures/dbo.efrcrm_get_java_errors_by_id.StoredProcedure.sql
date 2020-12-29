USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_java_errors_by_id]    Script Date: 02/14/2014 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Java_Errors
CREATE PROCEDURE [dbo].[efrcrm_get_java_errors_by_id] @Error_ID int AS
begin

select Error_ID, Class_Name, Error_Message, Error_Date from Java_Errors where Error_ID=@Error_ID

end
GO
