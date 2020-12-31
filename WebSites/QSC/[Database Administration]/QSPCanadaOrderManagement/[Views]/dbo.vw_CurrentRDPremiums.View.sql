USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_CurrentRDPremiums]    Script Date: 06/07/2017 09:18:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CurrentRDPremiums] AS
Select  pd.product_code , 
		Max(qsppremiumId) RDPremium,
		Product_sort_name
from 	QSPcanadaproduct..pricing_Details pd, 
	qspcanadaproduct..product p
where (pd.product_code=p.product_code )--or pd.product_Instance=p.product_instance)
and qsppremiumId > 0
and soundex(product_sort_name) like soundex('Reader''s Digest')
Group By pd.product_code,product_sort_name
GO
