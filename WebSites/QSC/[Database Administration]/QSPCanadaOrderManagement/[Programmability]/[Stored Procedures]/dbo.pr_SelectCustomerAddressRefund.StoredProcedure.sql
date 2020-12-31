USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCustomerAddressRefund]    Script Date: 06/07/2017 09:20:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectCustomerAddressRefund]

	@iCustomerOrderHeaderInstance	INT,
	@iTransID						INT

AS

SELECT	ref.FirstName,
		ref.LastName,
		ref.Address1,
		ref.Address2,
		ref.Country,
		ref.City,
		ref.Province AS [State],
		ref.PostalCode AS Zip
FROM	QSPCanadaFinance..Refund ref
WHERE	ref.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		ref.TransID = @iTransID
GO
