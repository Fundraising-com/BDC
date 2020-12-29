USE [eFundweb]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Images](
	[Images_ID] [int] NOT NULL,
	[Template_ID] [int] NOT NULL,
	[Images_Path] [varchar](120) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Images_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Images]  WITH NOCHECK ADD  CONSTRAINT [FK_Images_Template] FOREIGN KEY([Template_ID])
REFERENCES [dbo].[Template] ([Template_ID])
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_Template]
GO
