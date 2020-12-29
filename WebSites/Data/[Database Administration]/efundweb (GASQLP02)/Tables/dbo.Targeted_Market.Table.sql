USE [eFundweb]
GO
/****** Object:  Table [dbo].[Targeted_Market]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Targeted_Market](
	[Targeted_Market_ID] [int] NOT NULL,
	[Targeted_Market_Type_ID] [int] NULL,
	[Advertising_Support_ID] [int] NULL,
	[Target_Market_Type_ID] [int] NULL,
	[Seasonner] [bit] NOT NULL,
	[Age_Range] [char](25) NULL,
	[Education_Level] [char](25) NULL,
	[Description] [char](50) NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_Targeted_Market] PRIMARY KEY CLUSTERED 
(
	[Targeted_Market_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Targeted_Market]  WITH CHECK ADD  CONSTRAINT [FK_Targeted_Market_Targeted_Market_Type] FOREIGN KEY([Targeted_Market_Type_ID])
REFERENCES [dbo].[Targeted_Market_Type] ([Targeted_Market_Type_ID])
GO
ALTER TABLE [dbo].[Targeted_Market] CHECK CONSTRAINT [FK_Targeted_Market_Targeted_Market_Type]
GO
