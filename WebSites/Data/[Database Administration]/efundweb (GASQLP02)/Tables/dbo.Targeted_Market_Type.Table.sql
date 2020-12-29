USE [eFundweb]
GO
/****** Object:  Table [dbo].[Targeted_Market_Type]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Targeted_Market_Type](
	[Targeted_Market_Type_ID] [int] NOT NULL,
	[Description] [char](50) NULL,
	[Decision_Maker] [bit] NOT NULL,
	[Group_Type_ID] [int] NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_Targeted_Market_Type] PRIMARY KEY CLUSTERED 
(
	[Targeted_Market_Type_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Targeted_Market_Type]  WITH CHECK ADD  CONSTRAINT [FK_Targeted_Market_Type_Group_Type] FOREIGN KEY([Group_Type_ID])
REFERENCES [dbo].[Group_Type] ([Group_Type_ID])
GO
ALTER TABLE [dbo].[Targeted_Market_Type] CHECK CONSTRAINT [FK_Targeted_Market_Type_Group_Type]
GO
