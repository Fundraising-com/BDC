USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[SchoolPopOrderTable]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SchoolPopOrderTable](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[AccountID] [int] NULL,
	[SchoolPopAccountID] [varchar](50) NULL,
	[BFreeID] [varchar](50) NULL,
	[OrderTypeDesignator] [int] NULL,
	[DateBFreeUpload] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](4) NULL,
 CONSTRAINT [PK_SchoolPopOrderTable_1__13] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
