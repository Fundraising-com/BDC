USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Question_Entry_Form]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question_Entry_Form](
	[Entry_Form_ID] [int] NOT NULL,
	[Question_ID] [int] NOT NULL,
	[Is_Required] [bit] NOT NULL,
 CONSTRAINT [PK_Question_Entry_Form] PRIMARY KEY NONCLUSTERED 
(
	[Entry_Form_ID] ASC,
	[Question_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Question_Entry_Form]  WITH NOCHECK ADD  CONSTRAINT [fk_qef_entry_form_id] FOREIGN KEY([Entry_Form_ID])
REFERENCES [dbo].[Entry_Form] ([Entry_Form_ID])
GO
ALTER TABLE [dbo].[Question_Entry_Form] CHECK CONSTRAINT [fk_qef_entry_form_id]
GO
ALTER TABLE [dbo].[Question_Entry_Form] ADD  CONSTRAINT [DF_Question_Entry_Form_Is_Required]  DEFAULT (1) FOR [Is_Required]
GO
