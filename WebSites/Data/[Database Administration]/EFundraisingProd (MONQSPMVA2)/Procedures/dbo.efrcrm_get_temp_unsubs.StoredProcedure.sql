USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_temp_unsubs]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Temp_unsub
CREATE PROCEDURE [dbo].[efrcrm_get_temp_unsubs] AS
begin

select EMAIL from Temp_unsub

end
GO
