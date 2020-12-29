USE [fastfundraising]
GO
/****** Object:  Table [dbo].[searchitems]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[searchitems](
	[itemid] [int] NULL,
	[itemname] [varchar](100) NULL,
	[searchstring] [varchar](4000) NULL,
	[destination] [varchar](100) NULL,
	[categoryid] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
