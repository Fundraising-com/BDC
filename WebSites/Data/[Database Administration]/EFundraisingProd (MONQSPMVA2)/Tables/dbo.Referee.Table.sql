USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Referee]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Referee](
	[Referee_Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Lead_Id] [int] NOT NULL,
	[Entry_Date] [smalldatetime] NOT NULL,
	[First_Name] [varchar](25) NOT NULL,
	[Last_Name] [varchar](25) NOT NULL,
	[Email] [varchar](50) NULL,
	[Phone_Number] [varchar](25) NULL,
	[Is_Entered] [bit] NOT NULL,
 CONSTRAINT [PK_Referee] PRIMARY KEY NONCLUSTERED 
(
	[Referee_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Referee]  WITH NOCHECK ADD  CONSTRAINT [FK_referee_lead] FOREIGN KEY([Lead_Id])
REFERENCES [dbo].[lead] ([lead_id])
GO
ALTER TABLE [dbo].[Referee] CHECK CONSTRAINT [FK_referee_lead]
GO
ALTER TABLE [dbo].[Referee] ADD  CONSTRAINT [DF_Referee_Is_Entered]  DEFAULT (0) FOR [Is_Entered]
GO
