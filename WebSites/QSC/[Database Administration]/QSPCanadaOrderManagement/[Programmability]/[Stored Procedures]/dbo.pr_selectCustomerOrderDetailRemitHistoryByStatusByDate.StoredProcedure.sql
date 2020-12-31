USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_selectCustomerOrderDetailRemitHistoryByStatusByDate]    Script Date: 06/07/2017 09:20:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_selectCustomerOrderDetailRemitHistoryByStatusByDate]
			         

@iStatus  int,
@daDateFrom datetime = null,
@daDateTo datetime = null


AS

if(@daDateFrom = null or @daDateTo = null)
BEGIN

select codrh.*, crh.firstname, crh.lastname, codrh.status, cd.Description as codedetail,rb.date
from CustomerOrderDetailRemitHistory codrh , customerremithistory crh, CodeDetail cd, remitbatch rb
where rb.id = crh.remitbatchid 
  and crh.instance = codrh.customerremithistoryinstance
  and codrh.status = cd.instance
  and rb.status= @iStatus
END
else
BEGIN

select codrh.*, crh.firstname, crh.lastname, codrh.status, cd.Description as codedetail,rb.date
from CustomerOrderDetailRemitHistory codrh , customerremithistory crh, CodeDetail cd, remitbatch rb
where rb.id = crh.remitbatchid 
  and crh.instance = codrh.customerremithistoryinstance
  and codrh.status = cd.instance
  and rb.status= @iStatus
 and  rb.Date between @daDateFrom and @daDateTo

print @daDateFrom
print @daDateTo
END
GO
