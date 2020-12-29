USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_client]    Script Date: 02/14/2014 13:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[efrcrm_update_client]
	(@client_id 	[int],
	 @client_sequence_code 	[char](2),
	 @organization_class_code 	[varchar](4),
	 @group_type_id 	[tinyint],
	 @channel_code 	[varchar](4),
	 @promotion_id 	[int],
	 @lead_id 	[int],
	 @division_id 	[tinyint],
	 @csr_consultant_id 	[int],
	 @title_id 	[tinyint],
	 @salutation 	[varchar](10),
	 @first_name 	[varchar](50),
	 @last_name 	[varchar](50),
	 @title 	[varchar](50),
	 @organization 	[varchar](100),
	 @day_phone 	[varchar](20),
	 @day_time_call 	[varchar](45),
	 @evening_phone 	[varchar](20),
	 @evening_time_call 	[varchar](20),
	 @fax 	[varchar](20),
	 @email 	[varchar](50),
	 @extra_comment 	[text],
	 @interested_in_agent 	[bit],
	 @interested_in_online 	[bit],
	 @day_phone_ext 	[varchar](10),
	 @evening_phone_ext 	[varchar](10),
	 @other_phone 	[varchar](20),
	 @other_phone_ext 	[varchar](10))
	 

AS 


Update Client set
	 organization_class_code = @organization_class_code,
	 group_type_id = @group_type_id,
	 channel_code = @channel_code,
	 promotion_id = @promotion_id,
	 lead_id = @lead_id,
	 division_id = @division_id,
	 csr_consultant_id = @csr_consultant_id,
	 title_id = @title_id,
	 salutation = @salutation,
	 first_name = @first_name,
	 last_name = @last_name,
	 title = @title,
	 organization = @organization,
	 day_phone = @day_phone,
	 day_time_call = @day_time_call,
	 evening_phone = @evening_phone,
	 evening_time_call = @evening_phone,
	 fax = @fax,
	 email = @email,
	 extra_comment = @extra_comment,
	 interested_in_agent = @interested_in_agent,
	 interested_in_online = @interested_in_online,
	 day_phone_ext = @day_phone_ext,
	 evening_phone_ext = @evening_phone_ext,
	 other_phone = @other_phone,
	 other_phone_ext = @other_phone_ext
where client_id = @client_id and client_sequence_code = @client_sequence_code
GO
