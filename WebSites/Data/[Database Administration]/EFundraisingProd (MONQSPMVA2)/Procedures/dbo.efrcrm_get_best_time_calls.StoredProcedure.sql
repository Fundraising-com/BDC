USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_best_time_calls]    Script Date: 02/14/2014 13:03:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Best_time_call
CREATE PROCEDURE [dbo].[efrcrm_get_best_time_calls] AS
begin

select Best_time_call_id, Best_time_call_desc from Best_time_call

end
GO
