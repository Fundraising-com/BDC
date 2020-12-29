USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[pap_product_category]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[pap_product_category](
	[pap_product_category_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[product_category_code] [varchar](50) NULL,
	[product_category_desc] [varchar](255) NULL,
	[is_default] [bit] NULL,
	[create_date] [datetime] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_pap_product_category] PRIMARY KEY CLUSTERED 
(
	[pap_product_category_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[pap_product_category] ADD  CONSTRAINT [DF_pap_product_category_is_default]  DEFAULT ((0)) FOR [is_default]
GO
ALTER TABLE [dbo].[pap_product_category] ADD  CONSTRAINT [DF_pap_product_category_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[pap_product_category] ADD  CONSTRAINT [DF_pap_product_category_is_active]  DEFAULT ((1)) FOR [is_active]
GO
