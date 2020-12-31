USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_CurrentRogerPremiums]    Script Date: 06/07/2017 09:18:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CurrentRogerPremiums] AS
Select  pd.product_code , 
	Max(qsppremiumId) RogersPremium,
	Product_sort_name
from 	QSPcanadaproduct..pricing_Details pd, 
	qspcanadaproduct..product p
where (pd.product_code=p.product_code )
and qsppremiumId > 0
and  (
	 soundex(product_sort_name)=soundex('Chatelaine') OR
	 soundex(product_sort_name)=soundex('Flare') 	  OR
	 soundex(product_sort_name)=soundex('Maclean''s')
     )
Group By pd.product_code,product_sort_name
GO
