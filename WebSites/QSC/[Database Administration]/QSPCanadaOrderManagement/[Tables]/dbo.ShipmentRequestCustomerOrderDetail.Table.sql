USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentRequestCustomerOrderDetail]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShipmentRequestCustomerOrderDetail](
	[ShipmentRequestCustomerOrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[ShipmentRequestCustomerOrderHeaderID] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[ProductCode] [varchar](255) NOT NULL,
	[QtyOrder] [int] NOT NULL,
	[ShipToName] [varchar](255) NOT NULL,
	[ShipToAddress1] [varchar](255) NOT NULL,
	[ShipToAddress2] [varchar](255) NOT NULL,
	[ShipToCity] [varchar](255) NOT NULL,
	[ShipToZipCode] [varchar](255) NOT NULL,
	[ShipToCountry] [varchar](255) NOT NULL,
	[ShipToProvince] [varchar](2) NOT NULL,
	[ShipToContactName] [varchar](35) NOT NULL,
	[ShipToPhoneNumber] [varchar](20) NOT NULL,
 CONSTRAINT [PK_ShipmentRequestCustomerOrderDetail] PRIMARY KEY CLUSTERED 
(
	[ShipmentRequestCustomerOrderDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ShipmentRequestCustomerOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentRequestCustomerOrderDetail_ShipmentRequestCustomerOrderHeader] FOREIGN KEY([ShipmentRequestCustomerOrderHeaderID])
REFERENCES [dbo].[ShipmentRequestCustomerOrderHeader] ([ShipmentRequestCustomerOrderHeaderID])
GO
ALTER TABLE [dbo].[ShipmentRequestCustomerOrderDetail] CHECK CONSTRAINT [FK_ShipmentRequestCustomerOrderDetail_ShipmentRequestCustomerOrderHeader]
GO
