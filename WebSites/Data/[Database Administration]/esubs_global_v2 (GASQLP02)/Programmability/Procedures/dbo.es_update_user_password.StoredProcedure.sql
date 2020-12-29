USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_user_password]    Script Date: 02/14/2014 13:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jean-Francois Buist
-- Create date: March 2, 2011
-- Description:	Part of the Direct Mail project,
--              this stored procedure updates the
--              user table instead of the member
--              table.
-- =============================================

CREATE PROCEDURE [dbo].[es_update_user_password]
	@member_id int,
	@password varchar(100)
AS
BEGIN
	declare @userId int

	if @password is not null and len(@password) > 0
	begin
		set @userId = dbo.es_get_user_id_by_member_id (@member_id)
		
		if(@userId > 0)
		begin
			UPDATE [users]
			SET [password] = @password
			WHERE [user_id] = @userId
		end
	end
    
END
GO
