USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_mspub_identity_range_by_id]    Script Date: 02/14/2014 13:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for MSpub_identity_range
CREATE PROCEDURE [dbo].[efrcrm_get_mspub_identity_range_by_id] @Objid int AS
begin

select Objid, Range, Pub_range, Current_pub_range, Threshold, Last_seed from MSpub_identity_range where Objid=@Objid

end
GO
