USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[partner_store_template]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_store_template](
	[partner_id] [int] NOT NULL,
	[store_template_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_partner_store_template] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC,
	[store_template_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[partner_store_template]  WITH CHECK ADD  CONSTRAINT [FK_partner_store_template_store_template] FOREIGN KEY([store_template_id])
REFERENCES [dbo].[store_template] ([store_template_id])
GO
ALTER TABLE [dbo].[partner_store_template] CHECK CONSTRAINT [FK_partner_store_template_store_template]
GO
ALTER TABLE [dbo].[partner_store_template] ADD  CONSTRAINT [DF_partner_store_template_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
