USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ar_statuss]    Script Date: 02/14/2014 13:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for AR_Status
CREATE PROCEDURE [dbo].[efrcrm_get_ar_statuss] AS
begin

select AR_Status_ID, Description, Commission_On_Hold, Commission_Is_Payable, Commission_Is_Credited from AR_Status

end
GO
