USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerFieldManager]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerFieldManager](
	[FMID] [varchar](4) NOT NULL,
	[CustomerInstance] [int] NOT NULL,
 CONSTRAINT [PK_CustomerFieldManager] PRIMARY KEY CLUSTERED 
(
	[FMID] ASC,
	[CustomerInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
