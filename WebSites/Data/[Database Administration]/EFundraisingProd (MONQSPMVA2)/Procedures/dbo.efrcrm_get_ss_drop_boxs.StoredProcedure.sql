USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ss_drop_boxs]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for SS_Drop_Box
CREATE PROCEDURE [dbo].[efrcrm_get_ss_drop_boxs] AS
begin

select SS_Drop_Box_Id, SS_Drop_Box_Name, Display_Order from SS_Drop_Box

end
GO
