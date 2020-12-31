USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_CurrentPremiums]    Script Date: 06/07/2017 09:18:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CurrentPremiums] AS
Select  pd.product_code , 
	Max(qsppremiumId) Premium,
	Product_sort_name,
	Case soundex(product_sort_name)
	When soundex('Reader''s Digest') Then 'RD'
	Else 'ROGER'
	END Type
from 	QSPcanadaproduct..pricing_Details pd, 
	qspcanadaproduct..product p
where (pd.product_code=p.product_code )--or pd.product_Instance=p.product_instance)
and qsppremiumId > 0
and (soundex(product_sort_name) like soundex('Reader''s Digest')OR
      soundex(product_sort_name)=soundex('Chatelaine') 		OR
      soundex(product_sort_name)=soundex('Flare') 	  	OR
      soundex(product_sort_name)=soundex('Maclean''s')
    )
Group By pd.product_code,product_sort_name
GO
