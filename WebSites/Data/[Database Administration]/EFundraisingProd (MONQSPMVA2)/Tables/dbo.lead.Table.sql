USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[lead]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[lead](
	[lead_id] [int] NOT NULL,
	[lead_status_id] [int] NOT NULL,
	[lead_qualification_type_id] [int] NULL,
	[lead_priority_id] [int] NULL,
	[temp_lead_id] [int] NULL,
	[division_id] [tinyint] NOT NULL,
	[promotion_id] [int] NOT NULL,
	[channel_code] [varchar](4) NOT NULL,
	[consultant_id] [int] NOT NULL,
	[group_type_id] [tinyint] NOT NULL,
	[organization_type_id] [tinyint] NOT NULL,
	[hear_id] [tinyint] NOT NULL,
	[fk_kit_type_id] [int] NOT NULL,
	[old_lead_id] [int] NULL,
	[assigner_id] [int] NULL,
	[referee_id] [int] NULL,
	[title_id] [tinyint] NOT NULL,
	[campaign_reason_id] [tinyint] NOT NULL,
	[web_site_id] [int] NOT NULL,
	[promotion_code_id] [int] NULL,
	[activity_closing_reason_id] [tinyint] NOT NULL,
	[ext_consultant_id] [int] NULL,
	[salutation] [varchar](10) NULL,
	[first_name] [varchar](50) NULL,
	[last_name] [varchar](50) NULL,
	[organization] [varchar](100) NULL,
	[street_address] [varchar](100) NULL,
	[city] [varchar](50) NULL,
	[state_code] [varchar](10) NOT NULL,
	[country_code] [varchar](10) NOT NULL,
	[zip_code] [varchar](10) NULL,
	[day_phone] [varchar](20) NULL,
	[day_time_call] [varchar](20) NULL,
	[evening_phone] [varchar](20) NULL,
	[evening_time_call] [varchar](20) NULL,
	[fax] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[lead_entry_date] [datetime] NOT NULL,
	[member_count] [int] NULL,
	[participant_count] [int] NULL,
	[fund_raising_goal] [int] NULL,
	[decision_date] [datetime] NULL,
	[decision_maker] [bit] NOT NULL,
	[committee_meeting_required] [bit] NOT NULL,
	[committee_meeting_date] [datetime] NULL,
	[fund_raiser_start_date] [datetime] NULL,
	[onemaillist] [bit] NOT NULL,
	[faxkit] [bit] NOT NULL,
	[emailkit] [bit] NOT NULL,
	[comments] [varchar](1750) NULL,
	[kit_to_send] [bit] NOT NULL,
	[kit_sent] [bit] NOT NULL,
	[kit_sent_date] [datetime] NULL,
	[lead_assignment_date] [datetime] NULL,
	[interests] [varchar](2800) NULL,
	[has_been_contacted] [bit] NULL,
	[day_phone_ext] [varchar](10) NULL,
	[evening_phone_ext] [varchar](10) NULL,
	[other_phone] [varchar](20) NULL,
	[group_web_site] [varchar](50) NULL,
	[nb_queries] [int] NULL,
	[submit_date] [datetime] NULL,
	[cookie_content] [varchar](255) NULL,
	[first_contact_date] [datetime] NULL,
	[day_phone_is_good] [bit] NOT NULL,
	[evening_phone_is_good] [bit] NOT NULL,
	[account_number] [int] NULL,
	[valid_email] [bit] NOT NULL,
	[other_closing_activity_reason] [varchar](50) NULL,
	[transfered_date] [datetime] NULL,
	[matching_code] [varchar](10) NULL,
	[phone_number_tracking_id] [int] NULL,
	[customer_status_id] [int] NULL,
	[vif] [varchar](15) NULL,
	[address_zone_id] [int] NOT NULL,
	[is_postal_address_validated] [int] NOT NULL,
	[client_status_id] [int] NULL,
	[activation_date] [datetime] NULL,
	[fundraisers_per_year] [tinyint] NULL,
	[wfc_customer_number] [int] NULL,
	[other_phone_is_good] [int] NULL,
 CONSTRAINT [PK_lead] PRIMARY KEY CLUSTERED 
(
	[lead_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_assigner_id_consultant] FOREIGN KEY([assigner_id])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_assigner_id_consultant]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_channel_code] FOREIGN KEY([channel_code])
REFERENCES [dbo].[Lead_Channel] ([Channel_Code])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_channel_code]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_consultant] FOREIGN KEY([consultant_id])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_consultant]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_division] FOREIGN KEY([division_id])
REFERENCES [dbo].[division] ([division_id])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_division]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_hear_about_us] FOREIGN KEY([hear_id])
REFERENCES [dbo].[hear_about_us] ([hear_id])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_hear_about_us]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_lead_activity_closing_reason] FOREIGN KEY([activity_closing_reason_id])
REFERENCES [dbo].[Lead_Activity_Closing_Reason] ([Activity_Closing_Reason_ID])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_lead_activity_closing_reason]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_lead_priority] FOREIGN KEY([lead_priority_id])
REFERENCES [dbo].[Lead_Priority] ([Lead_Priority_Id])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_lead_priority]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_lead_qualification_type] FOREIGN KEY([lead_qualification_type_id])
REFERENCES [dbo].[Lead_Qualification_Type] ([Lead_Qualification_Type_ID])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_lead_qualification_type]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_lead_status] FOREIGN KEY([lead_status_id])
REFERENCES [dbo].[Lead_Status] ([Lead_Status_ID])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_lead_status]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_referee] FOREIGN KEY([referee_id])
REFERENCES [dbo].[Referee] ([Referee_Id])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_referee]
GO
ALTER TABLE [dbo].[lead]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_title] FOREIGN KEY([title_id])
REFERENCES [dbo].[title] ([title_id])
GO
ALTER TABLE [dbo].[lead] CHECK CONSTRAINT [FK_lead_title]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_lead_status_id]  DEFAULT (1) FOR [lead_status_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_division_id]  DEFAULT (1) FOR [division_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_consultant_id]  DEFAULT (0) FOR [consultant_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_group_type_id]  DEFAULT (99) FOR [group_type_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_organization_type_id]  DEFAULT (99) FOR [organization_type_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_hear_id]  DEFAULT (6) FOR [hear_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_fk_kit_type_id]  DEFAULT (0) FOR [fk_kit_type_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_title_id]  DEFAULT (99) FOR [title_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_campaign_reason_id]  DEFAULT (99) FOR [campaign_reason_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_web_site_id]  DEFAULT (2) FOR [web_site_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_activity_closing_reason_id]  DEFAULT (1) FOR [activity_closing_reason_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_lead_entry_date]  DEFAULT (getdate()) FOR [lead_entry_date]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_member_count]  DEFAULT (9) FOR [member_count]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_participant_count]  DEFAULT (0) FOR [participant_count]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_fund_raising_goal]  DEFAULT (0) FOR [fund_raising_goal]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_decision_maker]  DEFAULT (0) FOR [decision_maker]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_committee_meeting_required]  DEFAULT (0) FOR [committee_meeting_required]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_onemaillist]  DEFAULT (1) FOR [onemaillist]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_faxkit]  DEFAULT (1) FOR [faxkit]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_emailkit]  DEFAULT (0) FOR [emailkit]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_kit_to_send]  DEFAULT (0) FOR [kit_to_send]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_kit_sent]  DEFAULT (0) FOR [kit_sent]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_has_been_contacted]  DEFAULT (0) FOR [has_been_contacted]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_nb_queries]  DEFAULT (0) FOR [nb_queries]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_day_phone_is_good]  DEFAULT (1) FOR [day_phone_is_good]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_evening_phone_is_good]  DEFAULT (1) FOR [evening_phone_is_good]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_valid_email]  DEFAULT (1) FOR [valid_email]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_customer_status_id]  DEFAULT (1) FOR [customer_status_id]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_address_zone_id]  DEFAULT (3) FOR [address_zone_id]
GO
ALTER TABLE [dbo].[lead] ADD  DEFAULT (0) FOR [is_postal_address_validated]
GO
ALTER TABLE [dbo].[lead] ADD  CONSTRAINT [DF_lead_other_phone_is_good]  DEFAULT ((1)) FOR [other_phone_is_good]
GO
