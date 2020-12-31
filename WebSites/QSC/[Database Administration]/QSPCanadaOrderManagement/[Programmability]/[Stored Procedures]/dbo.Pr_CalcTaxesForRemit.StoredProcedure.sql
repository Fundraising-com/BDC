USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[Pr_CalcTaxesForRemit]    Script Date: 06/07/2017 09:19:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[Pr_CalcTaxesForRemit]

@RUNID  int

AS
set nocount on
--written by saqib shah,  Decmeber 2004
-- this proc calculate the tax amounts for Remit in CustomerOrderDetailRemitHistory   
--this proc should be executed  before AP run 

declare @Tax numeric(14,6), @Tax2 numeric(14,6)  
declare  @cohInstance int, @TransID int, @RemitAmount numeric(14,6), @State varchar(2),@TitleCode varchar(10) , @RemitBatchId int, @CustomerRemitHistoryInstance int 


Declare  c1 cursor for 
Select codrh.CustomerOrderHeaderInstance,codrh.Transid,codrh.RemitBatchID, codrh.CustomerRemitHistoryInstance,
Round(codrh.RemitRate * codrh.BasePrice,6)   as RemitAmount, crh.State,codrh.RemitCode
from QspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory as codrh,
     QspCanadaOrderManagement.dbo.RemitBatch rb,
     QSPCanadaOrderManagement..CustomerRemitHistory crh 
where codrh.remitbatchid = rb.id
and rb.runid = @RUNID 
and codrh.CustomerRemitHistoryInstance = crh.Instance
and codrh.status NOT in (42002,42003, 42004)  -- not cancelled
order by  codrh.CustomerOrderHeaderInstance,codrh.Transid



BEGIN


	Update  QspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory   --update the existing amounts to zero 
	set tax = 0, tax2 = 0
 	where remitbatchid in (Select distinct codrh.remitbatchid 
			         from  QspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory as codrh,  QspCanadaOrderManagement.dbo.RemitBatch rb 
			         where codrh.remitbatchid = rb.id
			            and rb.runid = @RUNID)


	OPEN C1

	    FETCH NEXT FROM C1    INTO @cohInstance , @TransID ,  @RemitBatchId, @CustomerRemitHistoryInstance ,@RemitAmount , @State , @TitleCode

	    WHILE @@FETCH_Status = 0
                  

               BEGIN
	

	    EXEC QspCanadaCommon.dbo.PR_CAN_TAX_CALC_TAX      @State, 'CA', '01/01/2004',  @RemitAmount, 2,@TitleCode,  @Tax   OUTPUT,@Tax2 OUTPUT





	   Update  QspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory
	   set tax = @tax, tax2 = @tax2
	   where CustomerOrderHeaderInstance  = @cohInstance
	   and  Transid = @TransID
                and  RemitBatchId  = @RemitBatchId
                and  CustomerRemitHistoryInstance = @CustomerRemitHistoryInstance
                 

	                            
	    FETCH NEXT FROM C1    INTO @cohInstance , @TransID ,  @RemitBatchId, @CustomerRemitHistoryInstance ,@RemitAmount , @State , @TitleCode

                END

	CLOSE C1
	DEALLOCATE C1

-- Madina 07/08/06 : inform application if errors encountered
IF (@@ERROR <> 0)
	SELECT 1
ELSE
	SELECT 0

END
GO
