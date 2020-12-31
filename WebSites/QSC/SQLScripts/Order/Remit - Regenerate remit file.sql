USE [QSPCanadaOrderManagement]

SELECT	top 100 *
FROM	RemitBatch rb
JOIN	CustomerOrderDetailRemitHistory codrh
			ON	codrh.RemitBatchID = rb.ID
WHERE	rb.RunID IN (1453)
AND		rb.fulfillmenthousenbr = 11

commit tran
update qspcanadaproduct..fulfillment_house
set interfacelayoutid = 33009, interfacemediaid = 32003--interfacelayoutid = 33002, interfacemediaid = 32003
where ful_nbr = 29

select * from qspcanadaproduct..product where remitcode = '3187'
select * from qspcanadaproduct..fulfillment_house where ful_name like 'cds%'
--If you need to recreate the remit file
pr_RemitTest_ProductSeason_Fix 1300
pr_RemitTest_CheckTitleAndFH_Fix 1300
pr_RemitTest_EffortKey_Fix 1300
pr_RemitTest_NbrOfIssues_Fix 1300
pr_RemitTest_Cancelled_Fix 1300
pr_CalcTaxesForRemit 1300
pr_RemitTest_BasePrice_RemitRate_Fix 1345
pr_RemitTest_Premium_Fix 1345
pr_RemitTest_RemitCode_Fix 1396
EXEC ReprocessRemitBatchByRemitBatch 236138

--If you need to recreate AP
EXEC QSPCanadaOrderManagement..AP_Remit_SendAP 1272, '1140'