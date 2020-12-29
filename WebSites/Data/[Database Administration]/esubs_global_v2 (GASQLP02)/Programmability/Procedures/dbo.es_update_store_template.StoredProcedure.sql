USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_store_template]    Script Date: 02/14/2014 13:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Store_template
CREATE PROCEDURE [dbo].[es_update_store_template] @Store_template_id int, @Culture_code nvarchar(10), @Store_id int, @Aggregator_id int, @Account_number int, @Description varchar(255), @Create_date datetime AS
begin

update Store_template set Culture_code=@Culture_code, Store_id=@Store_id, Aggregator_id=@Aggregator_id, Account_number=@Account_number, Description=@Description, Create_date=@Create_date where Store_template_id=@Store_template_id

end
GO
