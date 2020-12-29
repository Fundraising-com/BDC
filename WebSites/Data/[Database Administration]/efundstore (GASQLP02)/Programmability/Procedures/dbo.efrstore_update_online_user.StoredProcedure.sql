USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_online_user]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Online_user
CREATE PROCEDURE [dbo].[efrstore_update_online_user] @Online_user_id int, @Client_sequence_code char(2), @Client_id int, @Email varchar(75), @Online_user_pwd varbinary, @Date_created datetime AS
begin

update Online_user set Client_sequence_code=@Client_sequence_code, Client_id=@Client_id, Email=@Email, Online_user_pwd=@Online_user_pwd, Date_created=@Date_created where Online_user_id=@Online_user_id

end
GO
