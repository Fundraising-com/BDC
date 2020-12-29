USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_unassignlogin]    Script Date: 02/14/2014 13:08:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for UnAssignLogin
CREATE PROCEDURE [dbo].[efrcrm_update_unassignlogin] @UnAssignLogin_Id int, @User_Name varchar(50), @Consultant_Id int, @Lead_Id int, @Unassignment_TimeStamp smalldatetime AS
begin

update UnAssignLogin set User_Name=@User_Name, Consultant_Id=@Consultant_Id, Lead_Id=@Lead_Id, Unassignment_TimeStamp=@Unassignment_TimeStamp where UnAssignLogin_Id=@UnAssignLogin_Id

end
GO
