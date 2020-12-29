USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_store_template]    Script Date: 02/14/2014 13:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Store_template
CREATE PROCEDURE [dbo].[es_insert_store_template] @Store_template_id int OUTPUT, @Culture_code nvarchar(10), @Store_id int, @Aggregator_id int, @Account_number int, @Description varchar(255), @Create_date datetime AS
begin

insert into Store_template(Culture_code, Store_id, Aggregator_id, Account_number, Description, Create_date) values(@Culture_code, @Store_id, @Aggregator_id, @Account_number, @Description, @Create_date)

select @Store_template_id = SCOPE_IDENTITY()

end
GO
