USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_commission_rate]    Script Date: 02/14/2014 13:07:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Commission_Rate
CREATE PROCEDURE [dbo].[efrcrm_update_commission_rate] @Consultant_ID int, @Commission_Rate_Free decimal, @Commission_Rate_No_Free decimal, @Scratch_Book_ID int AS
begin

update Commission_Rate set Commission_Rate_Free=@Commission_Rate_Free, Commission_Rate_No_Free=@Commission_Rate_No_Free, Scratch_Book_ID=@Scratch_Book_ID where Consultant_ID=@Consultant_ID

end
GO
