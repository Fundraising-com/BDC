USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetSubInfo2]    Script Date: 06/07/2017 09:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_GetSubInfo2]
AS
SELECT	s.LastName AS StudentLastName, 
		s.FirstName AS StudentFirstName, 
		coh.studentinstance, b.OrderID, 
		crh.CustomerInstance, 
		crh.LastName AS RecipientLastName, 
		crh.FirstName AS RecipientFirstName, 
		crh.LastName AS CustomerLastName,
		crh.FirstName AS CustomerFirstName,
		crh.Address1, 
		crh.Address2, 
		crh.City AS CustomerCity, 
		crh.State AS CustomerState, 
		crh.Zip AS CustomerZip, 
		codrh.CustomerOrderHeaderInstance, 
		codrh.TransID, 
		cod.ProductCode as TitleCode, codrh.MagazineTitle AS Title, 
		codrh.NumberOfIssues AS IssuesSent, 
		codrh.CatalogPrice,
		--convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as Price,
		convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) AS Price, 
		codrh.ItemPriceTotal, 
		codrh.CurrencyID, 
		codrh.baseprice, 
		cod.OverrideProduct, 
		cd.Description AS Status, 
		rb.ID AS RemitBatchID, 
		rb.[Date] AS RemitBatchDate, 
		rb.RunID,
		rb.status as remitbatchstatus,
		cod.CreationDate AS SubscriptionDate, 
		b.CampaignID,
		cdBatch.Description as OrderStatus, 
		codrh.DateChanged as  DateSub,
		cdQualifier.Description as QualifierName, 
		cd.Instance AS SubStatusInstance, 
		cdProductType.Description AS ProductType,
		coh.AccountID, 
		crh.Instance as customerremithistoryinstance, 
		coh.OrderBatchDate, 
		coh.OrderBatchID,
		p.status as productstatus,
		cod.producttype as producttypeinstance


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
		and p.product_code=pd.product_code
		and p.product_year = pd.pricing_year
		and p.product_season = pd.pricing_season
		and pd.magprice_instance = cod.pricingdetailsid
		and codrh.CustomerRemitHistoryInstance =
                          (SELECT    top 1 CustomerRemitHistoryInstance
                            FROM          CustomerOrderDetailRemitHistory x
                            WHERE      x.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance AND x.TransID = codrh.TransID order by CustomerRemitHistoryInstance desc)
		and cdBatch.instance = b.statusinstance

		and cdQualifier.Instance = b.OrderQualifierID 
		and cdProductType.Instance = cod.ProductType
		and coh.CustomerBillToInstance = c.Instance

		and ca.ID = b.CampaignID
GO
