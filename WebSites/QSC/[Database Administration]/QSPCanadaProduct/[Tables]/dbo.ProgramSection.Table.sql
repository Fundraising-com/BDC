USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[ProgramSection]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProgramSection](
	[ID] [int] NOT NULL,
	[Program_ID] [int] NULL,
	[Type] [int] NULL,
	[CatalogCode] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [dbo].[UserID_UDDT] NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [dbo].[UserID_UDDT] NULL,
	[sub_product_code] [varchar](20) NULL,
 CONSTRAINT [PK_ProgramSection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
