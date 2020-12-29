USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_best_time_call]    Script Date: 02/14/2014 13:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Best_time_call
CREATE PROCEDURE [dbo].[efrstore_update_best_time_call] @Best_time_call_id tinyint, @Description varchar(20) AS
begin

update Best_time_call set Description=@Description where Best_time_call_id=@Best_time_call_id

end
GO
