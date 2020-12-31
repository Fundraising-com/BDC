USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[Adjustment_Type_Recipient]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adjustment_Type_Recipient](
	[Adjustment_Type_Recipient_ID] [int] IDENTITY(1,1) NOT NULL,
	[Recipient] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Adjustment_Type_Recipient] PRIMARY KEY CLUSTERED 
(
	[Adjustment_Type_Recipient_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
