USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[writeTotalSalesAmount]    Script Date: 02/14/2014 13:09:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Stored Procedure dbo.writeTotalSalesAmount    Script Date: 2003-02-22 20:34:56 ******/

CREATE procedure [dbo].[writeTotalSalesAmount]
	@nSalesId integer
as
  declare @nSalesAmount numeric(15,4);
  declare @nTaxAmount numeric(15,4);
  declare @nNetShipping numeric(15,4);
  declare @nTotalAmount numeric(15,4);
  /*  
  On fetch le Sales amount
  */
  set @nsalesamount = /*select*/ dbo.fetchSalesAmount(@nSalesId);-- into @nSalesAmount;
  /*
  On fetch les taxes
  */
  set @nTaxAmount = dbo.fetchTaxAmount(@nSalesId);-- into @nTaxAmount;
  /*
  Le Shipping
  */
  SET @nNetShipping =(
  select coalesce(fuelsurcharge, 0) + shipping_fees-coalesce(shipping_fees_discount,0) as net_shipping --into @nNetShipping
--select fuelsurcharge + shipping_fees-coalesce(shipping_fees_discount,0) as net_shipping --into @nNetShipping
    from sale where
    sales_id = @nSalesId);
  /* 
  On update la vente
  */
  set @nTotalAmount=@nSalesAmount+@nTaxAmount+@nNetShipping;
  update sale set Total_Amount = @nTotalAmount where
    sales_id = @nSalesId
GO
