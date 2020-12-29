USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sale_SSIS]    Script Date: 02/14/2014 13:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--update sales_item set unit_price_sold = 0 where sales_id = 73155
/*
declare @error nvarchar(50)
exec [dbo].[efrcrm_insert_sale_SSIS] 777, 935,1,'CA',1, 2,1,1,'1-1-1',0,'72309', 1, 77,@error output
print @error
*/

/*select * from scratch_book where product_code = '5551WFC'
select * from wfc_logs
select * from sale where sales_id = 73155
select * from sales_item where sales_id = 73155
*/
--select * from sale order by sales_id desc
--delete from _wfc_import
-- Generate insert stored proc for Sale
CREATE   PROCEDURE [dbo].[efrcrm_insert_sale_SSIS] @wfc_order_no int,
                        @Consultant_id int, @Payment_term_id tinyint, @Client_sequence_code char(2),
                        @Client_id int, @Sales_status_id int, @Payment_method_id tinyint,
                        @Ar_status_id int, @Sales_date datetime, @Shipping_fees decimal(15,4),
                        @item_code nvarchar(15), @item_qty int, @item_sales_amount decimal(15,4),
                        @free_amount decimal(15,4), @wfc_invoice_number nvarchar(15),@scheduled_delivery_date datetime,
                        @error_ nvarchar(50) OUTPUT

AS
begin

set @error_ = '-2'
declare @errorCode int 
declare @Sales_id int
declare @toBeCorrected bit
declare @item_price decimal(15,4)


set @Shipping_fees = isnull(@Shipping_fees,0)


--IGNORE REFUNDS
if @free_amount is null
begin

	--check if order nukmber is already logged
	select @Sales_id = sales_id, @toBeCorrected = toBeCorrected 
						   from wfc_import where order_number = @wfc_order_no

    if @item_sales_amount > 0
    begin  
       set @item_price = @item_sales_amount / @item_qty
    end
	

    if @Sales_id is null --and @toBeCorrected = 0
	begin

		declare @id int
		exec @id = sp_NewID  'Sales_ID', 'All'
		set @Sales_id = @id


		insert into Sale(Sales_id, 
			Consultant_id, 
			Payment_term_id, 
			Client_sequence_code, 
			Client_id, 
			Sales_status_id, 
			Payment_method_id, 
			Ar_status_id, 
			Sales_date, 
			Shipping_fees,
			total_amount,
			wfc_invoice_number,
            scheduled_delivery_date
			
		) 
		values(@Sales_id, 
			@Consultant_id, 
			@Payment_term_id, 
			@Client_sequence_code, 
			@Client_id, 
			@Sales_status_id, 
			@Payment_method_id, 
			@Ar_status_id, 
			@Sales_date, 	
			@Shipping_fees
			,@Shipping_fees --partial total amount
			,@wfc_invoice_number,
            @scheduled_delivery_date
			
		)



		SET @errorCode = @@error
		if (@errorCode = 0)
		begin
		   insert into wfc_import values (@wfc_order_no, @sales_id, 0)
		end


		--insert item---
		declare @sales_item_no int
		if (@item_code is not null)
		begin
	        
			declare @scratchBookID int
			select @scratchBookID = scratch_book_id from scratch_book where is_active <> 0 and replace (product_code,' ','') = @item_code
	        
			if @scratchBookID is not null		
			begin
			   select @sales_item_no = Isnull(max(Sales_item_no),0) + 1 from sales_item where sales_id = @Sales_id
		 		   insert into sales_item (sales_id,sales_item_no, scratch_book_id, quantity_sold, unit_price_sold,
						   quantity_free, sales_amount, paid_amount, adjusted_amount, sales_commission_amount,
    					   sponsor_commission_amount)
			   values(@sales_id,
					  @sales_item_no,
					  @scratchBookID,
					  @item_qty,
					  @item_price,0,@item_sales_amount,
					  0,0,0,0)

					--if error
					SET @errorCode = @@error
					if (@errorCode != 0)
					begin
						set @error_ = 'Error inserting item ' + @item_code + ' for new sale ' + cast(@sales_id as nvarchar(50)) + ' --' +  cast(@errorCode as varchar(50))
						select @error_
						return @errorCode  
				   end	
				   else
				   begin 
	                 
					  --update total_amount
					   if (@item_sales_amount is not null)
					   begin
						  update sale set total_amount = @item_sales_amount where sales_id = @sales_id
					   end

					end   
	    
		      
			end
			else
			begin  
			   set @error_ = 'Product ' + @item_code + ' was not found (spaces are removed from database codes)'
			   select @error_
			   return @errorCode
			end  
		end
		else 
		begin
		   set @error_ = 'Product is blank'
		   select @error_
		   return @errorCode
		end
		----insert item-----
		
	end
	else --SALE IS INSTERTED BUT MORE INFO NEEDS INSERTION (ITEM or Freight)
	begin
	  
		update wfc_import set toBeCorrected = 0 where order_number = @wfc_order_no

		--get the sales total amount to update it
		declare @total_amount decimal
		select @total_amount = total_amount from sale where sales_id = @sales_id

		set @error_ = cast(@sales_id as varchar(50))
		if (@shipping_fees > 0) --when the field is blank is the excel, sheet we dont want to update an existing value
		begin
		   update sale set shipping_fees = @shipping_fees, 
						   total_amount = @total_amount + @shipping_fees  where sales_id = @sales_id
		end
	 
		--if error
		SET @errorCode = @@error
		if (@errorCode != 0)
		begin
			set @error_ = 'Error updating sale ' + @sales_id + ' --' + cast(@errorCode as varchar(50))
			select @error_
			return @errorCode
		end 


		--insert item---

		if (@item_code is not null)
		begin
	        
	             
			select @scratchBookID = scratch_book_id from scratch_book where replace (product_code,' ','') = @item_code
	        
			if @scratchBookID is not null		
			begin
			   select @sales_item_no = Isnull(max(Sales_item_no),0) + 1 from sales_item where sales_id = @Sales_id
			   insert into sales_item (sales_id,sales_item_no, scratch_book_id, quantity_sold, unit_price_sold,
						   quantity_free, sales_amount, paid_amount, adjusted_amount, sales_commission_amount,
    					   sponsor_commission_amount)
			   values(@sales_id,
					  @sales_item_no,
					  @scratchBookID,
					  @item_qty,
					  @item_price,0,@item_sales_amount,
					  0,0,0,0)

					--if error
					SET @errorCode = @@error
					if (@errorCode != 0)
					begin
						set @error_ = 'Error inserting item ' + @item_code + ' for sale ' + cast(@sales_id as nvarchar(50)) + ' --' +  cast(@errorCode as varchar(50))
						select @error_
						return @errorCode  
				   end	
				   else
				   begin 
	                 
					  --update total_amount
					   if (@item_sales_amount is not null)
					   begin
						  update sale set total_amount = @total_amount + @item_sales_amount where sales_id = @sales_id
					   end

					end    
		      
			end
			else
			begin  
			   set @error_ = 'Product ' + @item_code + ' was not found (spaces are removed from database codes)'
			   select @error_
			   return @errorCode
			end  
		end
		else 
		begin
		   set @error_ = 'Product is blank !'
		   select @error_
		   return @errorCode
		end
		----insert item-----




		--if error
		SET @errorCode = @@error
		if (@errorCode != 0)
		begin
			set @error_ = cast(@errorCode as varchar(50))
			select @error_
			return @errorCode
		end 

	end



	set @error_ = 'Sale ' + cast(@sales_id as varchar(50)) + ' was successfully updated'
	select @error_
	return @errorCode


	--select * from sale order by sales_id desc
	--select * from sales_item where sales_id = 73066
	--select * from _wfc_import
	--select * from product_class
	--select replace (product_code,' ','') from scratch_book where product_class_id = 4
	end
end  --if free amount


--select  scratch_book_id from scratch_book where replace (product_code,' ','') = '72309'
GO
