USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Automaton_State]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Automaton_State](
	[Automaton_Id] [int] NOT NULL,
	[State_Id] [int] NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_Automaton_State] PRIMARY KEY CLUSTERED 
(
	[Automaton_Id] ASC,
	[State_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Automaton_State]  WITH NOCHECK ADD  CONSTRAINT [fk_AS_Automaton_Id] FOREIGN KEY([Automaton_Id])
REFERENCES [dbo].[Automaton] ([Automaton_Id])
GO
ALTER TABLE [dbo].[Automaton_State] CHECK CONSTRAINT [fk_AS_Automaton_Id]
GO
