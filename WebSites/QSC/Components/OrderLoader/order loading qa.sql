
select top 10 cod.*
--,batch.campaignid
--,quantity, quantityshipped,customerorderdetail.statusinstance, customerorderdetail.*
--distinct customerorderdetail.statusinstance
from batch,customerorderheader coh
left join customerpaymentheader cph on cph.customerorderheaderinstance = coh.instance
left join creditcardpayment ccp on ccp.customerpaymentheaderinstance = cph.instance
left join student s on s.instance = coh.studentinstance
left join customer billto on billto.instance = coh.customerbilltoinstance
,customerorderdetail cod
left join customer shipto on cod.customershiptoinstance=shipto.instance
 where 
orderbatchdate=date and orderbatchid=id
and cod.customerorderheaderinstance=coh.instance
--and ke3filename like '%QSPCanada2012_10_28_07_25_00%'

and date='10/28/12'
and startimporttime >'11/26/12'


order by orderid desc


spGetProductCodeFromRemitCodeAndTerm '8422', 24, 82703

Select * from qspcanadaproduct.dbo.pricing_details where product_code='9521' and pricing_year = 2013