USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_authentificate_user]    Script Date: 02/14/2014 13:03:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    procedure [dbo].[crm_authentificate_user] --'jlavign','1111'
           @user_name as varchar(50),
           @password as varchar(50)
   
           
as

select u.*, c.name
from crm_users u
inner join consultant c on u.consultant_id = c.consultant_id
where u.user_name = @user_name and u.password = @password
GO
