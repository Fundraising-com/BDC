USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_online_users]    Script Date: 02/14/2014 13:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Online_user
CREATE PROCEDURE [dbo].[efrstore_get_online_users] AS
begin

select Online_user_id, Client_sequence_code, Client_id, Email, Online_user_pwd, Date_created from Online_user

end
GO
