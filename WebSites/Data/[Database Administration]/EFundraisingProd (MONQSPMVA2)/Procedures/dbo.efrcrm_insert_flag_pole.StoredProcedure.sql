USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_flag_pole]    Script Date: 02/14/2014 13:06:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Flag_Pole
CREATE PROCEDURE [dbo].[efrcrm_insert_flag_pole] @Flag_Pole_ID int OUTPUT, @MDR_ID varchar(15) AS
begin

insert into Flag_Pole(MDR_ID) values(@MDR_ID)

select @Flag_Pole_ID = SCOPE_IDENTITY()

end
GO
