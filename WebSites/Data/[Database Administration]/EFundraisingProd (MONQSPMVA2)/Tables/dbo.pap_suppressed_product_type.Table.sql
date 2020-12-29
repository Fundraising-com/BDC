USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[pap_suppressed_product_type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[pap_suppressed_product_type](
	[pap_suppressed_product_type_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[application_id] [int] NULL,
	[ext_product_type_id] [varchar](50) NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_pap_suppressed_product_type] PRIMARY KEY CLUSTERED 
(
	[pap_suppressed_product_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[pap_suppressed_product_type]  WITH CHECK ADD  CONSTRAINT [FK_pap_suppressed_product_type_application_id] FOREIGN KEY([application_id])
REFERENCES [dbo].[application] ([application_id])
GO
ALTER TABLE [dbo].[pap_suppressed_product_type] CHECK CONSTRAINT [FK_pap_suppressed_product_type_application_id]
GO
ALTER TABLE [dbo].[pap_suppressed_product_type] ADD  CONSTRAINT [DF_pap_suppressed_product_type_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
