USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_general_comments]    Script Date: 02/14/2014 13:04:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for General_Comment
CREATE PROCEDURE [dbo].[efrcrm_get_general_comments] AS
begin

select General_Comment_Id, Lead_Id, Sales_Id, Entry_Date, General_Comment, User_Name, Department_ID from General_Comment

end
GO
