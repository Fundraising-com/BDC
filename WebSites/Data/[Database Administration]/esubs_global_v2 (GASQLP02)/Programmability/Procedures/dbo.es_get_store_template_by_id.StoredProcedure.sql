USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_store_template_by_id]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Store_template
CREATE PROCEDURE [dbo].[es_get_store_template_by_id] @Store_template_id int AS
begin

select store_template_id, culture_code, store_id, aggregator_id, account_number, [description], create_date, subdivision_code, opportunity_id
from Store_template where Store_template_id=@Store_template_id

end
GO
