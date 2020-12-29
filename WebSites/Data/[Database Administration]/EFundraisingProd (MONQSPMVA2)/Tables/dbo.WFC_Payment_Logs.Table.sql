USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[WFC_Payment_Logs]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFC_Payment_Logs](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[sale_id] [int] NULL,
	[invoice_number] [nchar](50) NULL,
	[message] [nchar](100) NULL,
 CONSTRAINT [PK_WFC_Payment_Logs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
