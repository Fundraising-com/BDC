USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_best_time_call_desc]    Script Date: 02/14/2014 13:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Best_time_call_desc
CREATE PROCEDURE [dbo].[efrstore_update_best_time_call_desc] @Best_time_call_id tinyint, @Culture_code nvarchar(10), @Description varchar(25) AS
begin

update Best_time_call_desc set Culture_code=@Culture_code, Description=@Description where Best_time_call_id=@Best_time_call_id

end
GO
