USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_partner_commission_by_promotion]    Script Date: 02/14/2014 13:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC sp_partner_commission_by_promotion '10/01/2003', '10/30/2003 23:59:59', 8

CREATE   PROCEDURE [dbo].[sp_partner_commission_by_promotion] (@date_from DATETIME = null, @date_to DATETIME = null, @partner_id INT = 8) AS

DECLARE		@partrner_id INT
DECLARE		@promotion_id INT
DECLARE		@promo_desc VARCHAR(100)
DECLARE        	@count_leads INT
DECLARE         @count_bad_leads INT
DECLARE 	@count_good_leads INT
DECLARE 	@cost_by_lead DECIMAL(38, 4)
DECLARE 	@commission_leads DECIMAL(38,4)
DECLARE	        @payment_brochure DECIMAL(38,4)
DECLARE	        @payment_choco DECIMAL(38,4)
DECLARE	        @payment_scratchcard DECIMAL(38,4)
DECLARE		@payment_magazine DECIMAL(38, 4)
DECLARE	        @commission_brochure DECIMAL(38,4)
DECLARE         @commission_choco DECIMAL(38,4)
DECLARE         @commission_scratchcard DECIMAL(38,4)
DECLARE 	@commission_magazine DECIMAL(38, 4)
DECLARE         @rate_brochure DECIMAL(38,4)
DECLARE         @rate_choco DECIMAL(38,4)
DECLARE         @rate_scratchcard DECIMAL(38,4)
DECLARE 	@rate_magazine DECIMAL(38, 4)



CREATE TABLE #tb_all_commission_info(partner_id INT,
        promotion_id INT,
        promo_desc VARCHAR(100),
        count_leads INT,
        count_bad_leads INT,
	count_good_leads INT, 
	cost_by_lead DECIMAL(38, 4),
	commission_leads DECIMAL(38,4), 
        payment_brochure DECIMAL(38,4),
        payment_choco DECIMAL(38,4),
        payment_scratchcard DECIMAL(38,4), 
	payment_magazine DECIMAL(38, 4), 
        commission_brochure DECIMAL(38,4),
        commission_choco DECIMAL(38,4),
        commission_scratchcard DECIMAL(38,4), 
	commission_magazine DECIMAL(38, 4), 
        rate_brochure DECIMAL(38,4),
        rate_choco DECIMAL(38,4),
        rate_scratchcard DECIMAL(38,4), 
	rate_magazine DECIMAL(38, 4))


CREATE TABLE #tbout(partner_id INT,
        promotion_id INT,
        promo_desc VARCHAR(100),
        count_leads INT,
        count_bad_leads INT,
	count_good_leads INT, 
	cost_by_lead DECIMAL(38, 4),
	commission_leads DECIMAL(38,4), 
        payment_brochure DECIMAL(38,4),
        payment_choco DECIMAL(38,4),
        payment_scratchcard DECIMAL(38,4), 
	payment_magazine DECIMAL(38, 4), 
        commission_brochure DECIMAL(38,4),
        commission_choco DECIMAL(38,4),
        commission_scratchcard DECIMAL(38,4), 
	commission_magazine DECIMAL(38, 4), 
        rate_brochure DECIMAL(38,4),
        rate_choco DECIMAL(38,4),
        rate_scratchcard DECIMAL(38,4), 
	rate_magazine DECIMAL(38, 4))

-- leads
SELECT 	promotion.partner_id, 
	promotion.promotion_id, 
	promotion.description as promo_desc, 
	count(lead_id) as count_leads
INTO #tb_leads
FROM lead, promotion, partner
 WHERE lead.promotion_id = promotion.promotion_id
	AND promotion.partner_id = partner.partner_id 
	AND Promotion.Partner_ID = @partner_id
	AND lead.lead_entry_date BETWEEN @date_from and @date_to
GROUP BY promotion.partner_id, promotion.promotion_id, promotion.description

--SELECT * FROM #tb_leads

-- bad leads
SELECT 	promotion.partner_id, 
	promotion.promotion_id, 
	promotion.description as promo_desc, 
	count(lead_id) as count_bad_leads
INTO #tb_bad
FROM lead, promotion, partner
 WHERE lead.promotion_id = promotion.promotion_id
	AND promotion.partner_id = partner.partner_id 
	AND Promotion.Partner_ID = @partner_id
	AND lead.lead_entry_date BETWEEN @date_from and @date_to
	AND ((lead.day_phone_is_good = 0 AND lead.evening_phone_is_good = 0) or lead.country_code <> 'US') 
GROUP BY promotion.partner_id, promotion.promotion_id, promotion.description

--SELECT * FROM #tb_bad

-- good leads
SELECT 	promotion.partner_id, 
	promotion.promotion_id, 
	promotion.description as promo_desc, 
	count(lead_id) as count_good_leads
INTO #tb_good
FROM lead, promotion, partner
 WHERE lead.promotion_id = promotion.promotion_id
	AND promotion.partner_id = partner.partner_id 
	AND Promotion.Partner_ID = @partner_id
	AND lead.lead_entry_date BETWEEN @date_from and @date_to
	AND ((lead.day_phone_is_good = 1 OR lead.evening_phone_is_good = 1) AND lead.country_code = 'US') 
GROUP BY promotion.partner_id, promotion.promotion_id, promotion.description

--SELECT * FROM #tb_good

-- commission leads
SELECT 	promotion.partner_id, 
	promotion.promotion_id, 
	promotion.description as promo_desc, 
	partner_fixed_cost.cost_by_lead as cost_by_lead, 
	(count(lead_id)*partner_fixed_cost.cost_by_lead) as commission_leads
INTO #tb_leads_commission
FROM lead, promotion, partner, partner_fixed_cost
 WHERE lead.promotion_id = promotion.promotion_id
	AND promotion.partner_id = partner.partner_id 
	AND promotion.partner_ID = @partner_id
	AND partner.partner_id = partner_fixed_cost.partner_id
	AND lead.lead_entry_date BETWEEN @date_from and @date_to
	AND ((lead.day_phone_is_good = 1 OR lead.evening_phone_is_good = 1) AND lead.country_code = 'US') 
GROUP BY promotion.partner_id, promotion.promotion_id, promotion.description, partner_fixed_cost.cost_by_lead

--SELECT * FROM #tb_leads_commission
-- SELECT * FROM Payment
-- payment
SELECT Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description as promo_desc,
       SUM(CASE WHEN Product_Class.product_class_id = 8 THEN (dbo.fct_sale_item_ratio(sale.sales_id, Product_Class.product_class_id)) * Payment.Payment_Amount ELSE 0 END) AS payment_brochure,
       SUM(CASE WHEN 	Product_Class.product_class_id IN (4, 5, 14) THEN (dbo.fct_sale_item_ratio(sale.sales_id, Product_Class.product_class_id)) * Payment.Payment_Amount ELSE 0 END) AS payment_candies,
       SUM(CASE WHEN Product_Class.product_class_id = 1 THEN (dbo.fct_sale_item_ratio(sale.sales_id, Product_Class.product_class_id)) * Payment.Payment_Amount ELSE 0 END) AS payment_scratchcard, 
       SUM(CASE WHEN Product_Class.product_class_id = 13 THEN (dbo.fct_sale_item_ratio(sale.sales_id, Product_Class.product_class_id)) * Payment.Payment_Amount ELSE 0 END) AS payment_magazine
INTO #tb_payment
FROM  dbo.Promotion INNER JOIN
               dbo.Lead ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID LEFT OUTER JOIN
               dbo.Client ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
               dbo.Sale ON dbo.Sale.Client_ID = dbo.Client.Client_ID AND dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code INNER JOIN
               dbo.Sales_Item ON dbo.Sales_Item.Sales_ID = dbo.Sale.Sales_ID INNER JOIN
               dbo.Scratch_Book ON dbo.Scratch_Book.Scratch_Book_ID = dbo.Sales_Item.Scratch_Book_ID INNER JOIN
               dbo.Product_Class ON dbo.Product_Class.Product_Class_ID = dbo.Scratch_Book.Product_Class_ID INNER JOIN
               dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID INNER JOIN
               dbo.Partner_Commission ON dbo.Product_Class.Product_Class_ID = dbo.Partner_Commission.Product_Class_ID AND 
               dbo.Promotion.Partner_ID = dbo.Partner_Commission.Partner_ID
 WHERE Promotion.Partner_ID = @partner_id
	  AND Payment.Payment_Entry_Date BETWEEN @date_from and @date_to
 GROUP BY Promotion.Partner_ID,
          Promotion.Promotion_ID,
          Promotion.Description 

--SELECT * FROM #tb_payment

-- Commission
SELECT 	Promotion.Partner_ID,
       	Promotion.Promotion_ID,
       	Promotion.Description as promo_desc,
	SUM(CASE WHEN Product_Class.product_class_id = 8 THEN dbo.fct_commission_rate(@partner_id, Product_Class.product_class_id) * (dbo.fct_sale_item_ratio(sale.sales_id, Product_Class.product_class_id) * Payment.Payment_Amount) ELSE 0 END) AS commission_brochure,
       	SUM(CASE WHEN Product_Class.product_class_id IN (4, 5, 14) THEN dbo.fct_commission_rate(@partner_id, Product_Class.product_class_id) * (dbo.fct_sale_item_ratio(sale.sales_id, Product_Class.product_class_id) * Payment.Payment_Amount)  ELSE 0 END) AS commission_candies,
       	SUM(CASE WHEN Product_Class.product_class_id = 1 THEN dbo.fct_commission_rate(@partner_id, Product_Class.product_class_id) * (dbo.fct_sale_item_ratio(sale.sales_id, Product_Class.product_class_id) * Payment.Payment_Amount) ELSE 0 END) AS commission_scratchcard, 
       	SUM(CASE WHEN Product_Class.product_class_id = 13 THEN dbo.fct_commission_rate(@partner_id, Product_Class.product_class_id) * (dbo.fct_sale_item_ratio(sale.sales_id, Product_Class.product_class_id) * Payment.Payment_Amount) ELSE 0 END) AS commission_magazine
INTO #tb_commission
FROM  dbo.Promotion INNER JOIN
               dbo.Lead ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID LEFT OUTER JOIN
               dbo.Client ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
               dbo.Sale ON dbo.Sale.Client_ID = dbo.Client.Client_ID AND dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code INNER JOIN
               dbo.Sales_Item ON dbo.Sales_Item.Sales_ID = dbo.Sale.Sales_ID INNER JOIN
               dbo.Scratch_Book ON dbo.Scratch_Book.Scratch_Book_ID = dbo.Sales_Item.Scratch_Book_ID INNER JOIN
               dbo.Product_Class ON dbo.Product_Class.Product_Class_ID = dbo.Scratch_Book.Product_Class_ID INNER JOIN
               dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID INNER JOIN
               dbo.Partner_Commission ON dbo.Product_Class.Product_Class_ID = dbo.Partner_Commission.Product_Class_ID AND 
               dbo.Promotion.Partner_ID = dbo.Partner_Commission.Partner_ID
 WHERE Promotion.Partner_ID = @partner_id
	  AND Payment.Payment_Entry_Date BETWEEN @date_from and @date_to
 GROUP BY Promotion.Partner_ID,
          Promotion.Promotion_ID,
          Promotion.Description

--SELECT * FROM #tb_commission

-- Rate
SELECT dbo.Promotion.Partner_ID, 
	dbo.Promotion.Promotion_ID, 
	dbo.Promotion.Description as promo_desc,
	SUM(CASE WHEN Product_Class.product_class_id = 8 THEN Partner_Commission.commission_rate ELSE 0 END) AS rate_brochure,
       	SUM(CASE WHEN Product_Class.product_class_id = 4 OR Product_Class.product_class_id = 5 OR Product_Class.product_class_id = 14 THEN Partner_Commission.commission_rate ELSE 0 END) AS rate_candies,
       	SUM(CASE WHEN Product_Class.product_class_id = 1 THEN Partner_Commission.commission_rate ELSE 0 END) AS rate_scratchcard, 
       	SUM(CASE WHEN Product_Class.product_class_id = 13 THEN Partner_Commission.commission_rate  ELSE 0 END) AS rate_magazine
INTO #tb_rate
FROM  dbo.Partner_Commission INNER JOIN
               dbo.Product_Class ON dbo.Partner_Commission.Product_Class_ID = dbo.Product_Class.Product_Class_ID INNER JOIN
               dbo.Promotion ON dbo.Partner_Commission.Partner_ID = dbo.Promotion.Partner_ID
 WHERE Promotion.Partner_ID = @partner_id
 GROUP BY Promotion.Partner_ID,
          Promotion.Promotion_ID,
          Promotion.Description




--SELECT * FROM #tb_rate

-- insert leads statistics
DECLARE c1 CURSOR FOR SELECT * FROM #tb_leads ORDER BY partner_id, promotion_id, promo_desc, count_leads
OPEN c1

FETCH NEXT FROM c1 INTO  @partner_id, @promotion_id, @promo_desc, @count_leads
WHILE @@FETCH_STATUS = 0
BEGIN

    INSERT INTO #tb_all_commission_info VALUES(@partner_id, @promotion_id, @promo_desc, @count_leads, 0, 
	0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0,
	0)

    FETCH NEXT FROM c1 INTO @partner_id, @promotion_id, @promo_desc, @count_leads
END

CLOSE c1
DEALLOCATE c1


-- insert bad leads statistics
DECLARE c1 CURSOR FOR SELECT * FROM #tb_bad ORDER BY partner_id, promotion_id
OPEN c1

FETCH NEXT FROM c1 INTO  @partner_id, @promotion_id, @promo_desc, @count_bad_leads
WHILE @@FETCH_STATUS = 0
BEGIN

    INSERT INTO #tb_all_commission_info VALUES(@partner_id, @promotion_id, @promo_desc, 0, @count_bad_leads,
	0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0,
	0)

    FETCH NEXT FROM c1 INTO @partner_id, @promotion_id, @promo_desc, @count_bad_leads
END

CLOSE c1
DEALLOCATE c1

-- insert good leads statistics
DECLARE c1 CURSOR FOR SELECT * FROM #tb_good ORDER BY partner_id, promotion_id
OPEN c1

FETCH NEXT FROM c1 INTO  @partner_id, @promotion_id, @promo_desc, @count_good_leads
WHILE @@FETCH_STATUS = 0
BEGIN

    INSERT INTO #tb_all_commission_info VALUES(@partner_id, @promotion_id, @promo_desc, 0, 0, @count_good_leads,
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0,
	0)

    FETCH NEXT FROM c1 INTO @partner_id, @promotion_id, @promo_desc, @count_good_leads
END

CLOSE c1
DEALLOCATE c1



-- insert commission leads statistics
DECLARE c1 CURSOR FOR SELECT partner_id, promotion_id, promo_desc, cost_by_lead, commission_leads FROM #tb_leads_commission ORDER BY partner_id, promotion_id
OPEN c1

FETCH NEXT FROM c1 INTO  @partner_id, @promotion_id, @promo_desc, @cost_by_lead, @commission_leads
WHILE @@FETCH_STATUS = 0
BEGIN

    INSERT INTO #tb_all_commission_info VALUES(@partner_id, @promotion_id, @promo_desc, 0, 0,
	0, @cost_by_lead, @commission_leads, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0,
	0)

    FETCH NEXT FROM c1 INTO @partner_id, @promotion_id, @promo_desc, @cost_by_lead, @commission_leads
END

CLOSE c1
DEALLOCATE c1

--SELECT * FROM #tb_all_commission_info


-- insert payments statistics
DECLARE c1 CURSOR FOR SELECT partner_id, promotion_id, promo_desc, payment_brochure, payment_candies, payment_scratchcard, payment_magazine FROM #tb_payment ORDER BY partner_id, promotion_id
OPEN c1

FETCH NEXT FROM c1 INTO  @partner_id, @promotion_id, @promo_desc, 
	@payment_brochure, @payment_choco, @payment_scratchcard, @payment_magazine
WHILE @@FETCH_STATUS = 0
BEGIN


    INSERT INTO #tb_all_commission_info VALUES(@partner_id, @promotion_id, @promo_desc, 0, 0, 
	0, 0, 0, @payment_brochure, @payment_choco, @payment_scratchcard, @payment_magazine,
	0, 0, 0, 0, 0, 0, 0,
	0)

    FETCH NEXT FROM c1 INTO  @partner_id, @promotion_id, @promo_desc, 
	@payment_brochure, @payment_choco, @payment_scratchcard, @payment_magazine
END

CLOSE c1
DEALLOCATE c1


-- insert commission statistics
DECLARE c1 CURSOR FOR SELECT partner_id, promotion_id, promo_desc, commission_brochure, commission_candies, commission_scratchcard, commission_magazine FROM #tb_commission ORDER BY partner_id, promotion_id
OPEN c1

FETCH NEXT FROM c1 INTO  @partner_id, @promotion_id, @promo_desc,  
	@commission_brochure, @commission_choco, @commission_scratchcard, @commission_magazine
WHILE @@FETCH_STATUS = 0
BEGIN


    INSERT INTO #tb_all_commission_info VALUES(@partner_id, @promotion_id, @promo_desc, 0, 0, 
	0, 0, 0, 0, 0, 0, 0,
	@commission_brochure, @commission_choco, @commission_scratchcard, @commission_magazine, 0, 0, 0,
	0)

    FETCH NEXT FROM c1 INTO @partner_id, @promotion_id, @promo_desc, 
	@commission_brochure, @commission_choco, @commission_scratchcard, @commission_magazine
END

CLOSE c1
DEALLOCATE c1

-- insert rate statistics
DECLARE c1 CURSOR FOR SELECT partner_id, promotion_id, promo_desc, rate_brochure, rate_candies, rate_scratchcard, rate_magazine FROM #tb_rate ORDER BY partner_id, promotion_id
OPEN c1

FETCH NEXT FROM c1 INTO  @partner_id, @promotion_id, @promo_desc, 
	@rate_brochure, @rate_choco, @rate_scratchcard, @rate_magazine 
WHILE @@FETCH_STATUS = 0
BEGIN


    INSERT INTO #tb_all_commission_info VALUES(@partner_id, @promotion_id, @promo_desc, 0, 0, 
	0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, @rate_brochure, @rate_choco, @rate_scratchcard,
	@rate_magazine)

    FETCH NEXT FROM c1 INTO @partner_id, @promotion_id, @promo_desc, 
	@rate_brochure, @rate_choco, @rate_scratchcard,	@rate_magazine 
END

CLOSE c1
DEALLOCATE c1

SELECT partner_id, promotion_id, promo_desc, SUM(count_leads) as count_by_lead, SUM(count_bad_leads) as count_bad_leads, 
	SUM(count_good_leads) as count_good_leads, SUM(cost_by_lead) as cost_by_lead, SUM(commission_leads) as commission_leads, 
	SUM(payment_brochure) as payment_brochure, SUM(payment_choco) as payment_choco, SUM(payment_scratchcard) as payment_scratchcard,
	SUM(payment_magazine) as payment_magazine, SUM(commission_brochure) as commission_brochure, SUM(commission_choco) as commission_choco,
	SUM(commission_scratchcard) as commission_scratchcard, SUM(commission_magazine) as commission_magazine, SUM(rate_brochure) as rate_brochure,
	SUM(rate_choco) as rate_choco, SUM(rate_scratchcard) as rate_scratchcard, SUM(rate_magazine) as rate_magazine
	FROM #tb_all_commission_info GROUP BY partner_id, promotion_id, promo_desc
GO
