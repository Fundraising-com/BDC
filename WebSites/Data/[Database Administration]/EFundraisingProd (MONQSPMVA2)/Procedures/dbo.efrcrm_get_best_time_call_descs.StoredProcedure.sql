USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_best_time_call_descs]    Script Date: 02/14/2014 13:03:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Best_time_call_desc
CREATE PROCEDURE [dbo].[efrcrm_get_best_time_call_descs] AS
begin

select Best_time_call_id, Language_id, Description from Best_time_call_desc

end
GO
