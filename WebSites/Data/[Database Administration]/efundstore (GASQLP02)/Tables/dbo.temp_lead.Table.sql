USE [eFundstore]
GO
/****** Object:  Table [dbo].[temp_lead]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[temp_lead](
	[temp_lead_id] [int] IDENTITY(1,1) NOT NULL,
	[division_id] [int] NULL,
	[promotion_id] [int] NULL,
	[channel_code] [varchar](4) NULL,
	[lead_status_id] [int] NULL,
	[organization_type_id] [tinyint] NOT NULL,
	[campaign_reason_id] [tinyint] NOT NULL,
	[web_site_id] [smallint] NULL,
	[group_type_id] [tinyint] NULL,
	[salutation] [varchar](10) NULL,
	[title_id] [tinyint] NULL,
	[hear_id] [tinyint] NOT NULL,
	[lead_entry_date] [datetime] NULL,
	[first_name] [varchar](50) NULL,
	[last_name] [varchar](50) NULL,
	[organization] [varchar](100) NULL,
	[street_address] [varchar](100) NULL,
	[city] [varchar](50) NULL,
	[state_code] [varchar](10) NULL,
	[country_code] [char](2) NULL,
	[zip_code] [varchar](10) NULL,
	[day_phone] [varchar](20) NULL,
	[day_time_call] [varchar](20) NULL,
	[evening_phone] [varchar](20) NULL,
	[fax] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[participant_count] [int] NULL,
	[fund_raising_goal] [int] NULL,
	[decision_date] [datetime] NULL,
	[decision_maker] [bit] NOT NULL,
	[fund_raiser_start_date] [datetime] NULL,
	[onemaillist] [bit] NOT NULL,
	[comments] [varchar](2000) NULL,
	[day_phone_ext] [varchar](10) NULL,
	[evening_phone_ext] [varchar](10) NULL,
	[other_phone] [varchar](20) NULL,
	[cookie_content] [varchar](255) NULL,
	[group_web_site] [varchar](50) NULL,
	[other_phone_ext] [varchar](10) NULL,
	[isnew] [bit] NOT NULL,
	[opt_in_sweepstakes] [bit] NULL,
 CONSTRAINT [PK_temp_lead] PRIMARY KEY CLUSTERED 
(
	[temp_lead_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_division_id]  DEFAULT (1) FOR [division_id]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_channel_code]  DEFAULT ('INT') FOR [channel_code]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_lead_status_id]  DEFAULT (1) FOR [lead_status_id]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_organization_type_id]  DEFAULT (99) FOR [organization_type_id]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_campaign_reason_id]  DEFAULT (99) FOR [campaign_reason_id]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_hear_id]  DEFAULT (0) FOR [hear_id]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_lead_entry_date]  DEFAULT (getdate()) FOR [lead_entry_date]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_participant_count]  DEFAULT (0) FOR [participant_count]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_fund_raising_goal]  DEFAULT (0) FOR [fund_raising_goal]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_decision_maker]  DEFAULT (0) FOR [decision_maker]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_on_email_list]  DEFAULT (1) FOR [onemaillist]
GO
ALTER TABLE [dbo].[temp_lead] ADD  CONSTRAINT [DF_temp_lead_is_new]  DEFAULT (1) FOR [isnew]
GO
