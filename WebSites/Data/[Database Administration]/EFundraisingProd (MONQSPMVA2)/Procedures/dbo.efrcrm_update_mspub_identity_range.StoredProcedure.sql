USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_mspub_identity_range]    Script Date: 02/14/2014 13:08:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for MSpub_identity_range
CREATE PROCEDURE [dbo].[efrcrm_update_mspub_identity_range] @Objid int, @Range bigint, @Pub_range bigint, @Current_pub_range bigint, @Threshold int, @Last_seed bigint AS
begin

update MSpub_identity_range set Range=@Range, Pub_range=@Pub_range, Current_pub_range=@Current_pub_range, Threshold=@Threshold, Last_seed=@Last_seed where Objid=@Objid

end
GO
