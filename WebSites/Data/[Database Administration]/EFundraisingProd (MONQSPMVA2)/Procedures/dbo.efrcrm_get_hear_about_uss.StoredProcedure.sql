USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_hear_about_uss]    Script Date: 02/14/2014 13:04:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Hear_about_us
CREATE PROCEDURE [dbo].[efrcrm_get_hear_about_uss] AS
begin

select Hear_id, Party_type_id, Name, Order_on_web, Is_active from Hear_about_us

end
GO
