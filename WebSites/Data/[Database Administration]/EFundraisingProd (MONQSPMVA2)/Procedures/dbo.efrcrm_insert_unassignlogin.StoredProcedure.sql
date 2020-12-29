USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_unassignlogin]    Script Date: 02/14/2014 13:07:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for UnAssignLogin
CREATE PROCEDURE [dbo].[efrcrm_insert_unassignlogin] @UnAssignLogin_Id int OUTPUT, @User_Name varchar(50), @Consultant_Id int, @Lead_Id int, @Unassignment_TimeStamp smalldatetime AS

declare @id int
exec @id = sp_NewID  'UnAssignLogin_ID', 'All'

begin

insert into UnAssignLogin(UnAssignLogin_Id, User_Name, Consultant_Id, Lead_Id, Unassignment_TimeStamp) values(@id, @User_Name, @Consultant_Id, @Lead_Id, @Unassignment_TimeStamp)

end
GO
