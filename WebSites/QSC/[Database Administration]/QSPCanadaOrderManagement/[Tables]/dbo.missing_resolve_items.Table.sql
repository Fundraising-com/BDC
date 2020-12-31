USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[missing_resolve_items]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[missing_resolve_items](
	[OrderID] [int] NOT NULL,
	[xml_path] [varchar](255) NOT NULL,
	[search_date] [datetime] NULL,
	[searched_by] [varchar](20) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
