select p.OracleCode SAPMaterialCode, p.Product_Code ProductCode, p.Product_Sort_Name ProductName, SUM(Quantity) Units, SUM(Price) Retail, SUM(cod.Net) WholeSale
from qspcanadaordermanagement..customerorderdetail cod
join QSPCanadaFinance..INVOICE inv on inv.INVOICE_ID = cod.InvoiceNumber
join QSPCanadaOrderManagement..CustomerOrderHeader coh on coh.instance = cod.customerorderheaderinstance
join qspcanadaordermanagement..batch b on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
join QSPCanadaProduct..PRICING_DETAILS pd ON pd.magprice_instance = cod.pricingdetailsid
join qspcanadaproduct..Product p ON p.Product_instance = pd.Product_Instance
where inv.INVOICE_DATE between '2017-07-01' and '2017-12-31'
and cod.producttype = 46018
group by p.OracleCode, p.Product_Code, p.Product_Sort_Name
Order by p.Product_Code