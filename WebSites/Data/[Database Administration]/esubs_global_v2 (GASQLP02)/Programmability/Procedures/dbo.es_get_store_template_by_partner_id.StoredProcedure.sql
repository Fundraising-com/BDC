USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_store_template_by_partner_id]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_store_template_by_partner_id] @Partner_id int AS
begin

select st.store_template_id, st.culture_code, st.store_id, st.aggregator_id, st.account_number, st.description, st.create_date, st.subdivision_code, st.opportunity_id
from Store_template st inner join partner_store_template pst on st.store_template_id = pst.store_template_id
and pst.partner_id = @Partner_id

end
GO
