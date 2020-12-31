USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_TechnicalProblemLetterReport_CreditCard]    Script Date: 06/07/2017 09:20:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_TechnicalProblemLetterReport_CreditCard] AS


SELECT		distinct 
		case when ca.Lang='FR' then 'FR' else 'EN' end as Lang,
		crh.LastName, 
		crh.FirstName, 
		crh.Address1, 
		coalesce(crh.Address2, '') AS Address2, 
		crh.City, 
		crh.State, 
		crh.Zip
		

FROM         	Batch b,
		CustomerOrderHeader coh,
		Student s,
		RemitBatch rb,
               	CustomerRemitHistory crh,
                	CustomerOrderDetailRemitHistory codrh ,
                	CodeDetail cd,
	        	CodeDetail cdBatch,
		CodeDetail cdQualifier,
		CodeDetail cdProductType,
		CustomerOrderDetail cod,
		Customer c,
		QSPCanadaProduct..Product p,
		QSPCanadaProduct..Pricing_details pd,
		QSPCanadaCommon..Campaign ca

WHERE     	coh.OrderBatchID = b.ID
		and coh.OrderBatchDate = b.[Date]
		and coh.StudentInstance = s.Instance
		and coh.Instance = cod.CustomerOrderHeaderInstance 
		and cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
		and codrh.TransID = cod.TransID
		and crh.RemitBatchID = codrh.RemitBatchID
		and crh.Instance = codrh.CustomerRemitHistoryInstance
		and rb.ID = crh.RemitBatchID
		and codrh.Status = cd.Instance
		and cod.productcode = p.product_code
		and p.product_code=pd.product_code
		and p.product_year = pd.pricing_year
		and p.product_season = pd.pricing_season
		and pd.magprice_instance = cod.pricingdetailsid
		and codrh.CustomerRemitHistoryInstance =
                          (SELECT     MAX(CustomerRemitHistoryInstance)
                            FROM          CustomerOrderDetailRemitHistory x with (nolock) 
                            WHERE      x.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance AND x.TransID = codrh.TransID)
		and cdBatch.instance = b.statusinstance

		and cdQualifier.Instance = b.OrderQualifierID 
		and cdProductType.Instance = cod.ProductType
		and coh.CustomerBillToInstance = c.Instance

		and ca.ID = b.CampaignID
		and orderqualifierid=39014
GO
