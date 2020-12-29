USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_online_user]    Script Date: 02/14/2014 13:05:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Online_user
CREATE PROCEDURE [dbo].[efrstore_insert_online_user] @Online_user_id int OUTPUT, @Client_sequence_code char(2), @Client_id int, @Email varchar(75), @Online_user_pwd varbinary, @Date_created datetime AS
begin

insert into Online_user(Client_sequence_code, Client_id, Email, Online_user_pwd, Date_created) values(@Client_sequence_code, @Client_id, @Email, @Online_user_pwd, @Date_created)

select @Online_user_id = SCOPE_IDENTITY()

end
GO
