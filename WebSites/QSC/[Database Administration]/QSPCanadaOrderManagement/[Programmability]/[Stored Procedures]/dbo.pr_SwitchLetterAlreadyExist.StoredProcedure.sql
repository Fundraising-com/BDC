USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterAlreadyExist]    Script Date: 06/07/2017 09:20:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SwitchLetterAlreadyExist]

	@iCustomerOrderHeaderInstance int =0,
	@iTransID int = 0,
	@iRemitBatchID int =0,
	@sTitleCode nvarchar(10) = '',
	@dFrom datetime ='',
	@dTo datetime =''
 AS

Declare @NbRow int,
	@iLastRemitBatchID int,
	@iCount int

declare  @Table table
	(
		customerorderheaderinstance int,
		transid int
	)

set @NbRow =0



SELECT top 1 @iLastRemitBatchID = RemitBatchID
   FROM CustomerOrderDetailRemitHistory  WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
 	 TransID = @iTransID
ORDER BY DateChanged Desc


if(@iCustomerOrderHeaderInstance <> 0 and @iTransID <> 0)
BEGIN


select @NbRow=count(*) from SwitchLetterBatchCustomerOrderDetail slbcod where slbcod.TransID=@iTransID and slbcod.CustomerOrderHeaderInstance= @iCustomerOrderHeaderInstance

END


else 
BEGIN

if(@iRemitBatchID <> 0 and @sTitleCode <> '' )
BEGIN
insert into @Table	 select 
			    codrh.customerorderheaderinstance,codrh.transid
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
			   and rb.RunID= @iRemitBatchID AND RemitCode=@sTitleCode  

	set @NbRow = @@rowcount
end

if(@dFrom <> '' and @dTo <> '')
BEGIN

	insert into @Table select cod.customerorderheaderinstance,cod.transid from customerorderdetail cod
				 where ProductCode = @sTitleCode and   
				cod.CreationDate  between  convert(nvarchar, @dFrom,101)  and convert(nvarchar, @dTo,101)
				

		set @NbRow = @@rowcount
END

declare c1 cursor for select CustomerOrderHeaderInstance, TransID from @Table
	    open c1
	    fetch next from c1 into @iCustomerOrderHeaderInstance, @iTransID
	    while @@fetch_status = 0
	    begin
		select @iCount = count(*) from SwitchLetterBatchCustomerOrderDetail slbcod where slbcod.TransID=@iTransID and slbcod.CustomerOrderHeaderInstance= @iCustomerOrderHeaderInstance
		set @NbRow = @NbRow - @iCount
		fetch next from c1 into @iCustomerOrderHeaderInstance, @iTransID
	    end
close c1
deallocate c1


--set @NbRow = (select count(*) from SwitchLetterBatch slb, CustomerOrderDetailRemitHistory codrh where slb.ProductCode = @sTitleCode and codrh.Status=42008 and codrh.RemitBatchID =@iLastRemitBatchID)
END


if(@NbRow = 0)
select 0
else
select 1
GO
