USE [eFundweb]
GO
/****** Object:  Table [dbo].[Questions_Entry_Form]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Questions_Entry_Form](
	[Questions_ID] [int] NOT NULL,
	[Entry_Form_ID] [int] NOT NULL,
	[Is_Required] [bit] NULL,
	[Questions_Order] [int] NULL,
	[Insert_Table] [varchar](100) NULL,
	[Insert_Column] [varchar](100) NOT NULL,
	[Default_Value] [varchar](200) NULL,
	[Instance_Visibility] [tinyint] NOT NULL,
 CONSTRAINT [PK_Questions_Entry_Form] PRIMARY KEY CLUSTERED 
(
	[Questions_ID] ASC,
	[Entry_Form_ID] ASC,
	[Insert_Column] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Questions_Entry_Form]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Entry_Form_Entry_Form] FOREIGN KEY([Entry_Form_ID])
REFERENCES [dbo].[Entry_Form] ([Entry_Form_ID])
GO
ALTER TABLE [dbo].[Questions_Entry_Form] CHECK CONSTRAINT [FK_Questions_Entry_Form_Entry_Form]
GO
ALTER TABLE [dbo].[Questions_Entry_Form]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Entry_Form_Questions] FOREIGN KEY([Questions_ID])
REFERENCES [dbo].[Questions] ([Questions_ID])
GO
ALTER TABLE [dbo].[Questions_Entry_Form] CHECK CONSTRAINT [FK_Questions_Entry_Form_Questions]
GO
ALTER TABLE [dbo].[Questions_Entry_Form] ADD  CONSTRAINT [DF_Questions_Entry_Form_Instance_Visibility]  DEFAULT (1) FOR [Instance_Visibility]
GO
