USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_transfer_statuss]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Transfer_status
CREATE PROCEDURE [dbo].[efrcrm_get_transfer_statuss] AS
begin

select Transfer_status_id, Transfer_status_desc from Transfer_status

end
GO
