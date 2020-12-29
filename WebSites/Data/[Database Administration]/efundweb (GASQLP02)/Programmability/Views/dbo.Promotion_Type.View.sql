USE [eFundweb]
GO
/****** Object:  View [dbo].[Promotion_Type]    Script Date: 02/14/2014 13:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Promotion_Type]
AS 
	SELECT Promotion_Type_Code, promotion_type_name as Description, 0 as Default_Commission_Rate
	from efrcommon..Promotion_Type
GO
