USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_ListInvoiceSectionbyLang]    Script Date: 06/07/2017 09:17:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_ListInvoiceSectionbyLang]

           (	  @InvoiceID    int , @Lang Varchar(2))

RETURNS Varchar(1000)  AS  

/*************************************************************************************************************************
--	CRL 8/2/2011
-- 	Exclude processing fee (Section type 8) from output, because they are included in the first invoice section to 
--	appear on the invoice
*************************************************************************************************************************/

BEGIN 


 DECLARE @SectionName VARCHAR(500)
 DECLARE @v_name VARCHAR(500)
 DECLARE @rec int

SET @rec = 0
 

Declare Cur_progname Cursor  For
select CASE Section_Type_Id
		WHEN 1 THEN (CASE @Lang
					WHEN 'EN' Then 'Gift'   --Inventory Products
					WHEN 'FR' Then 'Cadeau'   --French
					ELSE 'Gift'			
				   END) 	
		WHEN 2 THEN (CASE @Lang
					WHEN 'EN' Then 'Magazine'   --Magazine
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	
		WHEN 3 THEN (CASE @Lang
					WHEN 'EN' Then 'Field Supplies'   --Field Supplies
					WHEN 'FR' Then 'Approvisionnements De Champ'   --French
					ELSE 'Field Supplies'			
				   END)
		WHEN 4 THEN (CASE @Lang
					WHEN 'EN' Then 'Incentives'   --Incentives
					WHEN 'FR' Then 'Incitations'   --French
					ELSE 'Incentives'			
				   END)
		WHEN 5 THEN (CASE @Lang
					WHEN 'EN' Then 'Misc'   --Misc
					WHEN 'FR' Then 'Misc'   --French
					ELSE 'Misc'			
				   END)
		WHEN 6 THEN (CASE @Lang
					WHEN 'EN' Then 'Incentives'   --Inventory products without tax
					WHEN 'FR' Then 'Incitations'   --French
					ELSE 'Incentives'			
				   END)
		WHEN 7 THEN (CASE @Lang
					WHEN 'EN' Then 'Kanata'   --Inventory products without tax
					WHEN 'FR' Then 'Kanata'   --French
					ELSE 'Kanata'			
				   END)
		WHEN 9 THEN (CASE @Lang
					WHEN 'EN' Then 'Cookie Dough'   --Inventory products without tax
					WHEN 'FR' Then 'Pâte à Biscuits'   --French
					ELSE 'Cookie Dough'			
				   END)		
	    WHEN 10 THEN (CASE @Lang
					WHEN 'EN' Then 'Chocolate'   --Inventory products without tax
					WHEN 'FR' Then 'Chocolat'   --French
					ELSE 'Chocolate'			
				   END)					   
	    WHEN 11 THEN (CASE @Lang
					WHEN 'EN' Then 'Jewellery'   --Inventory products without tax
					WHEN 'FR' Then 'Bijoux'   --French
					ELSE 'Jewellery'			
				   END)					   
	    WHEN 13 THEN (CASE @Lang
					WHEN 'EN' Then 'Candles'
					WHEN 'FR' Then 'Chandelles'
					ELSE 'Candles'			
				   END)					   
	    WHEN 14 THEN (CASE @Lang
					WHEN 'EN' Then 'To Remember This'
					WHEN 'FR' Then 'Se Souvenir de'
					ELSE 'To Remember This'			
				   END)					   
		ELSE ''
	END AS Description
 from   QSPCanadaFinance.dbo.Invoice_Section s
where   s.Invoice_Id = @InvoiceID and s.Section_Type_ID <> 8 --Exclude processing fees from list since they will not appear as their own section


	OPEN Cur_progname
	    FETCH NEXT FROM Cur_progname INTO @v_name

	    WHILE @@FETCH_Status = 0
                BEGIN

                       SET @rec = @rec +1

                        IF @REC = 1 

                           BEGIN
                             SET  @SectionName  = @v_name
                           END

                        ELSE
                            BEGIN
		     SET  @SectionName  = ISNULL(@SectionName,'') + ' , ' + @v_name
                            END 

                                     
                   FETCH NEXT FROM Cur_progname INTO @v_name
                END
 
 	CLOSE Cur_progname
	DEALLOCATE Cur_progname

  RETURN @SectionName
  
END
GO
