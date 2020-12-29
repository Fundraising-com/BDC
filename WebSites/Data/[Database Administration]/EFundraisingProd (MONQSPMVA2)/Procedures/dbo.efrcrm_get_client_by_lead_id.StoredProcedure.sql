USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_client_by_lead_id]    Script Date: 02/14/2014 13:04:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Client_activity
create PROCEDURE [dbo].[efrcrm_get_client_by_lead_id] @lead_id int 
AS
begin

SELECT Client_sequence_code, Client_id, Organization_class_code, Group_type_id, Channel_code, Promotion_id, Lead_id, Division_id, Csr_consultant_id, Title_id, Salutation, First_name, Last_name, Title, Organization, Day_phone, Day_time_call, Evening_phone, Evening_time_call, Fax, Email, Extra_comment, Interested_in_agent, Interested_in_online, Day_phone_ext, Evening_phone_ext, Other_phone, Other_phone_ext
from Client
WHERE lead_id= @lead_id
end
GO
