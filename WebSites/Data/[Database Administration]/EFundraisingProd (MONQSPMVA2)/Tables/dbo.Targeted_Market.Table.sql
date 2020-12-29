USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Targeted_Market]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Targeted_Market](
	[Targeted_Market_ID] [int] NOT NULL,
	[Targeted_Market_Type_ID] [int] NOT NULL,
	[Advertising_Support_ID] [int] NOT NULL,
	[Target_Market_Type_ID] [int] NOT NULL,
	[Seasoner] [bit] NOT NULL,
	[Age_Range] [varchar](25) NULL,
	[Education_Level] [varchar](25) NULL,
	[Description] [varchar](50) NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_Targeted_Market] PRIMARY KEY NONCLUSTERED 
(
	[Targeted_Market_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Targeted_Market]  WITH NOCHECK ADD  CONSTRAINT [FK_targeted_market_targeted_market_type] FOREIGN KEY([Targeted_Market_Type_ID])
REFERENCES [dbo].[targeted_market_type] ([targeted_market_type_id])
GO
ALTER TABLE [dbo].[Targeted_Market] NOCHECK CONSTRAINT [FK_targeted_market_targeted_market_type]
GO
ALTER TABLE [dbo].[Targeted_Market]  WITH NOCHECK ADD  CONSTRAINT [fk_TM_Advertising_Support_ID] FOREIGN KEY([Advertising_Support_ID])
REFERENCES [dbo].[Advertising_Support] ([Advertising_Support_ID])
GO
ALTER TABLE [dbo].[Targeted_Market] NOCHECK CONSTRAINT [fk_TM_Advertising_Support_ID]
GO
ALTER TABLE [dbo].[Targeted_Market] ADD  CONSTRAINT [DF_Targeted_Market_Seasoner]  DEFAULT (0) FOR [Seasoner]
GO
