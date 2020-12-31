USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateTaxRegInfo]    Script Date: 06/07/2017 09:33:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdateTaxRegInfo] AS

-- Saqib - 07 july 2005
--this proc update gst and qst registration numbers in product table from TaxMagRegistration
set nocount on 

Declare @Title_Code varchar(10),@tax_id int ,@Tax_Registration_Number varchar(30), @cnt int
set @cnt = 0

 --GST Reg#
  DECLARE C_GST Cursor  For
  Select  Title_Code,tax_id,Tax_Registration_Number
  From QspCanadaCommon.dbo.TaxMagRegistration 
  Where tax_id = 1
  Order by title_code,tax_id; --test

--QST Reg #
  DECLARE C_QST Cursor  For
  Select  Title_Code,tax_id,Tax_Registration_Number
  From QspCanadaCommon.dbo.TaxMagRegistration 
  Where tax_id = 3
  Order by title_code,tax_id; --test

BEGIN
	   
	  OPEN C_GST
	    FETCH NEXT FROM C_GST INTO @Title_Code,@Tax_Id,@Tax_Registration_Number
	    WHILE @@FETCH_Status = 0
		

              BEGIN
	
	--Spring 2005 GST
	 Update qspcanadaproduct.dbo.product
	 Set   GST_registration_nbr  = @Tax_Registration_Number
	 where product_code  = @Title_Code
	 and product_season = 'S' and product_year  = 2005  
	-- and type  = 46001
	 and (GST_registration_nbr is null or GST_registration_nbr  = '0')   

	-- Fall 2006 GST
	 Update qspcanadaproduct.dbo.product
	 Set   GST_registration_nbr  = @Tax_Registration_Number
	 where product_code  = @Title_Code
	 and product_season = 'F' and product_year  = 2006  
	-- and type  = 46001
	 and (GST_registration_nbr is null or GST_registration_nbr  = '0')   


	set @cnt = @cnt+1
              
             
  
             FETCH NEXT FROM C_GST INTO @Title_Code,@Tax_Id,@Tax_Registration_Number 

            END

  	CLOSE C_GST
	DEALLOCATE C_GST

	select cast(@cnt as varchar(10) ) + ' GST Products Updated...'

END

--initialising variables
SET @Title_Code  = NULL
SET @Tax_Id = 0
SET @Tax_Registration_Number = NULL
set @cnt = 0

-----QST------------------

BEGIN

	  OPEN C_QST
	    FETCH NEXT FROM C_QST INTO @Title_Code,@Tax_Id,@Tax_Registration_Number
	    WHILE @@FETCH_Status = 0
		

              BEGIN
	
	--Spring 2005 QST
	 Update qspcanadaproduct.dbo.product
	 Set   QST_registration_nbr  = @Tax_Registration_Number
	 where product_code  = @Title_Code
	 and product_season = 'S' and product_year  = 2005  
	-- and type  = 46001
	 and (QST_registration_nbr is null or QST_registration_nbr  = '0')   

	-- Fall 2006 QST
	 Update qspcanadaproduct.dbo.product
	 Set   QST_registration_nbr  = @Tax_Registration_Number
	 where product_code  = @Title_Code
	 and product_season = 'F' and product_year  = 2006  
	-- and type  = 46001
	 and (QST_registration_nbr is null or QST_registration_nbr  = '0')   


	set @cnt = @cnt+1
              
             
  
             FETCH NEXT FROM C_QST INTO @Title_Code,@Tax_Id,@Tax_Registration_Number 

            END

  	CLOSE C_QST
	DEALLOCATE C_QST

	select cast(@cnt as varchar(10) ) + ' QST Products Updated...'

END
GO
