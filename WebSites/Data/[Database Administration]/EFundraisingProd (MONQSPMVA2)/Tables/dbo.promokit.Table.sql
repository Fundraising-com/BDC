USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[promokit]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[promokit](
	[lead_id] [int] NOT NULL,
	[lead_visit_id] [int] NULL,
	[kit_type_id] [int] NOT NULL,
	[carrier_id] [int] NOT NULL,
	[carrier_tracking_id] [int] NULL,
	[street_address] [varchar](100) NULL,
	[city] [varchar](50) NULL,
	[zip_code] [varchar](10) NULL,
	[country_code] [varchar](10) NOT NULL,
	[state_code] [varchar](10) NOT NULL,
	[validated] [int] NOT NULL,
	[create_date] [datetime] NULL,
	[sent_date] [datetime] NULL,
	[done] [int] NOT NULL,
 CONSTRAINT [PK_promokit] PRIMARY KEY CLUSTERED 
(
	[lead_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
