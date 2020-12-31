USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceOrdersNotProcessed]    Script Date: 06/07/2017 09:17:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan Martinez (juan_martinez@qsp.com)
-- Create date: 2009-10-08
-- Description:	Returns the number of orders not processed from an invoice.
-- =============================================
CREATE PROCEDURE [dbo].[GetInvoiceOrdersNotProcessed]

	@InvoiceID 	INT

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @OrderID INT
	SELECT	@OrderID = Order_ID
	FROM	Invoice
	WHERE	Invoice_ID = @InvoiceID

	-- CC Errors
	CREATE TABLE #OEFUCC
	(
		Classroom VARCHAR(255), TeacherName VARCHAR(255), ParticipantName VARCHAR(255), CustomerBillToInstance INT, SubscriberName VARCHAR(255), PurchaserName VARCHAR(255), BillToCustomerPhone VARCHAR(255), CustomerPhone VARCHAR(255), CustomerAddress1 VARCHAR(255), CustomerAddress2 VARCHAR(255), CustomerCity VARCHAR(255), CustomerState VARCHAR(255), CustomerZip VARCHAR(255), TitleCode VARCHAR(255), MagazineTitle VARCHAR(255), Numberofissues INT, CatalogPrice NUMERIC(16,2), Lang VARCHAR(255), StatusInstance INT
	)

	INSERT INTO #OEFUCC
	EXEC QSPCanadaOrderManagement..pr_OrderEntryFollowupReport_CC @OrderID

	DECLARE @CCErrors INT
	SET @CCErrors = @@ROWCOUNT
					/*(	
						FROM	Invoice inv
								JOIN QSPCanadaOrderManagement..Batch b ON b.OrderID = inv.Order_ID
								JOIN QSPCanadaOrderManagement..CustomerOrderHeader coh ON coh.OrderBatchID = b.ID AND coh.OrderBatchDate = b.Date
								JOIN QSPCanadaOrderManagement..CustomerPaymentHeader cph ON cph.CustomerOrderHeaderInstance = coh.Instance
								JOIN QSPCanadaOrderManagement..CreditCardPayment ccp ON ccp.CustomerPaymentHeaderInstance = cph.Instance
						WHERE	b.StatusInstance <> 40005 --40005: Cancelled
								AND ccp.StatusInstance IN (19001, 19002, 19005) --19001: Credit card payment error, 19002: Credit card payment over house limit, 19005: Credit card zero amount
								AND inv.Invoice_ID = @InvoiceID
								)*/

	-- Orders Not Fulfilled
	CREATE TABLE #OEFUMag
	(
		Classroom VARCHAR(255), TeacherName VARCHAR(255), ParticipantName VARCHAR(255), CustomerBillToInstance INT, StatusInstance INT, SubscriberName VARCHAR(255), PurchaserName VARCHAR(255), CustomerPhone VARCHAR(255), CustomerAddress1 VARCHAR(255), CustomerAddress2 VARCHAR(255), CustomerCity VARCHAR(255), CustomerState VARCHAR(255), CustomerZip VARCHAR(255), TitleCode VARCHAR(255), MagazineTitle VARCHAR(255), Numberofissues INT, CatalogPrice NUMERIC(16,2), Lang VARCHAR(255), CustomerOrderHeaderInstance INT, TransID INT, InvoiceNumber INT, ErrorCategory VARCHAR(255), ErrorCategory_FR VARCHAR(255), ErrorType VARCHAR(255), ErrorType_FR VARCHAR(255)
	)

	DECLARE @MagErrors INT
	INSERT INTO #OEFUMag
	EXEC QSPCanadaOrderManagement..pr_OrderEntryFollowupReport_Mag @OrderID
	SELECT	@MagErrors = COUNT(*)
	FROM	#OEFUMag
	WHERE	InvoiceNumber > 0 --Only include items that were invoiced
	

	CREATE TABLE #OEFUGift
	(
		Classroom VARCHAR(255), TeacherName VARCHAR(255), ParticipantName VARCHAR(255), CustomerBillToInstance INT, PurchaserName VARCHAR(255), CustomerPhone VARCHAR(255), CatalogPrice NUMERIC(16,2), Quantity INT, Price NUMERIC(16,2), OrderID INT, Lang VARCHAR(255)
	)
	DECLARE @GiftErrors INT
	INSERT INTO #OEFUGift
	EXEC QSPCanadaOrderManagement..pr_OrderEntryFollowupReport_Gift @OrderID
	SET @GiftErrors = @@ROWCOUNT

	DECLARE @NotFulfilled INT
	SET @NotFulfilled = @MagErrors + @GiftErrors
					/*(	SELECT	COUNT(*)
						FROM	Invoice inv
								JOIN QSPCanadaOrderManagement..Batch b ON b.OrderID = inv.Order_ID
								JOIN QSPCanadaOrderManagement..CustomerOrderHeader coh ON coh.OrderBatchID = b.ID AND coh.OrderBatchDate = b.Date
								JOIN QSPCanadaOrderManagement..CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = coh.Instance
						WHERE	b.StatusInstance <> 40005 --40005: Cancelled
								AND cod.DelFlag <> 1
								AND cod.StatusInstance IN (512, 513, 514, 517) --512: Unremittable, 513: Unshippable, 514: Library Order, 517: TV Week
								AND inv.Invoice_ID = @InvoiceID
								)*/

	SELECT ISNULL(@CCErrors, 0) AS ccerrors, ISNULL(@NotFulfilled, 0) AS notfulfilled

END
GO
