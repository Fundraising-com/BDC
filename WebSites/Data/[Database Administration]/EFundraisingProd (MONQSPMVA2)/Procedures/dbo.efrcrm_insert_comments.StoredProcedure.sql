USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_comments]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Comments
CREATE PROCEDURE [dbo].[efrcrm_insert_comments] @Comments_ID int OUTPUT, @Priority_ID int, @Sales_ID int, @Consultant_ID int, @Lead_ID int, @Department_ID int, @Entry_Date datetime, @Comments text AS

declare @id int
exec @id = sp_NewID  'Comments_ID', 'All'

begin

insert into Comments(Comments_ID, Priority_ID, Sales_ID, Consultant_ID, Lead_ID, Department_ID, Entry_Date, Comments) values(@id, @Priority_ID, @Sales_ID, @Consultant_ID, @Lead_ID, @Department_ID, @Entry_Date, @Comments)

end
GO
