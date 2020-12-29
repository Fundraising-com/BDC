USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[efr_Lead_View]    Script Date: 02/14/2014 13:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.efr_Lead_View    Script Date: 2003-02-22 20:34:18 ******/


/****** Object:  View dbo.efr_Lead_View    Script Date: 2/11/2003 12:27:44 PM ******/
create view [dbo].[efr_Lead_View] /* view column name,... */
  as select eFR_Lead.First_Name,eFR_Lead.Organization_Name,eFR_Lead.Lead_Activity_Detail,eFR_Lead.Activity_Scheduled_Date,
    eFR_Lead.Consultant_Ext,eFR_Lead.Lead_ID,eFR_Lead.Last_Name,eFR_Lead.Promotion_Description,eFR_Lead.Lead_Comment,
    eFR_Lead.Consultant_ID,eFR_Lead.Promotion_Type,eFR_Lead.Is_Done,eFR_Lead."2ndPhone_Extension" as Phone_Extension2,eFR_Lead.Phone_Number,
    eFR_Lead.phone_extension,eFR_Lead."2ndPhone_Number" as Phone_Number2 from
    eFR_Lead
GO
