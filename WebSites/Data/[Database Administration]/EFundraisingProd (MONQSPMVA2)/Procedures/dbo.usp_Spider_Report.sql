USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[usp_Spider_Report]    Script Date: 12/20/2017 10:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[usp_Spider_Report]

( @saleId				INT = NULL,
  @leadId				INT = NULL,
  @promotionId          INT = NULL,
  @scratchbookId        INT = NULL,
  @partnerID			INT = NULL,
  @stateCode			VARCHAR(10) = NULL,
  @consultantName		VARCHAR(50) = NULL,  
  @zipCode				VARCHAR(10) = NULL,
  @country				VARCHAR(10) = NULL,
  @dayPhone				VARCHAR(20) = NULL,  
  @eveningPhone			VARCHAR(20) = NULL,
  @email				VARCHAR(50) = NULL,
  @organizationType		VARCHAR(50) = NULL,
  @totalAmount			DECIMAL(15, 4) = NULL,
  @productClassDesc		VARCHAR(50) = NULL,
  @actualShipStartDate	DATETIME  = NULL,    
  @actualShipEndDate	DATETIME  = NULL,    
  @fundraiserStartDate	DATETIME  = NULL,
  @fundraiserEndDate	DATETIME  = NULL,
  @saleConfirmStartDate	DATETIME  = NULL,
  @saleConfirmEndDate	DATETIME  = NULL,
  @leadEntryStartDate	DATETIME  = NULL,
  @leadEntryEndDate		DATETIME  = NULL
)
as

SELECT
	Consultant.name,
	Lead.first_name,
	Lead.last_name,
	Lead.day_phone,
	Lead.evening_phone,
	Lead.fax,
	Lead.organization,
    Lead.street_address,
    Lead.zip_code,
    Lead.state_code,
    Lead.country_code,
     Lead.city,
    Lead.email,
    Lead.member_count,
    Lead.participant_count,
    Lead.fund_raiser_start_date,
    Organization_Type.organization_type_desc,
    dbo.sale.total_amount,
    dbo.sale.sales_id, 
    Lead.lead_id,
    sale.confirmed_date,
    dbo.sale.actual_ship_date,
    Lead.lead_entry_date,
    product_class.description as [Product_Description], 
    Promotion.promotion_type_code,
    Promotion.description as [Promotion_Description],
    Sales_Item.quantity_sold,
    Scratch_Book.description as  [Scrapbook_Description],
    Partner.partner_id, 
    Partner.partner_name,
    Partner.email_ext,
    Partner.phone_number,
    Lead_Channel.Channel_Code,
    Group_Type.description as [Group_Description],
    SUM(Adjustment.Adjustment_Amount) as [Total_Adjustments]
FROM
 
      Lead (NOLOCK)
      INNER JOIN Consultant (NOLOCK)  ON Lead.consultant_id = Consultant.consultant_id
      INNER JOIN Promotion (NOLOCK) ON Lead.promotion_id = Promotion.promotion_id
      INNER JOIN Partner (NOLOCK) ON Promotion.partner_id = Partner.partner_id
      LEFT JOIN Client (NOLOCK) ON Lead.lead_id = Client.lead_id
      LEFT JOIN dbo.sale (NOLOCK) on sale.lead_id = lead.lead_id
      LEFT JOIN Sales_Item (NOLOCK) ON dbo.sale.sales_id = Sales_Item.sales_id
      LEFT JOIN Scratch_Book (NOLOCK) ON Sales_Item.scratch_book_id = Scratch_Book.scratch_book_id-- ON Client.client_id = dbo.sale.client_id AND Client.client_sequence_code = dbo.sale.client_sequence_code
      LEFT JOIN product_class (NOLOCK) ON Scratch_Book.product_class_id = product_class.product_class_id
      LEFT JOIN Organization_Type (NOLOCK) ON Lead.organization_type_id = Organization_Type.organization_type_id
      LEFT JOIN Package (NOLOCK) ON Scratch_Book.package_id = Package.Package_Id INNER JOIN Lead_Channel ON Lead.channel_code = Lead_Channel.Channel_Code
      LEFT JOIN Lead_Activity (NOLOCK) ON Lead.lead_id = Lead_Activity.lead_id INNER JOIN Group_Type ON Lead.group_type_id = Group_Type.group_type_id
      LEFT JOIN Adjustment (NOLOCK) ON Adjustment.Sales_ID = sale.sales_id
WHERE
      (@leadId IS NULL OR Lead.lead_id = @leadId)    
      AND (@saleId IS NULL OR dbo.sale.sales_id = @saleId)
      AND (@promotionId IS NULL OR dbo.promotion.promotion_id = @promotionId)
      AND (@scratchbookId IS NULL OR dbo.scratch_book.scratch_book_id = @scratchbookId)
      AND (@partnerID IS NULL OR dbo.partner.partner_id = @partnerID)
      AND (@stateCode IS NULL OR dbo.lead.state_code = @stateCode)
      AND (@consultantName	IS NULL OR dbo.consultant.name = @consultantName)
      AND (@zipCode	IS NULL OR dbo.lead.zip_code = @zipCode)
      AND (@country	IS NULL OR dbo.lead.country_code = @country)
      AND (@dayPhone IS NULL OR dbo.lead.day_phone = @dayPhone)
      AND (@eveningPhone IS NULL OR dbo.lead.evening_phone = @eveningPhone)
      AND (@email IS NULL OR dbo.lead.email = @email)
      AND (@organizationType   IS NULL OR dbo.organization_type.organization_type_desc = @organizationType)
      AND (@totalAmount	IS NULL OR dbo.sale.total_amount = @totalAmount)
      AND (@productClassDesc IS NULL OR dbo.product_class.description = @productClassDesc)
      AND (@actualShipStartDate IS NULL OR dbo.sale.actual_ship_date >= @actualShipStartDate) 
      AND (@actualShipEndDate IS NULL OR dbo.sale.actual_ship_date <= @actualShipEndDate) 
	  AND (@fundraiserStartDate	IS NULL OR dbo.lead.fund_raiser_start_date >= @fundraiserStartDate)
	  AND (@fundraiserEndDate	IS NULL OR dbo.lead.fund_raiser_start_date <= @fundraiserEndDate)
	  AND (@saleConfirmStartDate	IS NULL OR dbo.sale.confirmed_date >= @saleConfirmStartDate)
	  AND (@saleConfirmEndDate	IS NULL OR dbo.sale.confirmed_date <= @saleConfirmEndDate)
	  AND (@leadEntryStartDate 	IS NULL OR dbo.lead.lead_entry_date >= @leadEntryStartDate) 
	  AND (@leadEntryEndDate 	IS NULL OR dbo.lead.lead_entry_date <= @leadEntryEndDate) 

  	  
GROUP BY Consultant.name, Lead.first_name, Lead.last_name, Lead.day_phone, Lead.evening_phone, Lead.fax, Lead.organization, 
      Lead.street_address, Lead.zip_code, Lead.state_code, Lead.country_code,Lead.city, Lead.email, Lead.member_count, Lead.participant_count, 
      Lead.fund_raiser_start_date, Organization_Type.organization_type_desc, dbo.sale.total_amount, dbo.sale.sales_id, 
      Lead.lead_id, dbo.sale.confirmed_date, dbo.sale.actual_ship_date, Lead.lead_entry_date, product_class.description, 
      Promotion.promotion_type_code, Promotion.description, Sales_Item.quantity_sold, Scratch_Book.description, Partner.partner_id, 
      Partner.partner_name, Partner.email_ext, Partner.phone_number, Lead_Channel.Channel_Code, Group_Type.description, Consultant.is_agent, Consultant.is_fm
      
      
 
