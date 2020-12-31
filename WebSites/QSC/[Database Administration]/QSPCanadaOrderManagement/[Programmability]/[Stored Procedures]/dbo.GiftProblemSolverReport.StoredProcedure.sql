USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GiftProblemSolverReport]    Script Date: 06/07/2017 09:19:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GiftProblemSolverReport]
	@ReportRequestID	int, 
	@OrderID			int
AS

SELECT		acc.ID AS AcctID,
			b.CampaignID,
			camp.Lang,
			b.OrderID,
			ContactFirstName + ' ' + ContactLastname AS ContactName,
			acc.Name AS AccountName,
			addShip.Street1 AS ShippingAddress,
			addShip.Street2 AS ShippingAddress2,
			addShip.City AS ShippingCity,
			addShip.StateProvince AS ShippingState,
			addShip.Postal_Code AS ShippingZip,
			PhoneNumber,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'N° du gérant de territoire:'
				ELSE 'FM ID:'
			END AS FMIDLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'N° de campagne:'
				ELSE 'Campaign ID:'
			END AS CAIDLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'N° de commande:'
				ELSE 'Order ID:'
			End AS OrderIDLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'N° de groupe:'
				ELSE 'Group ID'
			END AS ShipToAccLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'de'
				ELSE 'of'			END AS PageOf,
			CASE ISNULL(camp.Lang,'EN')
				WHEN 'FR' THEN '33 Prince Street Suite 200'
				ELSE '695 Riddell Road' 
			END AS QSPAddress1Label,
			CASE ISNULL(camp.Lang,'EN')
				WHEN 'FR' THEN 'Montreal, QC   H3C 2M7'
				ELSE 'Orangeville, ON   L9W 4Z5'
			END AS QSPAddress2Label,
			CASE ISNULL(camp.Lang,'EN')
				WHEN 'FR' THEN '1-800-667-2536'
				ELSE '1-800-667-2536'
			END AS QSPPhoneLabel,
			CASE ISNULL(camp.Lang,'EN')
				WHEN 'FR' THEN 'À :  QSP'
				ELSE 'Fax to:  QSP'
			END AS FaxtoQSPLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN  'De : ' 
				ELSE 'Fax From:'
			END	AS FaxFromlabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Nombre total de pages: '
				ELSE 'Total number of pages:'
			END AS TotalPagesLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Qté'
				ELSE 'Qty'
			End AS QtyLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'N° de classe'
				ELSE 'Room  No.  '
			END AS RoomNoLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Enseignant'
				ELSE 'Homeroom Teacher'
			END AS RoomTeacherLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Nom du participant'
				ELSE 'Participant Name'
			END AS ParticipantLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Code de l''article'
				ELSE 'Item Code'
			END AS ItemCodeLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Description de l''article'
				ELSE 'Item Description'
			END AS ItemDescriptionLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Manquant'
				ELSE 'Missing'
			END AS MissingLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Endommagé'
				ELSE 'Damaged'
			END AS DamagedLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Additionnel'
				ELSE 'Additional'
			END AS AdditionalLabel,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'A L''ATTENTION DE :'
				ELSE 'ATTN:'
			END AS AttnLabel	
FROM		Batch b  
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = b.AccountID
LEFT JOIN	QSPCanadaCommon..AddressList al
				ON	al.ID = acc.AddressListID
LEFT JOIN	QSPCanadaCommon..Address addShip
				ON	al.ID = addShip.AddressListID
				AND	addShip.Address_Type = 54001 --Ship To
LEFT JOIN	QSPCanadaCommon..Address addBill
				ON	al.ID = addBill.AddressListID
				AND	addBill.Address_Type = 54001 --Use ship to for both.
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
LEFT JOIN	QSPCanadaCommon..Phone phone
				ON	acc.PhoneListID = phone.PhoneListID
				AND	phone.Type = 30501
WHERE		b.OrderID = @OrderID
AND			EXISTS (SELECT	TOP 1 *
					FROM	CustomerOrderDetail cod
					JOIN	CustomerOrderHeader coh
								ON	coh.Instance = cod.CustomerOrderHeaderInstance
					JOIN	Batch b2
								ON	b2.ID = coh.OrderBatchID
								AND	b2.[Date] = coh.OrderBatchDate
								AND	(b2.OrderID = @OrderID
									 OR (b2.OrderID IN (SELECT DISTINCT OnlineOrderID  
														 FROM OnlineOrderMappingTable  
														 WHERE LandedOrderID = @OrderID)
														 AND IsShippedToAccount = 1))
					WHERE	cod.ProductType In ( 46002, 46007, 46018, 46019, 46020, 46022, 46024)
)
IF @ReportRequestID <> 0  -- if the value is not zero it means the report is called from a data driven subscription
BEGIN
   UPDATE ReportRequestBatch_ProblemSolverReport
   SET  RunDateStart = GETDATE()
   WHERE [ID]  = @ReportRequestID
END

SET NOCOUNT OFF
GO
