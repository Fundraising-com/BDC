USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_best_time_calls]    Script Date: 02/14/2014 13:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Best_time_call
CREATE PROCEDURE [dbo].[efrstore_get_best_time_calls] AS
begin

select Best_time_call_id, Description from Best_time_call

end
GO
