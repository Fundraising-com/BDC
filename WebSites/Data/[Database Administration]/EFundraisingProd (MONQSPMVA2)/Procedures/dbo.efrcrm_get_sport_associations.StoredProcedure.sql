USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sport_associations]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sport_Association
CREATE PROCEDURE [dbo].[efrcrm_get_sport_associations] AS
begin

select Sport_Association_Id, Sport_Ass_Desc from Sport_Association

end
GO
