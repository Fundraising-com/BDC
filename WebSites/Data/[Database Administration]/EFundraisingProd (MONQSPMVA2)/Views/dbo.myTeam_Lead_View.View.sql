USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[myTeam_Lead_View]    Script Date: 02/14/2014 13:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.myTeam_Lead_View    Script Date: 2003-02-22 20:34:18 ******/


/****** Object:  View dbo.myTeam_Lead_View    Script Date: 2/11/2003 12:27:44 PM ******/

create view [dbo].[myTeam_Lead_View]
  as select Lead.*,promotion.description from(Lead join Promotion on Lead.Promotion_ID = Promotion.Promotion_ID) join Partner on Promotion.Partner_ID = Partner.Partner_ID where Partner.Partner_ID = 1
GO
