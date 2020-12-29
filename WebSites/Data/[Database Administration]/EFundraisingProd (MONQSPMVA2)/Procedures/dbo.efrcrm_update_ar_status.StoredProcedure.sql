USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_ar_status]    Script Date: 02/14/2014 13:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for AR_Status
CREATE PROCEDURE [dbo].[efrcrm_update_ar_status] @AR_Status_ID int, @Description varchar(50), @Commission_On_Hold bit, @Commission_Is_Payable bit, @Commission_Is_Credited bit AS
begin

update AR_Status set Description=@Description, Commission_On_Hold=@Commission_On_Hold, Commission_Is_Payable=@Commission_Is_Payable, Commission_Is_Credited=@Commission_Is_Credited where AR_Status_ID=@AR_Status_ID

end
GO
