select p.Product_Code, d.CatalogProductCode, p.OracleCode, *
from Product p
join productdescription d on d.product_code = p.oraclecode
where d.CatalogProductCode <> p.Product_Code
and p.Product_Year = 2016
and p.Product_Code not in ('TRT02','1060895','S1272','E6523')

begin tran
update d
set d.CatalogProductCode = p.Product_Code
from Product p
join productdescription d on d.product_code = p.oraclecode
where d.CatalogProductCode <> p.Product_Code
and p.Product_Year = 2016
and p.Product_Code not in ('TRT02','1060895','S1272','E6523')
--commit tran
