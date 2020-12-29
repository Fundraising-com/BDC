USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[WFC_Import_Payments]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFC_Import_Payments](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Invoice_number] [nvarchar](50) NOT NULL,
	[ToBeCorrected] [bit] NOT NULL,
 CONSTRAINT [PK_WFC_Import_Payments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WFC_Import_Payments] ADD  CONSTRAINT [DF_WFC_Import_Payments_ToBeCorrected]  DEFAULT ((0)) FOR [ToBeCorrected]
GO
