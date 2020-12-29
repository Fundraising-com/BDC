USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_commission_rate]    Script Date: 02/14/2014 13:06:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Commission_Rate
CREATE PROCEDURE [dbo].[efrcrm_insert_commission_rate] @Consultant_ID int OUTPUT, @Commission_Rate_Free decimal, @Commission_Rate_No_Free decimal, @Scratch_Book_ID int AS
begin

insert into Commission_Rate(Commission_Rate_Free, Commission_Rate_No_Free, Scratch_Book_ID) values(@Commission_Rate_Free, @Commission_Rate_No_Free, @Scratch_Book_ID)

select @Consultant_ID = SCOPE_IDENTITY()

end
GO
