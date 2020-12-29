USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[store_template]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[store_template](
	[store_template_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[store_id] [int] NULL,
	[aggregator_id] [int] NOT NULL,
	[account_number] [int] NULL,
	[description] [varchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[subdivision_code] [nvarchar](7) NULL,
	[opportunity_id] [int] NULL,
 CONSTRAINT [PK_store_template] PRIMARY KEY CLUSTERED 
(
	[store_template_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[store_template]  WITH CHECK ADD FOREIGN KEY([subdivision_code])
REFERENCES [dbo].[subdivision] ([subdivision_code])
GO
ALTER TABLE [dbo].[store_template]  WITH CHECK ADD  CONSTRAINT [FK_store_template_culture] FOREIGN KEY([culture_code])
REFERENCES [dbo].[culture] ([culture_code])
GO
ALTER TABLE [dbo].[store_template] CHECK CONSTRAINT [FK_store_template_culture]
GO
ALTER TABLE [dbo].[store_template] ADD  CONSTRAINT [DF_store_template_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
