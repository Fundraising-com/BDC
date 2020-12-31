select	SUM(iSec.Total_Tax_Included - ISNULL(iSec.US_Postage_Amount, 0.00)) RetailAmount,
		SUM(iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) NetAmount,
		SUM(iSec.Total_Tax_Excluded - ISNULL(iSec.US_Postage_Amount, 0.00)) AdjustedGross,
		SUM(iSec.ITEM_COUNT) NumUnits,
		SUM(iSec.GROUP_PROFIT_AMOUNT) GroupProfitAmount
from INVOICE_SECTION iSec
join invoice i on i.invoice_id = iSec.invoice_id
where iSec.section_type_id = 9 --Cookie Dough
and i.invoice_date between '2017-07-01' and '2017-12-31'

select	fm.Firstname + ' ' + fm.Lastname FM, acc.ID AccountID, acc.Name AccountName,
		coh.Instance OrderHeaderInstance,
		(SELECT	ISNULL(SUM(cod2.Price), 0.00)
		FROM	qspcanadaordermanagement..CustomerOrderDetail cod2
		WHERE	cod2.CustomerOrderHeaderInstance = coh.Instance
		AND		cod2.ProductType IN (46021)
		AND		cod2.DelFlag = 0
		AND		cod2.Recipient = cod.Recipient
		AND		cod2.ProductCode = 'SHIPFEE') OrderShippingFee,
		p.oraclecode SAPMaterialNumber, cd.Description ProductType, case cod.IsShippedToAccount WHEN 1 THEN 'AccountDelivered' ELSE 'CustomerDelivered' END AccountDelivery, cod.productcode, cod.productname, 
		(CASE WHEN cod.ProductType = 46001 THEN 1 ELSE cod.quantity END) Units, (cod.price) RetailAmount, cod.Net WholeSaleAmount, cod.GroupProfitAmount --(case acc.CAccountCodeClass when 'FM' THEN 0.00 ELSE cod.Net END) WholeSaleAmount, (case acc.CAccountCodeClass when 'FM' THEN 0.00 ELSE cod.GroupProfitAmount END) GroupProfitAmount
from qspcanadaordermanagement..customerorderdetail cod
join QSPCanadaOrderManagement..CustomerOrderHeader coh on coh.instance = cod.customerorderheaderinstance
join qspcanadaordermanagement..batch b on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
join qspcanadacommon..campaign c on c.id = b.campaignid
join qspcanadacommon..caccount acc on acc.id = c.billtoaccountid
join QSPCanadaFinance..INVOICE inv on inv.INVOICE_ID = cod.InvoiceNumber
join QSPCanadaCommon..FieldManager fm on fm.fmid = c.fmid
join QSPCanadaProduct..PRICING_DETAILS pd on pd.MagPrice_Instance = cod.PricingDetailsID
join QSPCanadaProduct..Product p on p.product_instance = pd.product_instance
join qspcanadacommon..CodeDetail cd on cd.Instance = cod.producttype
where inv.INVOICE_DATE between '2017-07-01' and '2017-12-31'
--and cod.producttype = 46018
and b.orderqualifierid = 39009
and cod.ProductType not in (46017, 46021)
--group by coh.Instance, p.OracleCode, cd.Description, cod.IsShippedToAccount, cod.ProductCode, cod.ProductName, fm.Firstname + ' ' + fm.Lastname, acc.ID, acc.Name
order by coh.Instance, cd.Description, p.OracleCode, cod.ProductCode, cod.IsShippedToAccount

select top 99 b.orderqualifierid, *
from INVOICE_SECTION iSec
join invoice i on i.invoice_id = iSec.invoice_id
join QSPCanadaOrderManagement..Batch b on b.orderid = i.order_ID
where iSec.section_type_id = 9 --Cookie Dough
and i.invoice_date between '2014-07-01' and '2015-04-01'
order by i.INVOICE_ID desc


select	fm.Firstname + ' ' + fm.Lastname FM, acc.ID AccountID, acc.Name AccountName,
		SUM(case when i.invoice_date <= '2017-07-01' then iSec.ITEM_COUNT else 0 end) FA16Units,
		SUM(case when i.invoice_date >= '2017-07-01' then iSec.ITEM_COUNT else 0 end) FA17Units,
		SUM(case when i.invoice_date <= '2017-01-01' then iSec.Total_Tax_Included - ISNULL(iSec.US_Postage_Amount, 0.00) else 0.00 end) FA16Retail,
		SUM(case when i.invoice_date >= '2017-01-01' then iSec.Total_Tax_Included - ISNULL(iSec.US_Postage_Amount, 0.00) else 0.00 end) FA17Retail,
		SUM(case when i.invoice_date <= '2017-01-01' then iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00) else 0.00 end) FA16NetAmount,
		SUM(case when i.invoice_date >= '2017-01-01' then iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00) else 0.00 end) FA17NetAmount,
		SUM(case when i.invoice_date <= '2017-01-01' then iSec.GROUP_PROFIT_AMOUNT else 0.00 end) FA16GroupProfitAmount,
		SUM(case when i.invoice_date >= '2017-01-01' then iSec.GROUP_PROFIT_AMOUNT else 0.00 end) FA17GroupProfitAmount
from	INVOICE_SECTION iSec
join	invoice i on i.invoice_id = iSec.invoice_id
join	qspcanadaordermanagement..batch b on b.orderid = i.order_id
join	qspcanadacommon..campaign c on c.id = b.campaignid
join	qspcanadacommon..caccount acc on acc.id = c.billtoaccountid
join	QSPCanadaCommon..FieldManager fm on fm.fmid = c.fmid
where	iSec.section_type_id = 9 --Cookie Dough
and		(i.invoice_date between '2016-07-01' and '2016-12-31' or i.invoice_date between '2017-07-01' and '2017-12-31')
and		QSPCanadaOrderManagement.[dbo].[UDF_IsCampaignCombo](c.ID) = 0
and		acc.caccountcodeclass <> 'FM'
group by fm.Firstname + ' ' + fm.Lastname, acc.ID, acc.Name
having	SUM(case when i.invoice_date <= '2017-07-01' then iSec.ITEM_COUNT else 0 end) > 0
and		SUM(case when i.invoice_date >= '2017-07-01' then iSec.ITEM_COUNT else 0 end) > 0
order by fm.Firstname + ' ' + fm.Lastname, acc.ID, acc.Name