USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertisements]    Script Date: 02/14/2014 13:03:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Advertisement
CREATE PROCEDURE [dbo].[efrcrm_get_advertisements] AS
begin

select Advertisement_id, Division_id, Description, Size, Nb_colors, Comments from Advertisement

end
GO
