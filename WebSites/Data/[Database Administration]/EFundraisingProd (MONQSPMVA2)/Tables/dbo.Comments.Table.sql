USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Comments_ID] [int] NOT NULL,
	[Priority_ID] [int] NULL,
	[Sales_ID] [int] NULL,
	[Consultant_ID] [int] NULL,
	[Lead_ID] [int] NULL,
	[Department_ID] [int] NULL,
	[Entry_Date] [datetime] NULL,
	[Comments] [text] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Comments_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Department] FOREIGN KEY([Department_ID])
REFERENCES [dbo].[Department] ([Department_Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Department]
GO
ALTER TABLE [dbo].[Comments]  WITH NOCHECK ADD  CONSTRAINT [FK_comments_lead] FOREIGN KEY([Lead_ID])
REFERENCES [dbo].[lead] ([lead_id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_comments_lead]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Priority] FOREIGN KEY([Priority_ID])
REFERENCES [dbo].[Priority] ([Priority_ID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Priority]
GO
