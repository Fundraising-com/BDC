USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_cancelation_reasons]    Script Date: 02/14/2014 13:03:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Cancelation_Reason
CREATE PROCEDURE [dbo].[efrcrm_get_cancelation_reasons] AS
begin

select Cancelation_Reason_Id, Description from Cancelation_Reason

end
GO
