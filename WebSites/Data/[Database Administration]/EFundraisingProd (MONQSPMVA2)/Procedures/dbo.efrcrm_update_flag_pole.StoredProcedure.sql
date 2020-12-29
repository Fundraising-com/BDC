USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_flag_pole]    Script Date: 02/14/2014 13:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Flag_Pole
CREATE PROCEDURE [dbo].[efrcrm_update_flag_pole] @Flag_Pole_ID int, @MDR_ID varchar(15) AS
begin

update Flag_Pole set MDR_ID=@MDR_ID where Flag_Pole_ID=@Flag_Pole_ID

end
GO
