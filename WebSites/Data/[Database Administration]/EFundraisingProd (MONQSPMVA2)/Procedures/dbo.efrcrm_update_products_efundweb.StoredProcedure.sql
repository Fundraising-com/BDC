USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_products_efundweb]    Script Date: 02/14/2014 13:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     procedure [dbo].[efrcrm_update_products_efundweb] --4078
                     @Root_package_id int
as 

declare @scratch_book_id int
declare @name varchar(200)
declare @raising_potential decimal
declare @is_active bit
declare @product_code varchar(10)
declare @short_desc varchar(200)
declare @product_id int
declare @real_product_id int
declare @package_id int
declare @sb_package_id int
declare @sb_product_class_id int
declare @product_class_id int
declare @product_class_package_id int
declare @package_package_id int
declare @parent_package_id int
declare @product_class_name varchar(50)
declare @current_description varchar(50)
declare @product_name varchar(50)
declare @package_name varchar(50)
declare @exists bit
declare @update bit
declare @create_date datetime

set @update = 0

--get products updates
WHILE EXISTS ( select * from scratch_book where replicated = 0)
begin
          select top 1
              @scratch_book_id = scratch_book_id, 
                 @sb_package_id =package_id, 
                 @sb_product_class_id = product_class_id,
                 @name = description,
                 @current_description = current_description ,
                 @raising_potential = raising_potential,
                 @is_active = is_active,
                 @product_code = product_code  
              from scratch_book where replicated = 0

    --First find the product class id of the product on efundstore side
    --Find the parent package id of the product on efundstore side    

    select @product_class_name = [description] from product_class where product_class_id = @sb_product_class_id   

    select @product_class_package_id = package_id from efundstore.dbo.package where name = @product_class_name 
           and parent_package_id = @Root_package_id --4078

    if @sb_package_id = 0 
    begin
        set @parent_package_id = @product_class_package_id
    end
    else
    begin
       --since there is a package, the parent package of the product is the not the product class, lets find
       --the efundstore package under the product class
       select @package_name = description from package where package_id = @sb_package_id   
       select @parent_package_id = package_id from efundstore.dbo.package where name = @package_name 
           and parent_package_id = @product_class_package_id
    end   

    --validate if we have a parent_package_id
    if @parent_package_id is null
    begin
       --ERROR   
         set @update = -1
         update scratch_book set replicated = 1 where scratch_book_id = @scratch_book_id
    end
    else
    begin
     
    --we have now found the parent package id of the product, lets see if the product updated already axists 
      select @product_id = p.product_id from efundstore.dbo.product p inner join
             efundstore.dbo.product_package pp on p.product_id = pp.product_id
      where pp.package_id = @parent_package_id and p.scratch_book_id = @scratch_book_id

     --if exists
     if @product_id is not null
     begin
          update efundstore.dbo.product set name = @name,
                                             raising_potential = @raising_potential,
                                             product_code = @product_code,
                                             enabled = @is_active,
                                             create_date = getdate()
           where (product_id = @product_id) 

           update efundstore.dbo.product_desc set name = @name, short_desc = isnull(@current_description,'')
           where (product_id = @product_id) 

     end 
     else
     begin
         
         -- BEGIN TRAN
          set @create_date = getdate()
          insert into efundstore.dbo.product (scratch_book_id,name,raising_potential, product_code, enabled, create_date)
                     values (@scratch_book_id, @name, @raising_potential, @product_code,1,@create_date)
 
          SELECT @product_id = product_id from efundstore.dbo.product
                 where scratch_book_id = @scratch_book_id and create_date = @create_date 
        

          insert into efundstore.dbo.product_desc (product_id,culture_code, name,short_desc, long_desc, enabled, create_date)
                     values (@product_id, 'en-US', @name, isnull(@current_description,''),'',1,getdate())
 
           --insert product package relation
           insert into efundstore.dbo.product_package (product_id, package_id,display, create_date)
  	                   values (@product_id, @parent_package_id, 1,getdate())
         --  COMMIT TRAN

     end

     update scratch_book set replicated = 1 where scratch_book_id = @scratch_book_id
     set @update = 1;

   end


  
end

  select @update
GO
