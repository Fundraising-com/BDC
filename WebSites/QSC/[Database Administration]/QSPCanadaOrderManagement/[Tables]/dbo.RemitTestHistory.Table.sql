USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[RemitTestHistory]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RemitTestHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RunID] [int] NOT NULL,
	[RemitTestID] [int] NOT NULL,
	[ResultCodeInstance] [int] NOT NULL,
	[TestDate] [datetime] NOT NULL,
 CONSTRAINT [PK_RemitTestHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RemitTestHistory]  WITH CHECK ADD  CONSTRAINT [FK_RemitTestHistory_RemitTest] FOREIGN KEY([RemitTestID])
REFERENCES [dbo].[RemitTest] ([ID])
GO
ALTER TABLE [dbo].[RemitTestHistory] CHECK CONSTRAINT [FK_RemitTestHistory_RemitTest]
GO
