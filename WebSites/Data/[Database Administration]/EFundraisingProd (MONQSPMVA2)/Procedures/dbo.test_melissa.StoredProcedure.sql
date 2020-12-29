USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[test_melissa]    Script Date: 02/14/2014 13:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Stored Procedure dbo.test_melissa    Script Date: 2003-02-22 20:34:55 ******/
create procedure [dbo].[test_melissa](@nSalesId integer)
AS
  /*******************************************************************************************************
  Créé le 16/03/2001
  1)  On va chercher le discount percent du payment_term
  2)  Pour chaque sales_item associé à la vente on:
  On met le montant du discount dans discount_amount ( Quantity_Sold * Unit_Price_Sold * discount_percent)
  Fait le total de Quantity_Sold * Unit_Price_Sold * (1 - discount_percent)

  *******************************************************************************************************/
  /* On déclare l'erreur "Error Not Found"								*/
  declare @nDiscountPercent numeric(15,4);
  declare @nDiscountAmount numeric(15,4);
  declare @nDiscountedSalesAmount numeric(15,4);

  /* Variables qui contiendront les valeurs du curseur							*/
  declare @nSalesItemNo integer;
  declare @nTotSalesAmount numeric(15,4);

  /* Curseur qui sera utilisé pour aller chercher chaque sales_item associé au sales_id.		*/
  declare rstSalesItem cursor   
	for select sales_item_no,quantity_sold*unit_price_sold as total_sales_amount from sales_item 
	where sales_id = @nSalesId;
	/*	select sales_item_no,quantity_sold*unit_price_sold as total_sales_amount 
		into nTotSalesAmount from sales_item where sales_id = nSalesId;				*/

  /*  1) On va chercher le discount_percent								*/
  select @nDiscountPercent = discount_percent/100 from payment_term as p,sale as s where
    s.payment_term_id = p.payment_term_id and
    s.sales_id = @nSalesId;
  /*  2) On met à jour les totaux des sales_item  							*/
  OPEN rstSalesItem;
  FETCH NEXT FROM rstSalesItem INTO @nSalesItemNo,@nTotSalesAmount;

  WHILE (@@FETCH_STATUS = 0)
  BEGIN
   	/* On update le montant du sales_item								*/
    	set @nDiscountAmount=@nTotSalesAmount*@nDiscountPercent;
    	set @nDiscountedSalesAmount=@nTotSalesAmount*(1-@nDiscountPercent);
    	update sales_item set
      		Discount_Amount = @nDiscountAmount,Sales_Amount = @nDiscountedSalesAmount where
      		sales_id = @nSalesId and sales_item_no = @nSalesItemNo
	
  	FETCH NEXT FROM rstSalesItem INTO @nSalesItemNo,@nTotSalesAmount;
  END
  CLOSE rstSalesItem;
GO
