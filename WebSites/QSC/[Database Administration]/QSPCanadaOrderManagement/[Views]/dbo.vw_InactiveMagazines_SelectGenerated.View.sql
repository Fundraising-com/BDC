USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_InactiveMagazines_SelectGenerated]    Script Date: 06/07/2017 09:18:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jeff Miles
-- Create date: 09/17/2007
-- Description:	Gets all inactive magazines generated (used by qsp.ca)
-- =============================================
CREATE VIEW [dbo].[vw_InactiveMagazines_SelectGenerated]

AS
SELECT		cod.CustomerOrderHeaderInstance,
			cod.TransID,
			LEFT(COALESCE(cod.Recipient, ''), CHARINDEX(' ', COALESCE(cod.Recipient, ''), CHARINDEX(' ', COALESCE(cod.Recipient, ''), 1))) AS RecipientFirstName,
			LTRIM(RIGHT(COALESCE(cod.Recipient, ''), LEN(REPLACE(COALESCE(cod.Recipient, ''), ' ', '_')) - COALESCE(CHARINDEX(' ', COALESCE(cod.Recipient, ''), 1), 0))) AS RecipientLastName,
			c.Address1,
			COALESCE(c.Address2, '') AS Address2,
			c.City,
			c.State AS Province,
			c.Zip AS PostalCode,
			p.Product_Sort_Name AS MagazineTitle,
			CONVERT(numeric(10, 2), cod.Price) AS Price,
			pd.Nbr_Of_Issues AS NbrOfIssues,
			COALESCE(p.Lang, 'EN') AS Language,
			cod.CreationDate,
			imlb.ProductCode,
			p.RemitCode,
			imlb.Reason
FROM		LetterBatchCustomerOrderDetail lbcod
JOIN		LetterBatch lb
				ON	lb.ID = lbcod.LetterBatchID
				AND	lb.DeletedTF = 0
				AND	lb.LetterTemplateID = 4 --Inactive Magazine
JOIN		InactiveMagazineLetterBatch imlb
				ON	imlb.LetterBatchID = lb.ID
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = lbcod.CustomerOrderHeaderInstance
				AND	cod.TransID = lbcod.TransID
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Customer c
				ON	c.Instance =
					CASE cod.CustomerShipToInstance
						WHEN 0 THEN	coh.CustomerBillToInstance
						ELSE		cod.CustomerShipToInstance
					END
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
GO
