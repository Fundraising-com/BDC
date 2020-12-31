USE QSPCanadaCommon
GO

select c.BillToAccountID, c.ID CampaignID
into #c
from campaign c
where c.StartDate >= '2017-01-01'
and c.Status = 37002
and c.billtoaccountid in (

	select acc.Id
	from QSPCanadaCommon..Campaign camp
	join QSPCanadaCommon..CAccount acc ON acc.Id = camp.BillToAccountID
	where (camp.DateModified >= DATEADD(dd, -2, getdate())
	or acc.DateUpdated >= DATEADD(dd, -2, getdate())
	or camp.StartDate BETWEEN DATEADD(dd, -2, getdate()) AND getdate())
	and camp.StartDate >= '2017-07-01'
	and camp.Status = 37002
)
order by c.BillToAccountID, c.ID

DECLARE @XYList AS varchar(MAX) -- Leave as NULL
 
SELECT @XYList = COALESCE(@XYList + ',', '') + CONVERT(nvarchar(20), CampaignID)
FROM #c

select @XYList

drop table #c


----

select ISNULL(p.RemitCode, p.OracleCode), p.Type ProductType, p.CommentDate ProductDateChanged, pd.Log_Dt PricingDetailChanged, p.Status ProductStatus, pd.Status PricingDetailStatus, p.Product_Sort_Name, *
from QSPCanadaProduct..Pricing_details pd
join qspcanadaproduct..Product p ON p.Product_Instance = pd.Product_Instance
where (p.CommentDate >= DATEADD(dd, -7, getdate())
or pd.Log_Dt >= DATEADD(dd, -7, getdate()))
and p.Type not in (46008)
order by p.CommentDate desc

select p.remitcode, *
from qspcanadaproduct..pricing_detailsaudit a
join qspcanadaproduct..pricing_details pd on pd.magprice_instance = a.magprice_instance
join qspcanadaproduct..product p on p.product_instance = pd.product_instance
where auditdate >= DATEADD(dd, -7, getdate())
order by auditdate desc
