USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[writeSalesShippingFeesDiscount]    Script Date: 02/14/2014 13:09:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Stored Procedure dbo.writeSalesShippingFeesDiscount    Script Date: 2003-02-22 20:34:56 ******/

CREATE procedure [dbo].[writeSalesShippingFeesDiscount](@nSalesId integer)
as
begin
  /*************************************************************************************************
  Créé le 16/03/2001
  1)  On va chercher le discount percent du payment_term
  2)  On calcule le shipping_fees_discount (shipping_fees * discount_percent)

  *************************************************************************************************/
  declare @nDiscountPercent numeric(15,4);
  declare @nShippingAmount numeric(15,4);
  declare @nShippingDiscount numeric(15,4);
  --  1) On va chercher le discount_percent
  set @nDiscountPercent = (
	select discount_percent/100 as discount 
	from payment_term as p,sale as s 
	where s.payment_term_id = p.payment_term_id 
		and s.sales_id = @nSalesId);
  --  On va chercher le montant de shipping_fees
  set @nShippingAmount = (
  	select shipping_fees
	from sale 
	where sales_id = @nSalesId);
  -- On calcule le shipping_fees_discount
  set @nShippingDiscount=@nShippingAmount*@nDiscountPercent;
  -- On update la vente
  update sale set shipping_fees_discount = @nShippingDiscount where
    sales_id = @nSalesId

/*print '@nsalesid=';
print @nsalesid;
print '@ndiscountpercent=';
print @ndiscountpercent;
print '@nShippingAmount=';
print @nShippingAmount;
print '@nShippingDiscount=';
print @nShippingDiscount;*/
end
GO
