select		ost.ToteID, MAX(b.OrderID) OrderID,
			MAX(ReceiptDate) LicensePlatedInNashvilleDate, MAX(TransmitDate) ExportedToFFSDate, MAX(ost.DateInvoiced) InvoiceDate,
			DATEDIFF(dd, MAX(ReceiptDate), MAX(TransmitDate)) NumberDaysInFocus,
			DATEDIFF(dd, MAX(TransmitDate), MAX(DateInvoiced)) NumberDaysInFFS,
			CASE MAX(b.OrderQualifierID) WHEN 39002 THEN 'Supplementary Order' ELSE 'Main Order' END OrderType
from		OrderStageTracking ost
left join	Batch b on b.OrderID = ost.OrderID
where		ost.StageDate >= '2014-07-15'
and			ost.ToteID > 0
group by	ost.ToteID
having		MAX(ost.DateInvoiced) IS NOT NULL
order by	ost.ToteID
