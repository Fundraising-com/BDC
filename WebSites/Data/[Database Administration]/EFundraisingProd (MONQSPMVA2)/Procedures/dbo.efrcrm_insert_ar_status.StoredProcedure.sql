USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_ar_status]    Script Date: 02/14/2014 13:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for AR_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_ar_status] @AR_Status_ID int OUTPUT, @Description varchar(50), @Commission_On_Hold bit, @Commission_Is_Payable bit, @Commission_Is_Credited bit AS
begin

insert into AR_Status(Description, Commission_On_Hold, Commission_Is_Payable, Commission_Is_Credited) values(@Description, @Commission_On_Hold, @Commission_Is_Payable, @Commission_Is_Credited)

select @AR_Status_ID = SCOPE_IDENTITY()

end
GO
