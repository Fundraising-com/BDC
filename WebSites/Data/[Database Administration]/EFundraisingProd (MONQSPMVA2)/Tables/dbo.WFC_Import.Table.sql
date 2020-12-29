USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[WFC_Import]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFC_Import](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Order_Number] [nvarchar](50) NULL,
	[Sales_ID] [int] NULL,
	[ToBeCorrected] [bit] NOT NULL,
 CONSTRAINT [PK__WFC_Import] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WFC_Import] ADD  CONSTRAINT [DF_WFC_Import_ToBeCorrected]  DEFAULT ((0)) FOR [ToBeCorrected]
GO
