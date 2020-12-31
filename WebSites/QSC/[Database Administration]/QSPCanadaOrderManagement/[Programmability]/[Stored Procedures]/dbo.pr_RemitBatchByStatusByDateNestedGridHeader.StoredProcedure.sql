USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitBatchByStatusByDateNestedGridHeader]    Script Date: 06/07/2017 09:20:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_RemitBatchByStatusByDateNestedGridHeader] 

@iStatus int,
@daDateFrom datetime = null,
@daDateTo datetime = null


AS


if(@daDateFrom = null or @daDateTo = null)
Begin

select 	x.FormattedDate as date ,
	sum(x.RemitTotal) as RemitTotal,
	sum(x.TotalBasePrice) as TotalBasePrice,
	sum(x.TotalCatalogPrice) as TotalCatalogPrice,
	sum(x.TotalItemPrice) as TotalItemPrice,
	sum(x.TotalUnits) as TotalUnits 
from (
select 
LEFT( CONVERT(varchar, [Date], 120), 10) as FormattedDate,
 count(*) as RemitTotal, 
sum(TotalBasePrice) as TotalBasePrice,
sum(TotalCatalogPrice) as TotalCatalogPrice,
sum(TotalItemPrice) TotalItemPrice,
sum(TotalUnits) as TotalUnits  

from RemitBatch  
where  RemitBatch.Status = @iStatus
group by date
) x group by x.FormattedDate

end

else
Begin
select 	x.FormattedDate as date ,
	sum(x.RemitTotal) as RemitTotal,
	sum(x.TotalBasePrice) as TotalBasePrice,
	sum(x.TotalCatalogPrice) as TotalCatalogPrice,
	sum(x.TotalItemPrice) as TotalItemPrice,
	sum(x.TotalUnits) as TotalUnits 
from (
select 
LEFT( CONVERT(varchar, [Date], 120), 10) as FormattedDate, 
count(*) as RemitTotal, 
sum(TotalBasePrice) as TotalBasePrice,
sum(TotalCatalogPrice) as TotalCatalogPrice,
sum(TotalItemPrice) TotalItemPrice,
sum(TotalUnits) as TotalUnits  

from RemitBatch  
where  RemitBatch.Status = @iStatus and  LEFT( CONVERT(varchar, [Date], 120), 10) between LEFT( CONVERT(varchar,@daDateFrom, 120), 10) and LEFT( CONVERT(varchar, @daDateTo, 120), 10)
group by date
) x 
group by x.FormattedDate

end
GO
