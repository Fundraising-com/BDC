USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Lead_Personalized_Old]    Script Date: 02/14/2014 13:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Lead_Personalized    Script Date: 2003-02-22 20:34:17 ******/


/****** Object:  View dbo.Lead_Personalized    Script Date: 2/11/2003 12:27:44 PM ******/
create view [dbo].[Lead_Personalized_Old] as /* view-column-name, ... */
  select Email as GoodEmail from Administrative_Email union
  select GoodEmail from Efund_New_Letter_Leads
GO
