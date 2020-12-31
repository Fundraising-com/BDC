USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetPeopleNbrOfIssuesByTeacherID]    Script Date: 06/07/2017 09:21:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_GetPeopleNbrOfIssuesByTeacherID]
(
	@OrderID		int,
	@NumIssues		int,
	@TeacherID		int
)  
RETURNS int AS  
BEGIN 
	DECLARE	@ReturnValue	int

		SET @ReturnValue = 0
		
		SELECT		@ReturnValue = ISNULL(SUM( CASE COD.ProductType
											WHEN 46001 Then (1) 	   
											Else COD.Quantity
										END),0)
		FROM		QSPCanadaOrderManagement..CustomerOrderDetail	cod 
		INNER JOIN	QSPCanadaOrderManagement..CustomerOrderHeader	coh		ON COH.Instance = COD.CustomerOrderHeaderInstance
		INNER JOIN	QSPCanadaOrderManagement..Batch					b		ON COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
		INNER JOIN	QSPCanadaOrderManagement..Student				s		ON S.Instance = COH.StudentInstance
		INNER JOIN  QSPCanadaOrderManagement..Teacher				teach	ON teach.Instance = s.TeacherInstance
		INNER JOIN	QSPCanadaCommon..Campaign						c		ON C.ID = B.CampaignID
		JOIN		QSPCanadaProduct.dbo.Pricing_Details			pd		ON pd.MagPrice_Instance = cod.PricingDetailsID 
		JOIN		QSPCanadaProduct.dbo.Product					p		ON p.Product_Instance = pd.Product_Instance
		WHERE		(b.OrderID = @OrderID OR
					b.OrderID IN	(SELECT	DISTINCT OnlineOrderID
										FROM	OnlineOrderMappingTable
										WHERE	LandedOrderID = @OrderID)) AND
					pd.Nbr_of_Issues	= @NumIssues	AND
					teach.Instance		= @TeacherID	AND
					p.RemitCode = '3313' AND -- People Magazine
					pd.QspPremiumID > 0

	RETURN	@ReturnValue
END
GO
