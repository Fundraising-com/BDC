USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_commission_rate_by_id]    Script Date: 02/14/2014 13:04:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Commission_Rate
CREATE PROCEDURE [dbo].[efrcrm_get_commission_rate_by_id] @Consultant_ID int AS
begin

select Consultant_ID, Commission_Rate_Free, Commission_Rate_No_Free, Scratch_Book_ID from Commission_Rate where Consultant_ID=@Consultant_ID

end
GO
