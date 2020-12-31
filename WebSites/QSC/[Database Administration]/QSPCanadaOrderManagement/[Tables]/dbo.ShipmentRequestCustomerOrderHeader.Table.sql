USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentRequestCustomerOrderHeader]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShipmentRequestCustomerOrderHeader](
	[ShipmentRequestCustomerOrderHeaderID] [int] IDENTITY(1,1) NOT NULL,
	[ShipmentRequestOrderID] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[StudentName] [varchar](255) NOT NULL,
	[ClassID] [varchar](255) NOT NULL,
	[ClassName] [varchar](255) NOT NULL,
 CONSTRAINT [PK_ShipmentRequestCustomerOrderHeader] PRIMARY KEY CLUSTERED 
(
	[ShipmentRequestCustomerOrderHeaderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ShipmentRequestCustomerOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentRequestCustomerOrderHeader_ShipmentRequestOrder] FOREIGN KEY([ShipmentRequestOrderID])
REFERENCES [dbo].[ShipmentRequestOrder] ([ShipmentRequestOrderID])
GO
ALTER TABLE [dbo].[ShipmentRequestCustomerOrderHeader] CHECK CONSTRAINT [FK_ShipmentRequestCustomerOrderHeader_ShipmentRequestOrder]
GO
