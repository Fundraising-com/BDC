USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fetchTaxAmount]    Script Date: 02/14/2014 13:09:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  User Defined Function dbo.fetchTaxAmount    Script Date: 2003-02-22 20:35:35 ******/

create function [dbo].[fetchTaxAmount](@nSales_id integer) returns numeric(15,4)
begin
--declare @nsaleInt int;
	declare @nTTA decimal(15,4);
	declare @saleID int; --Placeholder pour curseur...
	  /*
	  Dernière mise-à-jour: 14/02/2003
	  On fait la somme des tax amount dans la table Applicable_Tax. Si aucune taxe
	  n'est trouvée pour la vente, on returne 0.
	  */
	declare curs1 cursor for
	select	sale.sales_id,
		coalesce(Sum(Applicable_Tax.Tax_Amount),0) as Total_Tax
	from	Sale left
		outer join Applicable_Tax on Sale.Sales_ID = Applicable_Tax.Sales_ID
	group by Sale.Sales_ID
	having sale.sales_id = @nSales_id

	open curs1
	fetch next from curs1 into @saleid,@nTTA
	--SELECT @nTTA
	close curs1
	deallocate curs1

	return(@nTTA)
end
GO
