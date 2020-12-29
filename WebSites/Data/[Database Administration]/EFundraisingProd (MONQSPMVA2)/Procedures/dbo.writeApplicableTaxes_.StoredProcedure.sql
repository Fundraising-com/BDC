USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[writeApplicableTaxes_]    Script Date: 02/14/2014 13:09:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Stored Procedure dbo.writeApplicableTaxes    Script Date: 2003-02-22 20:34:55 ******/



CREATE       procedure [dbo].[writeApplicableTaxes_] (@nSalesId integer)
as
BEGIN
  /* On declare l erreur Error Not Found*/
  declare @err_notfound varchar(20); -- for sqlstate value '02000';
  /* On déclare les variables nécessaires*/
  declare @nTax_rate numeric(15,4);
  declare @nTaxable_amount numeric(15,4);
  declare @nTax_amount numeric(15,4);
  declare @sTax_Code varchar(4);


 -- On commence par détruire les anciennes taxes de la vente

   print @nSalesId

if Exists(select sales_id from applicable_tax where sales_id=@nSalesId)
  delete from applicable_tax where sales_id = @nSalesId;


   
  --On va chercher le montant taxable de la vente
 -- La somme des sales_amount des sales_item + shipping_fees - shipping_fees_discount
 
set @nTaxable_amount=(
  select 	coalesce(Sum(Sales_Amount),0)+Shipping_Fees-Shipping_fees_discount as Taxable_Amount 
	/*into nTaxable_amount2*/
    	from Sale left outer join Sales_Item on Sale.Sales_ID = Sales_Item.Sales_ID
	    group by Sale.Sales_ID,Sale.Shipping_Fees,Sale.Shipping_Fees_Discount,Shipping_Fees-Shipping_fees_discount having
	    sale.sales_id = @nSalesId);
  /* 
  On ouvre le curseur
  Pour chaque Tax_code, on fait une entrée dans applicable_tax jusqu'à ce que "Error not found"
  soit atteint ( recordset est vide ).
  */
  IF @nTaxable_amount > 0
  Begin   

	IF Exists(select tax_code,tax_rate from applicable_tax_rate where sales_id = @nSalesId)
	BEGIN
		  /* Déclaration du curseur*/
		  declare Applicable_Taxes cursor for
		          select tax_code,tax_rate from applicable_tax_rate 
		          where sales_id = @nSalesId order by tax_order asc;
		 
		
		     OPEN Applicable_Taxes;
		   
		     FETCH NEXT FROM Applicable_Taxes into @sTax_Code,@nTax_rate;
		     WHILE @@FETCH_STATUS = 0     
		     BEGIN
		            
		        set @nTax_amount=@nTaxable_amount*@nTax_rate/100;
		        insert into Applicable_Tax(Sales_ID,Tax_Code,Tax_Amount) values(@nSalesId,@sTax_Code,@nTax_amount);
		        set @nTaxable_amount=@nTaxable_amount+@nTax_amount
		    
		        FETCH NEXT FROM Applicable_Taxes into @sTax_Code,@nTax_rate;
		     END
		
		     close Applicable_Taxes;
		    deallocate Applicable_Taxes;
	END
  END

END
GO
