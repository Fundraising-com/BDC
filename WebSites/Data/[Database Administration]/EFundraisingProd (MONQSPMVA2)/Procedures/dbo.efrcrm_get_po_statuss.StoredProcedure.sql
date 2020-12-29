USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_po_statuss]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Po_status
CREATE PROCEDURE [dbo].[efrcrm_get_po_statuss] AS
begin
	select Po_status_id, Description from Po_status where active = 1
end
GO
