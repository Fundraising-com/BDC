USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_po_status_by_id]    Script Date: 02/14/2014 13:05:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Po_status
create PROCEDURE [dbo].[efrcrm_get_po_status_by_id]
                 @po_status_id int

AS
begin

select Po_status_id, Description from Po_status
where po_status_id = @po_status_id

end
GO
