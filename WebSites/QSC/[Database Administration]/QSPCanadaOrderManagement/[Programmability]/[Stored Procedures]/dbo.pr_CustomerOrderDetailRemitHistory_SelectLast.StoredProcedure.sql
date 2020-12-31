USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetailRemitHistory_SelectLast]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetailRemitHistory_SelectLast]
	@iCustomerOrderHeaderInstance int,
	@iTransID int
	
AS
SET NOCOUNT ON

declare @DateTime		DateTime,
	@FulName		varchar(125),
	@FulTel		varchar(50),
	@MagazineStatus	varchar(64),
	@DonorName		varchar(81),
	@GiftOrderType		varchar(1),
	@premiumindicator	int,
	@premiumcode		varchar(50),
	@premiumdesc		varchar(50),
	@Renewal		char(1),
	@NumberOfIssues	int


select  @DateTime = coalesce(gco.date, '')  from 	
	customerorderdetailRemitHistory codrh,
	giftcardoutput gco,
	customerorderdetail cod,
	giftcardremitbatch gcrb
where 	gco.id = gcrb.giftcardoutputid and
	gcrb.remitbatchid = codrh.remitbatchid and
	cod.isgift=1 and
	cod.isgiftcardsent = 1 and
	cod.customerorderheaderinstance = codrh.customerorderheaderinstance and
	cod.TransID = codrh.TransID and 
	codrh.customerorderheaderinstance = @iCustomerOrderHeaderInstance and
	codrh.TransID = @iTransID


--set @DateTime = getDate()

SELECT top 1
	@FulName = ffh.Ful_Name,
	@FulTel = ffh.Ful_Tel,
	@MagazineStatus = cdp.Description,
	@DonorName = cod.SupporterName,
	@GiftOrderType = cod.GiftCD,
	@premiumindicator=premiumindicator,
	@premiumcode = codrh.premiumcode,
	@premiumdesc = premiumdescription,
	@Renewal = codrh.Renewal,
	@NumberOfIssues = codrh.NumberOfIssues

FROM	CustomerOrderDetailRemitHistory codrh,
	QSPCanadaProduct..FULFILLMENT_HOUSE ffh,
	RemitBatch rb,
	QSPCanadaProduct..Product p,
	QSPCanadaProduct..Pricing_Details pd,
	CodeDetail cdp,
	CustomerOrderDetail cod,
	CustomerOrderHeader coh,
	QSPCanadaCommon..Campaign ca,
	Batch b

WHERE
	codrh.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND codrh.TransID = @iTransID
	AND b.Date = coh.OrderBatchDate
	AND b.ID = coh.OrderBatchID
	 AND codrh.remitbatchid = rb.ID 
	and rb.Fulfillmenthousenbr = ffh.Ful_Nbr
	and cdp.Instance = p.status 
	and cod.PricingDetailsID = pd.MagPrice_Instance
	and p.Product_Code = pd.Product_Code
	and p.Product_Season = pd.Pricing_Season
	and p.Product_Year = pd.Pricing_Year
	and cod.CustomerOrderHeaderInstance = coh.Instance
	and ca.ID = coh.CampaignID
	
	and cod.customerorderheaderinstance = codrh.customerorderheaderinstance 
	and cod.TransID = codrh.TransID 

ORDER BY codrh.customerremithistoryinstance desc	


SELECT	vw.StudentLastName, 
		vw.StudentFirstName, 
		vw.studentinstance,
		vw.OrderID, 
		vw.CustomerInstance, 
		vw.RecipientLastName, 
		vw.RecipientFirstName, 
		vw.CustomerLastName,
		vw.CustomerFirstName,
		vw.Address1, 
		vw.Address2, 
		vw.CustomerCity, 
		vw.CustomerState, 
		vw.CustomerZip, 
		vw.CustomerOrderHeaderInstance, 
		vw.TransID, 
		vw.Title,
		vw.TitleCode,
		vw.IssuesSent, 
		vw.CatalogPrice,
		vw.Price,
		vw.ItemPriceTotal, 
		vw.CurrencyID, 
		vw.baseprice, 
		vw.OverrideProduct, 
		vw.Status, 
		vw.RemitBatchID, 
		case when vw.RemitBatchStatus >42000 then vw.RemitBatchDate else null end AS RemitBatchDate,
		coalesce(vw.RunID, 0) AS RunID,
		coalesce(vw.RemitBatchCount, 0) AS RemitBatchCount,
		vw.SubscriptionDate, 
		vw.CampaignID,
		vw.OrderStatus, 
		vw.DateSub,
		vw.QualifierName, 
		vw.SubStatusInstance, 
		vw.ProductType,
		vw.AccountID, 
		vw.customerremithistoryinstance, 
		vw.OrderBatchDate, 
		vw.OrderBatchID,
		vw.productstatus,
		vw.producttypeinstance,
		coalesce(@FulName,'') as Ful_Name,
		coalesce(@FulTel,'') as Ful_Tel,
		vw.Status as StatusDescription,
		coalesce(@MagazineStatus, '') as MagazineStatus,
		coalesce(@DonorName, '') as DonorName,
		coalesce(@GiftOrderType, '') as GiftOrderType,
		vw.Price as PriceEntered,
		coalesce(@premiumindicator,0) as premiumindicator,
		coalesce(@premiumcode,'') as premiumcode,
		coalesce(@premiumdesc,'') as premiumdescription,
		@DateTime as DateCardSent,
		coalesce(@Renewal, 'N') as Renewal,
		coalesce(@NumberOfIssues, 1) as NumberOfIssues,
		coalesce(vw.SubscriptionDate, '1995-01-01') as OrderKeyedDate,
		vw.ToteID,
		vw.CustomerOrderID,
		vw.InvoiceNumber

FROM 
vw_GetSubAndProductsInfo vw

WHERE
	vw.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND vw.TransID = @iTransID
GO
