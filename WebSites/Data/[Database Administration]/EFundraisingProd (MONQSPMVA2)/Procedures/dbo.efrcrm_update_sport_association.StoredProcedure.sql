USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sport_association]    Script Date: 02/14/2014 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sport_Association
CREATE PROCEDURE [dbo].[efrcrm_update_sport_association] @Sport_Association_Id int, @Sport_Ass_Desc varchar(50) AS
begin

update Sport_Association set Sport_Ass_Desc=@Sport_Ass_Desc where Sport_Association_Id=@Sport_Association_Id

end
GO
