USE [fastfundraising]
GO
/****** Object:  Table [dbo].[saleitems]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[saleitems](
	[saleid] [int] IDENTITY(1001,1) NOT NULL,
	[itemid] [int] NULL,
	[imagepath] [varchar](100) NULL,
	[descriptionpath] [varchar](100) NULL,
	[baseprice] [float] NULL,
	[status] [int] NULL,
	[dateadded] [datetime] NULL,
	[salestartdate] [datetime] NULL,
	[saleenddate] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
