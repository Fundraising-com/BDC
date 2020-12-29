USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[client_activity_type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[client_activity_type](
	[client_activity_type_id] [tinyint] NOT NULL,
	[carrier_shipping_status_id] [tinyint] NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_client_activity_type] PRIMARY KEY CLUSTERED 
(
	[client_activity_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[client_activity_type]  WITH NOCHECK ADD  CONSTRAINT [FK_client_activity_type_carrier_shipping_status] FOREIGN KEY([carrier_shipping_status_id])
REFERENCES [dbo].[carrier_shipping_status] ([carrier_shipping_status_id])
GO
ALTER TABLE [dbo].[client_activity_type] CHECK CONSTRAINT [FK_client_activity_type_carrier_shipping_status]
GO
