USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Detailed_Promotion]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Detailed_Promotion](
	[Promotion_ID] [int] NOT NULL,
	[Promotion_Type_Code] [varchar](4) NOT NULL,
	[Target_Age_Group_Code] [varchar](4) NULL,
	[Target_Gender_Group_Code] [varchar](4) NULL,
	[Target_Group_Code] [varchar](4) NOT NULL,
	[Promotion_Year] [smallint] NOT NULL,
	[Promotion_Month] [smallint] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Quantity_Sent] [int] NOT NULL,
	[Call_Goal] [int] NOT NULL,
	[Card_Budget] [int] NOT NULL,
 CONSTRAINT [PK_Detailed_Promotion] PRIMARY KEY NONCLUSTERED 
(
	[Promotion_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Detailed_Promotion] ADD  CONSTRAINT [DF_Detailed_Promotion_Quantity_Sent]  DEFAULT (0) FOR [Quantity_Sent]
GO
ALTER TABLE [dbo].[Detailed_Promotion] ADD  CONSTRAINT [DF_Detailed_Promotion_Call_Goal]  DEFAULT (0) FOR [Call_Goal]
GO
ALTER TABLE [dbo].[Detailed_Promotion] ADD  CONSTRAINT [DF_Detailed_Promotion_Card_Budget]  DEFAULT (0) FOR [Card_Budget]
GO
