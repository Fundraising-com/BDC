USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[postal_address]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[postal_address](
	[postal_address_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[address] [varchar](100) NULL,
	[city] [varchar](100) NULL,
	[zip_code] [varchar](10) NULL,
	[country_code] [nvarchar](10) NULL,
	[subdivision_code] [nvarchar](7) NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_postal_address] PRIMARY KEY CLUSTERED 
(
	[postal_address_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[postal_address] ADD  CONSTRAINT [DF_postal_address_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
