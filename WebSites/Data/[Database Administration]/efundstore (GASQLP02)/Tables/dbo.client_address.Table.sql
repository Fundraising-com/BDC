USE [eFundstore]
GO
/****** Object:  Table [dbo].[client_address]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[client_address](
	[address_id] [int] NOT NULL,
	[client_sequence_code] [char](2) NOT NULL,
	[client_id] [int] NOT NULL,
	[address_type] [char](2) NOT NULL,
	[street_address] [varchar](100) NULL,
	[state_code] [varchar](10) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[zip_code] [varchar](10) NULL,
	[country_code] [varchar](10) NOT NULL,
	[attention_of] [varchar](100) NULL,
 CONSTRAINT [PK_client_address] PRIMARY KEY CLUSTERED 
(
	[address_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
