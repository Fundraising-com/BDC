USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Automaton_Transition_Function]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Automaton_Transition_Function](
	[Automaton_Id] [int] NOT NULL,
	[State_To_Id] [int] NOT NULL,
	[State_From_Id] [int] NOT NULL,
	[Automaton_Function_Id] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_Automaton_Transition_Function] PRIMARY KEY CLUSTERED 
(
	[Automaton_Id] ASC,
	[State_To_Id] ASC,
	[State_From_Id] ASC,
	[Automaton_Function_Id] ASC,
	[Sequence] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Automaton_Transition_Function]  WITH NOCHECK ADD  CONSTRAINT [fk_ATF_Automaton_Function_Id] FOREIGN KEY([Automaton_Function_Id])
REFERENCES [dbo].[Automaton_Function] ([Automaton_Function_Id])
GO
ALTER TABLE [dbo].[Automaton_Transition_Function] CHECK CONSTRAINT [fk_ATF_Automaton_Function_Id]
GO
ALTER TABLE [dbo].[Automaton_Transition_Function]  WITH NOCHECK ADD  CONSTRAINT [fk_ATF_Automaton_Id_State_To_Id_State_From_Id] FOREIGN KEY([Automaton_Id], [State_To_Id], [State_From_Id])
REFERENCES [dbo].[Automaton_Transition] ([Automaton_Id], [State_To_Id], [State_From_Id])
GO
ALTER TABLE [dbo].[Automaton_Transition_Function] CHECK CONSTRAINT [fk_ATF_Automaton_Id_State_To_Id_State_From_Id]
GO
