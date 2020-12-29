USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_flag_pole_by_id]    Script Date: 02/14/2014 13:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Flag_Pole
CREATE PROCEDURE [dbo].[efrcrm_get_flag_pole_by_id] @Flag_Pole_ID int AS
begin

select Flag_Pole_ID, MDR_ID from Flag_Pole where Flag_Pole_ID=@Flag_Pole_ID

end
GO
