USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_flag_poles]    Script Date: 02/14/2014 13:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Flag_Pole
CREATE PROCEDURE [dbo].[efrcrm_get_flag_poles] AS
begin

select Flag_Pole_ID, MDR_ID from Flag_Pole

end
GO
