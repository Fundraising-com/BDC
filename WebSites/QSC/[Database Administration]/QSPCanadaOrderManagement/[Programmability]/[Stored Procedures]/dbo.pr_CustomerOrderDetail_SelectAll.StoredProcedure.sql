USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetail_SelectAll]    Script Date: 06/07/2017 09:19:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetail_SelectAll]

	@iCustomerOrderHeaderInstance	int,
	@iAdjustPriceToStaffOrder	int = 0

AS

SELECT	cod.CustomerOrderHeaderInstance,
		cod.TransID,
		cod.CustomerShipToInstance,
		cod.ProductCode,
		cod.ProductName,
		cod.Quantity,
		--MS March 25, 2007
		--convert(numeric(10,2), CASE WHEN @iAdjustPriceToStaffOrder = 0 OR ca.IsStaffOrder = 0 THEN cod.Price WHEN @iAdjustPriceToStaffOrder = 1 AND ca.IsStaffOrder = 1 THEN cod.Price * ca.StaffOrderDiscount / 100.00 END) as Price,
		convert(numeric(10,2), CASE WHEN @iAdjustPriceToStaffOrder = 0 OR ca.IsStaffOrder = 0 THEN cod.Price WHEN @iAdjustPriceToStaffOrder = 1 AND ca.IsStaffOrder = 1   THEN cod.Price * ((100 - IsNull(ca.StaffOrderDiscount,0))/100)    END) as Price,
		cod.PriceA,
		cod.Tax,
		cod.TaxA,
		cod.StatusInstance,
		cod.DelFlag,
		cod.Renewal,
		cod.Recipient,
		cod.OverrideProduct,
		cod.CreationDate,
		cod.CrossedBridgeDate,
		cod.ChangeUserID,
		cod.ChangeDate,
		cod.InvoiceNumber,
		cod.AlphaProductCode,
		cod.CouponPage,
		cod.FDIndicator,
		cod.MktgIndicator,
		cod.ToteInstance,
		cod.GiftCD,
		cod.IsGift,
		cod.IsGiftCardSent,
		cod.SendGiftCardBeforeDate,
		cod.ProgramSectionID,
		cod.CatalogPrice,
		cod.QuantityReserved,
		cod.PriceOverrideID,
		cod.ProductType,
		cod.PricingDetailsID,
		cod.Tax2,
		cod.Tax2A,
		cod.Net,
		cod.Gross,
		cod.SupporterName,
		cod.SendGiftCard,
		cod.QuantityShipped,
		cod.ShipmentID,
		cod.ReplacedProductCode,
		cod.ReplacedProductQty,
		cod.DistributionCenterID,
		cod.Comment,
		cod.CustomerComment,
		ca.IsStaffOrder,
		pm.program_type as CatalogName

FROM		CustomerOrderDetail cod,
		CustomerOrderHeader coh,
		QSPCanadaCommon..Campaign ca,
		QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_master pm

WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		coh.Instance = @iCustomerOrderHeaderInstance
AND		ca.ID = coh.CampaignID
and     programsectionid = ps.id
and     ps.Program_ID=pm.Program_ID
GO
