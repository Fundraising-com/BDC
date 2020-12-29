USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[lead_history]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[lead_history](
	[transaction_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[lead_id] [int] NOT NULL,
	[promotion_id] [int] NOT NULL,
	[channel_code] [varchar](4) NOT NULL,
	[consultant_id] [int] NOT NULL,
	[first_name] [varchar](50) NULL,
	[last_name] [varchar](50) NULL,
	[organization] [varchar](200) NULL,
	[street_address] [varchar](100) NULL,
	[city] [varchar](50) NULL,
	[state_code] [varchar](10) NOT NULL,
	[country_code] [varchar](10) NOT NULL,
	[zip_code] [varchar](10) NULL,
	[day_phone] [varchar](20) NULL,
	[evening_phone] [varchar](20) NULL,
	[fax] [varchar](20) NULL,
	[email] [varchar](100) NULL,
	[lead_entry_date] [datetime] NOT NULL,
	[participant_count] [int] NULL,
	[transaction_date] [datetime] NOT NULL,
 CONSTRAINT [PK_lead_history] PRIMARY KEY CLUSTERED 
(
	[transaction_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[lead_history] ADD  CONSTRAINT [DF_lead_history_transaction_date]  DEFAULT (getdate()) FOR [transaction_date]
GO
