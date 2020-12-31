USE QSPCanadaProduct
GO

select p.Type, *
from ProductDescription pd
join Product p on p.OracleCode = pd.PRODUCT_CODE
where p.PRODUCT_CODE <> pd.CatalogProductCode
and p.Product_Year = 2017
order by p.Type

begin tran
update pd
set catalogproductcode = p.product_code
from ProductDescription pd
join Product p on p.OracleCode = pd.PRODUCT_CODE
where p.PRODUCT_CODE <> pd.CatalogProductCode
and p.Product_Year = 2017
and p.Type in (46008, 46013, 46014)
--commit tran
