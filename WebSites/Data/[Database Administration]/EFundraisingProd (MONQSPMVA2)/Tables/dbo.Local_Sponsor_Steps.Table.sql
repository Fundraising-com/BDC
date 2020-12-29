USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Local_Sponsor_Steps]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Local_Sponsor_Steps](
	[Step_Id] [int] NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_Local_Sponsor_Steps] PRIMARY KEY NONCLUSTERED 
(
	[Step_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Local_Sponsor_Steps] ADD  CONSTRAINT [DF_Local_Sponsor_Steps_Step_Id]  DEFAULT (0) FOR [Step_Id]
GO
