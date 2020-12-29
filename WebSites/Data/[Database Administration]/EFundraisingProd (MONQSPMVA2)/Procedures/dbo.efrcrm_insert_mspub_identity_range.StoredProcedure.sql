USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_mspub_identity_range]    Script Date: 02/14/2014 13:07:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for MSpub_identity_range
CREATE PROCEDURE [dbo].[efrcrm_insert_mspub_identity_range] @Objid int OUTPUT, @Range bigint, @Pub_range bigint, @Current_pub_range bigint, @Threshold int, @Last_seed bigint AS
begin

insert into MSpub_identity_range(Range, Pub_range, Current_pub_range, Threshold, Last_seed) values(@Range, @Pub_range, @Current_pub_range, @Threshold, @Last_seed)

select @Objid = SCOPE_IDENTITY()

end
GO
