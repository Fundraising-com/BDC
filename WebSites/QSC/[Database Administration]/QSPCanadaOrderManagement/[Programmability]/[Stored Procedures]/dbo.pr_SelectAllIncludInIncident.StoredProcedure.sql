USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllIncludInIncident]    Script Date: 06/07/2017 09:20:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectAllIncludInIncident]
 AS

select distinct Instance,FirstName,LastName from qspcanadacommon..cuserprofile cu,Incident inc where cu.instance = inc.useridcreated
GO
