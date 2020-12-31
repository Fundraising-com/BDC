USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterSelectReportPreview_old]    Script Date: 06/07/2017 09:20:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SwitchLetterSelectReportPreview_old]
	@sTitleCode varchar(10),
	@iRemitBatchID  int =0,
	@dFrom datetime = '',
	@dTo datetime = ''
AS



DECLARE  @iCount int,
	@iCustomerOrderHeaderInstance int,
	@iTransID int

declare	@Table TABLE
	(
		UniqueID int IDENTITY(1,1),
		LastName varchar(50),
    		FirstName varchar(50),
     		Street1 varchar(50),
		Street2 varchar(50),
		City varchar(50),
		province varchar(50),
		postalcode varchar(50),
		Country varchar(50),
		MagazineTitle varchar(50),
		NbIssues int,
		Amount int,
		CustomerOrderHeaderInstance int,
		TransID int ,
		remitbatchid int,
		Language varchar(2)
	)



if @iRemitBatchID <> 0 
begin
insert into @Table
	(LastName,
   	FirstName,
     	Street1 ,
	Street2,
	City ,
	province,
	postalcode,
	Country ,
	MagazineTitle ,
	NbIssues,
	Amount,
	CustomerOrderHeaderInstance,
	TransID,
	remitbatchid,
	Language)
select 
	crh.LastName,
    	crh.FirstName,
     	crh.Address1 as Street1,
	crh.Address2 as Street2,
	crh.City,
	crh.state as province,
	crh.zip as postalcode,
	'Canada' as Country,
	codrh.MagazineTitle,
	codrh.Quantity-codrh.NumberOfissues	as NbIssues,
	0 as Amount,
	codrh.CustomerOrderHeaderInstance,
	codrh.TransID,
	codrh.remitbatchid,
	case c.state when 'QC' then 'fr' else 'en' end as Language
from 

dbo.RemitBatch rb,
dbo.CustomerRemitHistory crh,
dbo.Customer c,
dbo.CustomerOrderDetailRemitHistory  codrh

where 

   crh.RemitBatchId = rb.Id
   and codrh.RemitBatchId = crh.RemitBatchId
   and crh.CustomerInstance = c.Instance
   and codrh.CustomerRemitHistoryInstance = crh.Instance
   and crh.instance = (select max(instance) from customerremithistory x where x.customerinstance= c.instance)
   and codrh.TitleCode = @sTitleCode
   and rb.RunID = @iRemitBatchID

end
else
begin

insert into @Table
	(LastName,
   	FirstName,
     	Street1 ,
	Street2,
	City ,
	province,
	postalcode,
	Country ,
	MagazineTitle ,
	NbIssues,
	Amount,
	CustomerOrderHeaderInstance,
	TransID,
	remitbatchid,
	Language)
 select 
	crh.LastName,
    	crh.FirstName,
     	crh.Address1 as Street1,
	crh.Address2 as Street2,
	crh.City,
	crh.state as province,
	crh.zip as postalcode,
	'Canada' as Country,
	codrh.MagazineTitle,
	codrh.Quantity-codrh.NumberOfissues	as NbIssues,
	cod.price as Amount,
	codrh.CustomerOrderHeaderInstance,
	codrh.TransID,
	codrh.remitbatchid,
	case c.state when 'QC' then 'fr' else 'en' end as Language
from 
dbo.RemitBatch rb,
dbo.CustomerRemitHistory crh,
dbo.CustomerOrderHeader coh,
dbo.Customer c,
dbo.Batch b,
dbo.CustomerOrderDetailRemitHistory  codrh,
dbo.CustomerOrderDetail cod

where 

   crh.RemitBatchId = rb.Id
   and codrh.RemitBatchId = crh.RemitBatchId
   and codrh.CustomerRemitHistoryInstance = crh.Instance
   and codrh.CustomerOrderHeaderInstance = coh.Instance
   and coh.OrderBatchDate = b.[Date]
   and coh.OrderBatchID = b.ID
   and crh.CustomerInstance = c.Instance 
   and crh.instance = (select max(instance) from customerremithistory x where x.customerinstance= c.instance)
   and codrh.TitleCode = @sTitleCode 
   and cod.CreationDate  between  convert(nvarchar, @dFrom,101)  and convert(nvarchar, @dTo,101)
   and cod.CustomerOrderheaderinstance = codrh.CustomerOrderheaderinstance
   and cod.transid = codrh.transid
   and cod.CustomerOrderHeaderInstance=codrh.CustomerOrderHeaderInstance 
   and cod.TransID= codrh.TransID
end
	
declare c1 cursor for select CustomerOrderHeaderInstance, TransID from @table
	    open c1
	    fetch next from c1 into @iCustomerOrderHeaderInstance, @iTransID
	    while @@fetch_status = 0
	    begin
		select @iCount = count(*) from SwitchLetterBatchCustomerOrderDetail slbcod where slbcod.TransID=@iTransID and slbcod.CustomerOrderHeaderInstance= @iCustomerOrderHeaderInstance
		if(@iCount =1)
		BEGIN
			delete from @table where CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance  and TransID = @iTransID
		END
		


		fetch next from c1 into @iCustomerOrderHeaderInstance, @iTransID
	    end
close c1
deallocate c1


 select * from @Table
GO
