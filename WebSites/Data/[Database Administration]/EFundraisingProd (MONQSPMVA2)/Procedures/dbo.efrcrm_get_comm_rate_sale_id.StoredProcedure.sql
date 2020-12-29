USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_comm_rate_sale_id]    Script Date: 02/14/2014 13:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[efrcrm_get_comm_rate_sale_id] (@sale_id int)
as 

SELECT 
	Sale.Sales_ID
	, Sale.Consultant_ID
	, Sale.Sales_Date
	, Commission_Rate.Consultant_ID
	, Commission_Rate.Scratch_Book_ID
	, Sale.Actual_Ship_Date
	, Scratch_Book.Product_Class_ID
	, Scratch_Book.Scratch_Book_ID
	, Scratch_Book.Description
	, Sales_Item.Scratch_Book_ID
	, Sales_Item.Sales_Amount
	, Sales_Item.Quantity_Free
	, (case 
		-- default commission tally sales, scratchcard et dicount card always same rate pas tenir compte 
		-- des ranges et pas de diminution pour free cases
		when Commission_Rate.Commission_Rate_Free is null and Scratch_Book.Product_Class_ID in (1,10,11,23)
			then scratch_book_commission.commission_rate
		-- specifique commission tally sales, scratchcard et dicount card always same rate pas tenir compte 
		-- des ranges et pas de diminution pour free cases
		when Commission_Rate.Commission_Rate_Free is not null and Scratch_Book.Product_Class_ID in (1,10,11,23)
			then Commission_Rate.Commission_Rate_Free
		-- pour tout autre produits sans ranges 
		when p.profit_min is null and Commission_Rate.Commission_Rate_Free is null
			then scratch_book_commission.commission_rate * 0.5 
		-- pour les produits avec range dont le raising potential n'est pas mis mettre le taux de commission 
		-- par default
		when p.profit_min is not null and Commission_Rate.Commission_Rate_Free is null 
			and scratch_book.raising_potential is null or scratch_book.raising_potential = 0
			then scratch_book_commission.commission_rate * 0.5 
		-- pour les produits avec profit >= au max, taux de commission minimum
		when p.profit_min is not null and Commission_Rate.Commission_Rate_Free is null
			and profit_max <= (1 - Sales_Item.unit_price_sold / scratch_book.raising_potential)
			then scratch_book_commission.commission_rate * 0.5 
		-- pour les produits avec profit <= au min, taux de commission maximum
		when p.profit_min is not null and Commission_Rate.Commission_Rate_Free is null 
			and profit_min >= (1 - Sales_Item.unit_price_sold / scratch_book.raising_potential)
			then (scratch_book_commission.commission_rate + convert(decimal(15,4),convert(real, (profit_max - profit_min) * 100 / 5)) * 0.01) * 0.5 
		-- pour tout les autres produits avec range calculs du taux de commission a ajouter
		when p.profit_min is not null and Commission_Rate.Commission_Rate_Free is null 
			and scratch_book.raising_potential is null or scratch_book.raising_potential = 0
			then (scratch_book_commission.commission_rate + convert(decimal(15,4),(convert(real, profit_max - (1 - Sales_Item.unit_price_sold / scratch_book.raising_potential)) * 100 / 5)) * 0.01) * 0.5 
		-- pour tout autres cas mettre le taux par default
		else Commission_Rate.Commission_Rate_Free end
	) as Commission_Rate_Free
	, (case 
		-- default commission tally sales, scratchcard et dicount card always same rate pas tenir compte des ranges et pas de diminution pour free cases
		when Commission_Rate.Commission_Rate_Free is null and Scratch_Book.Product_Class_ID in (1,10,11,23)
			then scratch_book_commission.commission_rate
		-- specifique commission tally sales, scratchcard et dicount card always same rate pas tenir compte des ranges et pas de diminution pour free cases
		when Commission_Rate.Commission_Rate_Free is not null and Scratch_Book.Product_Class_ID in (1,10,11,23)
			then Commission_Rate.Commission_Rate_Free
		-- pour les produits avec range dont le raising potential n'est pas mis mettre le taux de commission par default
		when p.profit_min is not null and Commission_Rate.Commission_Rate_Free is null 
			and scratch_book.raising_potential is null or scratch_book.raising_potential = 0
			then scratch_book_commission.commission_rate
		-- pour les produits avec profit >= au max, taux de commission minimum
		when p.profit_min is not null and Commission_Rate.Commission_Rate_Free is null
			and profit_max <= (1 - Sales_Item.unit_price_sold / scratch_book.raising_potential)
			then scratch_book_commission.commission_rate
		-- pour les produits avec profit <= au min, taux de commission maximum
		when p.profit_min is not null and Commission_Rate.Commission_Rate_Free is null
			and profit_min >= (1 - Sales_Item.unit_price_sold / scratch_book.raising_potential)
			then scratch_book_commission.commission_rate + convert(decimal(15,4), convert(real, (profit_max - profit_min) * 100 / 5) * 0.01)
		-- pour tout les autres produits avec range calculs du taux de commission a ajouter
		when p.profit_min is not null and Commission_Rate.Commission_Rate_Free is null
			and scratch_book.raising_potential is null or scratch_book.raising_potential = 0
			then scratch_book_commission.commission_rate + convert(decimal(15,4), (convert(real, profit_max - (1 - Sales_Item.unit_price_sold / scratch_book.raising_potential)) * 100 / 5)) * 0.01
		-- pour tout autres cas mettre le taux par default
		else scratch_book_commission.commission_rate end 
	) as Commission_Rate_No_Free_test
	, Scratch_Book.Product_Class_ID, 0.01 as Commission_SC
	, (case 
		when scratch_book_commission.commission_rate is null 
		then 0 
		else scratch_book_commission.commission_rate end
	) as default_comm_rate 
from sale inner join sales_item on sales_item.sales_id = sale.sales_id 
inner join scratch_book on sales_item.scratch_book_id = scratch_book.scratch_book_id 
left join Commission_Rate on sales_item.scratch_book_id = commission_Rate.scratch_book_id and commission_Rate.consultant_id = sale.consultant_id 
left join scratch_book_commission on scratch_book_commission.scratch_book_id = sales_item.scratch_book_id 
inner join package p on scratch_book.package_id = p.Package_Id 
WHERE Sale.Sales_ID = @sale_id
GO
