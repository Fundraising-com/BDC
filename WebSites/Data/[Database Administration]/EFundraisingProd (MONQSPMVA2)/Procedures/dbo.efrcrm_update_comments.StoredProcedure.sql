USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_comments]    Script Date: 02/14/2014 13:07:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Comments
CREATE PROCEDURE [dbo].[efrcrm_update_comments] @Comments_ID int, @Priority_ID int, @Sales_ID int, @Consultant_ID int, @Lead_ID int, @Department_ID int, @Entry_Date datetime, @Comments text AS
begin

update Comments set Priority_ID=@Priority_ID, Sales_ID=@Sales_ID, Consultant_ID=@Consultant_ID, Lead_ID=@Lead_ID, Department_ID=@Department_ID, Entry_Date=@Entry_Date, Comments=@Comments where Comments_ID=@Comments_ID

end
GO
