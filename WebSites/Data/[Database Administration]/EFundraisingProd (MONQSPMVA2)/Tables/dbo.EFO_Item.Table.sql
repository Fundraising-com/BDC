USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Item]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFO_Item](
	[Item_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Title] [varchar](20) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Amount2Supplier] [decimal](10, 2) NOT NULL,
	[Amount2Group] [decimal](10, 2) NOT NULL,
	[Description] [varchar](150) NULL,
 CONSTRAINT [PK_EFO_Item] PRIMARY KEY NONCLUSTERED 
(
	[Item_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
