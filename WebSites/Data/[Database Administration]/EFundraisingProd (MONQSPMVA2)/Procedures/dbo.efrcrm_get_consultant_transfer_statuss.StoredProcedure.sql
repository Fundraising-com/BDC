USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_consultant_transfer_statuss]    Script Date: 02/14/2014 13:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Consultant_transfer_status
CREATE PROCEDURE [dbo].[efrcrm_get_consultant_transfer_statuss] AS
begin

select Consultant_transfer_status_id, Consultant_transfer_status_desc from Consultant_transfer_status

end
GO
