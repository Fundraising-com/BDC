select *
from product p
join pricing_details pd on pd.product_instance = p.product_instance
where p.product_code = '1062196'

select *
from qspcanadaordermanagement..customerorderdetail
where productcode in ('1061856','1061857','1061859','1062196')

select *
from ProductDescription
where PRODUCT_CODE in ('1061856','1061857','1061859','1062196')

begin tran
update qspcanadaordermanagement..customerorderdetail
set productcode = '1063294'
where productcode = '1061856'

update qspcanadaordermanagement..customerorderdetail
set productcode = '1063295'
where productcode = '1061857'

update qspcanadaordermanagement..customerorderdetail
set productcode = '1063297'
where productcode = '1061859'

update qspcanadaordermanagement..customerorderdetail
set productcode = '1063298'
where productcode = '1062196'


update product
set product_code = '1063294', OracleCode = '1063294'
where product_instance = 55170

update product
set product_code = '1063295', OracleCode = '1063295'
where product_instance = 55171

update product
set product_code = '1063297', OracleCode = '1063297'
where product_instance = 55172

update product
set product_code = '1063298', OracleCode = '1063298'
where product_instance = 55173


update PRICING_DETAILS
set product_code = '1063294', OracleCode = '1063294', FSCatalog_Product_Code = ''
where product_instance = 55170

update PRICING_DETAILS
set product_code = '1063295', OracleCode = '1063295', FSCatalog_Product_Code = ''
where product_instance = 55171

update PRICING_DETAILS
set product_code = '1063297', OracleCode = '1063297', FSCatalog_Product_Code = ''
where product_instance = 55172

update PRICING_DETAILS
set product_code = '1063298', OracleCode = '1063298', FSCatalog_Product_Code = ''
where product_instance = 55173


update ProductDescription
set PRODUCT_CODE = '1063294'
where PRODUCT_CODE = '1061856'

update ProductDescription
set PRODUCT_CODE = '1063295'
where PRODUCT_CODE = '1061857'

update ProductDescription
set PRODUCT_CODE = '1063297'
where PRODUCT_CODE = '1061859'

update ProductDescription
set PRODUCT_CODE = '1063298'
where PRODUCT_CODE = '1062196'

--commit tran
