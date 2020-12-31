USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentWayBill]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShipmentWayBill](
	[ShipmentWayBill] [int] IDENTITY(1,1) NOT NULL,
	[ShipmentID] [int] NULL,
	[WayBillNumber] [varchar](50) NULL,
 CONSTRAINT [PK_ShipmentWayBill] PRIMARY KEY CLUSTERED 
(
	[ShipmentWayBill] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
