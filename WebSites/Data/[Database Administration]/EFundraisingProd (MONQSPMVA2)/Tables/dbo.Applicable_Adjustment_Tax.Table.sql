USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Applicable_Adjustment_Tax]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Applicable_Adjustment_Tax](
	[Sales_Id] [int] NOT NULL,
	[Adjustement_No] [int] NOT NULL,
	[Tax_Code] [varchar](4) NOT NULL,
	[Tax_Amount] [decimal](15, 4) NULL,
 CONSTRAINT [PK_Applicable_Adjustment_Tax] PRIMARY KEY NONCLUSTERED 
(
	[Sales_Id] ASC,
	[Adjustement_No] ASC,
	[Tax_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Applicable_Adjustment_Tax] ADD  CONSTRAINT [DF_Applicable_Adjustment_Tax_Sales_Id]  DEFAULT (0) FOR [Sales_Id]
GO
ALTER TABLE [dbo].[Applicable_Adjustment_Tax] ADD  CONSTRAINT [DF_Applicable_Adjustment_Tax_Adjustement_No]  DEFAULT (0) FOR [Adjustement_No]
GO
ALTER TABLE [dbo].[Applicable_Adjustment_Tax] ADD  CONSTRAINT [DF_Applicable_Adjustment_Tax_Tax_Amount]  DEFAULT (0) FOR [Tax_Amount]
GO
