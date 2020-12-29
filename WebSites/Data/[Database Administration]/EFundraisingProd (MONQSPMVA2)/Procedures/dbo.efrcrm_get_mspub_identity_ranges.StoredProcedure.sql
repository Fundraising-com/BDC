USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_mspub_identity_ranges]    Script Date: 02/14/2014 13:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for MSpub_identity_range
CREATE PROCEDURE [dbo].[efrcrm_get_mspub_identity_ranges] AS
begin

select Objid, Range, Pub_range, Current_pub_range, Threshold, Last_seed from MSpub_identity_range

end
GO
