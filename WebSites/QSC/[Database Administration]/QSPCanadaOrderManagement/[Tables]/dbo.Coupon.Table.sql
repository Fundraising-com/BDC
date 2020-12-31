USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Coupon](
	[ID] [varchar](50) NOT NULL,
	[CouponSetID] [int] NOT NULL,
	[IsUsed] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
