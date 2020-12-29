USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_indexdefrag]    Script Date: 02/14/2014 13:03:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_indexdefrag] 
AS
BEGIN
dbcc indexdefrag('eFundraisingProd', 'client', 'IX_client_lead_id');
dbcc indexdefrag('eFundraisingProd', 'client', 'PK_client');

dbcc indexdefrag('eFundraisingProd', 'client_activity', 'IX_client_activity_client_id');
dbcc indexdefrag('eFundraisingProd', 'client_activity', 'IX_client_activity_2');
dbcc indexdefrag('eFundraisingProd', 'client_activity', 'IX_client_activity_3');
dbcc indexdefrag('eFundraisingProd', 'client_activity', 'PK_client_activity');

dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_consultant');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_consultant_assignment_date');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_consultant_id_lead_id');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_consultant_state_priority');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_day_phone_evening_phone');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_division_id');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_email');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_evening_phone');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_ext_consultant_id');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_first_name_last_name_lead_id');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_kit_type_id');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_lead_consultant_title_country');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_lead_entry_date');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_matching_code');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_organization');
dbcc indexdefrag('eFundraisingProd', 'lead', 'IX_lead_promotion_id');
dbcc indexdefrag('eFundraisingProd', 'lead', 'PK_lead');

dbcc indexdefrag('eFundraisingProd', 'lead_activity', 'IX_lead_activity_dates');
dbcc indexdefrag('eFundraisingProd', 'lead_activity', 'IX_lead_activity_2');
dbcc indexdefrag('eFundraisingProd', 'lead_activity', 'IX_lead_activity_3');
dbcc indexdefrag('eFundraisingProd', 'lead_activity', 'IX_lead_activity_lead_id');
dbcc indexdefrag('eFundraisingProd', 'lead_activity', 'IX_lead_activity_lead_id2');
dbcc indexdefrag('eFundraisingProd', 'lead_activity', 'PK_lead_activity');

dbcc indexdefrag('eFundraisingProd', 'sale', 'IX_sale_2');
dbcc indexdefrag('eFundraisingProd', 'sale', 'IX_sale_3');
dbcc indexdefrag('eFundraisingProd', 'sale', 'IX_sale_consultant_sales_date');
dbcc indexdefrag('eFundraisingProd', 'sale', 'IX_sale_lead_id');
dbcc indexdefrag('eFundraisingProd', 'sale', 'IX_sale_reship_date');
dbcc indexdefrag('eFundraisingProd', 'sale', 'IX_sale_sales_status_id');
dbcc indexdefrag('eFundraisingProd', 'sale', 'IX_sales_consultant_id');
dbcc indexdefrag('eFundraisingProd', 'sale', 'NI_sale_actual_ship_date');
dbcc indexdefrag('eFundraisingProd', 'sale', 'NI_sale_box_return_date');
dbcc indexdefrag('eFundraisingProd', 'sale', 'NI_sale_client');
dbcc indexdefrag('eFundraisingProd', 'sale', 'PK_sale');

dbcc indexdefrag('eFundraisingProd', 'sales_item', 'IX_sales_item');
dbcc indexdefrag('eFundraisingProd', 'sales_item', 'IX_sales_item_1');
dbcc indexdefrag('eFundraisingProd', 'sales_item', 'IX_sales_item_2');
dbcc indexdefrag('eFundraisingProd', 'sales_item', 'IX_sales_item_3');
dbcc indexdefrag('eFundraisingProd', 'sales_item', 'PK_sales_item');

dbcc indexdefrag('eFundraisingProd', 'Adjustment', 'IX_Adjustment');
dbcc indexdefrag('eFundraisingProd', 'Adjustment', 'PK_Adjustment');

dbcc indexdefrag('eFundraisingProd', 'AR_Activity', 'AR_Activity_Sale_ID');
dbcc indexdefrag('eFundraisingProd', 'AR_Activity', 'PK_AR_Activity');

dbcc indexdefrag('eFundraisingProd', 'client_address', 'IX_client_address_2');
dbcc indexdefrag('eFundraisingProd', 'client_address', 'IX_client_address_3');
dbcc indexdefrag('eFundraisingProd', 'client_address', 'PK_Client_Address');

dbcc indexdefrag('eFundraisingProd', 'comments', 'IX_Comments');
dbcc indexdefrag('eFundraisingProd', 'comments', 'IX_Comments_Sales_ID_Department_ID');
dbcc indexdefrag('eFundraisingProd', 'comments', 'PK_Comments');

dbcc indexdefrag('eFundraisingProd', 'consultant', 'IX_consultant_department_id');
dbcc indexdefrag('eFundraisingProd', 'consultant', 'IX_consultant_id_name');
dbcc indexdefrag('eFundraisingProd', 'consultant' ,'IX_consultant_nt_login');
dbcc indexdefrag('eFundraisingProd', 'consultant', 'PK_consultant');
dbcc indexdefrag('eFundraisingProd', 'consultant', 'UX_consultant_name');

dbcc indexdefrag('eFundraisingProd', 'payment','IX_payment_sales_id_payment_amount')
dbcc indexdefrag('eFundraisingProd', 'payment','PK_payment')

dbcc indexdefrag('eFundraisingProd', 'promotion', 'IX_promotion_3');
dbcc indexdefrag('eFundraisingProd', 'promotion', 'IX_promotion_description');
dbcc indexdefrag('eFundraisingProd', 'promotion', 'IX_promotion_destinations');
dbcc indexdefrag('eFundraisingProd', 'promotion', 'IX_promotion_partner');
dbcc indexdefrag('eFundraisingProd', 'promotion', 'IX_promotion_promotion_type');
dbcc indexdefrag('eFundraisingProd', 'promotion', 'IX_promotion_2');
dbcc indexdefrag('eFundraisingProd', 'promotion', 'PK_promotion');

dbcc indexdefrag('eFundraisingProd', 'scratch_book', 'IX_scratch_book_product_class');
dbcc indexdefrag('eFundraisingProd', 'scratch_book', 'PK_scratch_book');

dbcc freeproccache;
END
GO
