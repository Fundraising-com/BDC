USE [fastfundraising]
GO
/****** Object:  Table [dbo].[ffcart]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ffcart](
	[cookieid] [int] NULL,
	[ipaddr] [varchar](50) NULL,
	[itemid] [int] NULL,
	[itemname] [varchar](100) NULL,
	[qty] [int] NULL,
	[itempriceapplied] [float] NULL,
	[totalcost] [float] NULL,
	[addtocartdatetime] [datetime] NULL,
	[extfmid] [int] NULL,
	[promotionid] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
