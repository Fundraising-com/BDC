USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Adjustment]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adjustment](
	[Sales_ID] [int] NOT NULL,
	[Adjustment_No] [int] NOT NULL,
	[Reason_ID] [int] NULL,
	[Adjustment_Date] [datetime] NULL,
	[Adjustment_Amount] [decimal](15, 4) NOT NULL,
	[Comment] [text] NULL,
	[Adjustment_On_Shipping] [decimal](15, 4) NULL,
	[Adjustment_On_Taxes] [decimal](15, 4) NULL,
	[Adjustment_On_Sale_Amount] [decimal](15, 4) NULL,
	[Create_Date] [datetime] NULL,
	[Create_User_ID] [int] NULL,
	[Ext_Adjustment_Id] [int] NULL,
	[charge_id] [int] NULL,
 CONSTRAINT [PK_Adjustment] PRIMARY KEY CLUSTERED 
(
	[Sales_ID] ASC,
	[Adjustment_No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Adjustment]  WITH CHECK ADD  CONSTRAINT [fk_Reason_ID] FOREIGN KEY([Reason_ID])
REFERENCES [dbo].[Reason] ([Reason_ID])
GO
ALTER TABLE [dbo].[Adjustment] CHECK CONSTRAINT [fk_Reason_ID]
GO
ALTER TABLE [dbo].[Adjustment] ADD  CONSTRAINT [DF_Adjustment_Reason_ID]  DEFAULT (0) FOR [Reason_ID]
GO
ALTER TABLE [dbo].[Adjustment] ADD  CONSTRAINT [DF_Adjustment_Adjustment_Amount]  DEFAULT (0) FOR [Adjustment_Amount]
GO
ALTER TABLE [dbo].[Adjustment] ADD  CONSTRAINT [DF_Adjustment_Adjustment_On_Shipping]  DEFAULT (0) FOR [Adjustment_On_Shipping]
GO
ALTER TABLE [dbo].[Adjustment] ADD  CONSTRAINT [DF_Adjustment_Adjustment_On_Taxes]  DEFAULT (0) FOR [Adjustment_On_Taxes]
GO
ALTER TABLE [dbo].[Adjustment] ADD  CONSTRAINT [DF_Adjustment_Adjustment_On_Sale_Amount]  DEFAULT (0) FOR [Adjustment_On_Sale_Amount]
GO
