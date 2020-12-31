DECLARE @zProcessingFeeProductCode varchar(20);
DECLARE @zComment varchar(255)

/* 	Using the comment AND ProductCode for deletes should insure that we only delete the entries created 
	for tests. These 2 values need to be updated if the CreateFeeProduct.sql or CreateTestFees.sql
	scripts are changed to alter either the comment included in COD transactions, or the overall product code for processing fees */
SET @zComment = 'Processing fee for tests';
SET @zProcessingFeeProductCode = 'PFEE';

/*	Reset the NextTransID to bring it back to the value before processing fees were added to the order */
UPDATE [QSPCanadaOrderManagement].[dbo].[CustomerOrderHeader]
SET NextDetailTransID = NextDetailTransID - 1
WHERE Instance IN 
	(SELECT CustomerOrderHeaderInstance 
	 FROM [QSPCanadaOrderManagement].[dbo].[CustomerOrderDetail] 
	 WHERE ProductCode = @zProcessingFeeProductCode and Comment = @zComment)

/*	Delete the actual test fees */
DELETE FROM [QSPCanadaOrderManagement].[dbo].[CustomerOrderDetail] 
WHERE ProductCode = @zProcessingFeeProductCode and Comment = @zComment;