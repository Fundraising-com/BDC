USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_statuss]    Script Date: 02/14/2014 13:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Status
CREATE PROCEDURE [dbo].[efrcrm_get_efo_statuss] AS
begin

select Status_ID, Status from EFO_Status

end
GO
