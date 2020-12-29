USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Sale_Info_Server_View_copy]    Script Date: 02/14/2014 13:02:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Sale_Info_Server_View_copy    Script Date: 2003-02-22 20:34:17 ******/

create view [dbo].[Sale_Info_Server_View_copy] /* view column name,... */
  as select distinct 
	Sale.Sales_ID as Sale_Id,
	Sale.Client_Sequence_Code as Client_Seq,
	Sale.Client_ID as Client_Id,
	Sale.Sales_Date as Sale_Date,
    	Sale.Scheduled_Delivery_Date as Scheduled_Delivery,
	Consultant.Name as Consultant_Name,
	Sales_Status.Description as Sale_Status_Desc,
    	Sales_Status.Sales_Status_ID as Sale_Status_Id,
    	Production_Status.Description as Production_Status,
	Payment_Method.Description as Method,
    	Payment_Term.Description as Term,
	Sale.PO_Number as PO,
	PO_Status.Description as PO_Status,
	Sale."Comment",
    	Carrier.Description as Carrier,
	Carrier_Shipping_Option.Description as Shipping_Option,
	Billing_Company.Billing_Company_Code 
from
    	Sales_Status 
	join(Consultant 
	join(Production_Status 
	join(PO_Status 
	right outer join(Payment_Term 
	join(Payment_Method 
	join(Carrier 
	right outer join((Sale 
	left outer join Carrier_Shipping_Option on Sale.Shipping_Option_ID = Carrier_Shipping_Option.Shipping_Option_ID) 
	left outer join Billing_Company on Sale.Billing_Company_ID = Billing_Company.Billing_Company_ID) on Carrier.Carrier_ID = Sale.Carrier_ID) on
    Payment_Method.Payment_Method_ID = Sale.Payment_Method_ID) on Payment_Term.Payment_Term_ID = 
    Sale.Payment_Term_ID) on PO_Status.PO_Status_ID = Sale.PO_Status_ID) on Production_Status.Production_Status_ID = 
    Sale.Production_Status_ID) on Consultant.Consultant_ID = Sale.Consultant_ID) on Sales_Status.Sales_Status_ID = 
    Sale.Sales_Status_ID
GO
