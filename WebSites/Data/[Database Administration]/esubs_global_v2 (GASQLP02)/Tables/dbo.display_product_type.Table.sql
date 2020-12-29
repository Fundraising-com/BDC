USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[display_product_type]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[display_product_type](
	[display_product_type_id] [int] IDENTITY(1,1) NOT NULL,
	[display_id] [int] NOT NULL,
	[external_product_type_id] [int] NOT NULL,
	[store_id] [int] NOT NULL,
	[description] [varchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_display_product_type] PRIMARY KEY CLUSTERED 
(
	[display_product_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[display_product_type] ADD  CONSTRAINT [DF_display_product_type_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
