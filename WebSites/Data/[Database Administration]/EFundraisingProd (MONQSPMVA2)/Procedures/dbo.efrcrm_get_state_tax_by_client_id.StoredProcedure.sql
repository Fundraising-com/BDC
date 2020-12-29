USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_state_tax_by_client_id]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[efrcrm_get_state_tax_by_client_id] --26663, 'ui'
            @client_id as int 
           ,@client_sequence_code varchar(5)
   
           
as
begin

declare @state varchar(3)
select @state = state_code from client_address where address_type = 'BT' and client_id = @client_id and client_sequence_code = @client_sequence_code
--print @state

select a.state_code, a.tax_code, a.tax_rate, a.tax_order
from state_tax a
inner join (
    select tax_code, max(effective_date) effective_date
    from state_tax 
    where state_code = @state
    group by tax_code
)b on a.tax_code = b.tax_code and a.effective_date = b.effective_date
where state_code = @state


end
GO
