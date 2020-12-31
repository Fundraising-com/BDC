USE [QSPCanadaOrderManagement]

CREATE TABLE #Temp
(
	OrderID int,
	FSOrder	bit
)

INSERT #Temp
select		OrderID,
			FSOrder = 
			CASE OrderQualifierID 
				WHEN 39007 THEN 1
				ELSE 0
			END
from		QSPCanadaOrderManagement..batch b
--left join	QSPCanadaOrderManagement..customerorderheader coh on coh.orderbatchdate = b.date and coh.orderbatchid = b.id
--left join	QSPCanadaOrderManagement..customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
--where		cod.customerorderheaderinstance is null and b.statusinstance <> 40005 and b.date > '2011-07-01'
WHERE		b.orderID IN (701250, 701251)



DECLARE	@OrderID			INT
DECLARE @IsFieldSupplyOrder	BIT
DECLARE	@SentForShipment	BIT

SET	@SentForShipment = 1


DECLARE cOrderToCancel cursor for 
	SELECT OrderID, FSOrder
	FROM #Temp
	
Open cOrderToCancel;
fetch NEXT from cOrderToCancel into @OrderID, @IsFieldSupplyOrder;

while @@fetch_status = 0
BEGIN
		UPDATE	QSPCanadaOrderManagement..batch
		SET		batch.StatusInstance = 40005
		FROM	QSPCanadaOrderManagement..Batch b
		WHERE	orderID = @OrderID


		UPDATE	cod
		SET		cod.DelFlag = 1
		FROM	QSPCanadaOrderManagement..CustomerOrderDetail cod
		JOIN	QSPCanadaOrderManagement..CustomerOrderHeader coh
					ON	coh.Instance = cod.CustomerOrderHeaderInstance
		JOIN	QSPCanadaOrderManagement..Batch b
					ON	b.ID = coh.OrderBatchID
					AND	b.Date = coh.OrderBatchDate
		WHERE	b.OrderID = @OrderID

		UPDATE	QSPCanadaCommon..SystemErrorLog
		SET		IsReviewed = 1,
				IsFixed = 1
		WHERE	OrderID = @OrderID

		IF @SentForShipment = 1
		BEGIN
			DELETE	srcod
			FROM	QSPCanadaOrderManagement..ShipmentRequestOrder sro
			JOIN	QSPCanadaOrderManagement..ShipmentRequestCustomerOrderHeader srcoh
						ON	srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
			JOIN	QSPCanadaOrderManagement..ShipmentRequestCustomerOrderDetail srcod
						ON	srcod.ShipmentRequestCustomerOrderHeaderID = srcoh.ShipmentRequestCustomerOrderHeaderID
			WHERE	sro.OrderID = @OrderID

			DELETE	srcoh
			FROM	QSPCanadaOrderManagement..ShipmentRequestOrder sro
			JOIN	QSPCanadaOrderManagement..ShipmentRequestCustomerOrderHeader srcoh
						ON	srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
			WHERE	sro.OrderID = @OrderID

			DELETE	sro
			FROM	QSPCanadaOrderManagement..ShipmentRequestOrder sro
			WHERE	sro.OrderID = @OrderID

		END

		IF @IsFieldSupplyOrder = 1
		BEGIN

			USE [QSPCanadaCommon]

			UPDATE	camp
			SET		FSOrderRecCreated = 0
			FROM	QSPCanadaCommon..Campaign camp
			JOIN	QSPCanadaOrderManagement..Batch batch
						ON	batch.CampaignID = camp.ID
			WHERE	batch.OrderID = @OrderID

		END

	fetch NEXT from cOrderToCancel into @OrderID, @IsFieldSupplyOrder;
END

CLOSE cOrderToCancel
DEALLOCATE cOrderToCancel
DROP TABLE #Temp



