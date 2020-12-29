USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[promotion_type]    Script Date: 02/14/2014 13:02:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from _tbd_promotion_type 

CREATE VIEW [dbo].[promotion_type] 
AS 
	select Promotion_Type_Code, promotion_type_name as Description, 0 as Default_Commission_Rate, 1 as Channel
	from efrcommon..promotion_type
GO
