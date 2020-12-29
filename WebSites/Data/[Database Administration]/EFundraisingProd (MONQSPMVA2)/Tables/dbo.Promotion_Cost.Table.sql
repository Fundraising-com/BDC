USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Promotion_Cost]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion_Cost](
	[Promotion_ID] [int] NOT NULL,
	[Period_Month] [int] NOT NULL,
	[Period_Year] [int] NOT NULL,
	[Cost] [decimal](15, 4) NOT NULL,
 CONSTRAINT [PK_Promotion_Cost] PRIMARY KEY CLUSTERED 
(
	[Promotion_ID] ASC,
	[Period_Month] ASC,
	[Period_Year] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Promotion_Cost] ADD  CONSTRAINT [DF_Promotion_Cost_Cost]  DEFAULT (0) FOR [Cost]
GO
