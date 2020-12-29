USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Associate_Mentor]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Associate_Mentor](
	[Associate_ID] [int] NOT NULL,
	[Mentor_ID] [int] NOT NULL,
	[Start_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
 CONSTRAINT [PK_Associate_Mentor] PRIMARY KEY NONCLUSTERED 
(
	[Associate_ID] ASC,
	[Mentor_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Associate_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Associate_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Associate_Mentor]  WITH NOCHECK ADD  CONSTRAINT [FK_am_associate_consultant] FOREIGN KEY([Associate_ID])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[Associate_Mentor] CHECK CONSTRAINT [FK_am_associate_consultant]
GO
ALTER TABLE [dbo].[Associate_Mentor]  WITH NOCHECK ADD  CONSTRAINT [FK_am_mentor_consultant] FOREIGN KEY([Mentor_ID])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[Associate_Mentor] CHECK CONSTRAINT [FK_am_mentor_consultant]
GO
