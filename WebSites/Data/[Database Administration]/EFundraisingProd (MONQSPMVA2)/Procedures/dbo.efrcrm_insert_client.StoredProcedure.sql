USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_client]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_insert_client]
	(@client_id 	[int] OUTPUT,
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
declare @id int
exec @id = sp_NewID  'Client_Sequence',@client_sequence_code
set @client_id = @id

INSERT INTO [dbo].[client] 
	 ( [client_sequence_code],
	 [client_id],
	 [organization_class_code],
	 [group_type_id],
	 [channel_code],
	 [promotion_id],
	 [lead_id],
	 [division_id],
	 [csr_consultant_id],
	 [title_id],
	 [salutation],
	 [first_name],
	 [last_name],
	 [title],
	 [organization],
	 [day_phone],
	 [day_time_call],
	 [evening_phone],
	 [evening_time_call],
	 [fax],
	 [email],
	 [extra_comment],
	 [interested_in_agent],
	 [interested_in_online],
	 [day_phone_ext],
	 [evening_phone_ext],
	 [other_phone],
	 [other_phone_ext]) 
 
VALUES 
	( @client_sequence_code,
	 @id,
	 @organization_class_code,
	 @group_type_id,
	 @channel_code,
	 @promotion_id,
	 @lead_id,
	 @division_id,
	 @csr_consultant_id,
	 @title_id,
	 @salutation,
	 @first_name,
	 @last_name,
	 @title,
	 @organization,
	 @day_phone,
	 @day_time_call,
	 @evening_phone,
	 @evening_time_call,
	 @fax,
	 @email,
	 @extra_comment,
	 @interested_in_agent,
	 @interested_in_online,
	 @day_phone_ext,
	 @evening_phone_ext,
	 @other_phone,
	 @other_phone_ext)
GO
