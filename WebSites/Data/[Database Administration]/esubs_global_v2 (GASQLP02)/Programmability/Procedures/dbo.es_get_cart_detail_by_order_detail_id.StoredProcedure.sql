USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_cart_detail_by_order_detail_id]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Cart_Detail
CREATE PROCEDURE [dbo].[es_get_cart_detail_by_order_detail_id]
@order_detail_id int 
AS
begin

select  Cart_Detail_Id, 
		Cart_Id, 
		X_Catalog_Item_Detail_Id, 
		X_Order_Detail_Id, 
		Renewal_TF, 
		Gift_TF, 
		Quantity, 
		Shipping_X_Postal_Address_Id, 
		Create_Date, 
		Modify_Date, 
		Modified_By, 
		Deleted_TF, 
		Price_Applied, 
		Gift_Email_Address, 
		Gift_Personalized_Message, 
		Gift_Recipient, 
		Gift_From 
from qspecommerce..Cart_Detail with (nolock)
where X_Order_Detail_Id=@order_detail_id
		
end
GO
