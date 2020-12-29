USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[postal_address]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[postal_address](
	[postal_address_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[address_1] [varchar](100) NULL,
	[address_2] [varchar](100) NULL,
	[city] [varchar](100) NULL,
	[zip_code] [varchar](10) NULL,
	[country_code] [nvarchar](2) NULL,
	[subdivision_code] [nvarchar](7) NULL,
	[create_date] [datetime] NOT NULL,
	[matching_code] [varchar](10) NULL,
	[is_validated] [int] NULL,
 CONSTRAINT [PK_postal_address] PRIMARY KEY CLUSTERED 
(
	[postal_address_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[postal_address]  WITH NOCHECK ADD  CONSTRAINT [FK_postal_address_subdivision] FOREIGN KEY([subdivision_code])
REFERENCES [dbo].[subdivision] ([subdivision_code])
GO
ALTER TABLE [dbo].[postal_address] CHECK CONSTRAINT [FK_postal_address_subdivision]
GO
ALTER TABLE [dbo].[postal_address] ADD  CONSTRAINT [DF_postal_address_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
