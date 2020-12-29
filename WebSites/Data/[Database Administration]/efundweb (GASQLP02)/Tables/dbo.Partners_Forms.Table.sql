USE [eFundweb]
GO
/****** Object:  Table [dbo].[Partners_Forms]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Partners_Forms](
	[Partner_ID] [int] NOT NULL,
	[Entry_Form_ID] [int] NOT NULL,
	[Recipients] [varchar](600) NULL,
	[Form_Type_ID] [int] NULL,
 CONSTRAINT [PK_Partners_Forms] PRIMARY KEY CLUSTERED 
(
	[Partner_ID] ASC,
	[Entry_Form_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Partners_Forms]  WITH CHECK ADD  CONSTRAINT [FK_Partners_Forms_Entry_Form] FOREIGN KEY([Entry_Form_ID])
REFERENCES [dbo].[Entry_Form] ([Entry_Form_ID])
GO
ALTER TABLE [dbo].[Partners_Forms] CHECK CONSTRAINT [FK_Partners_Forms_Entry_Form]
GO
ALTER TABLE [dbo].[Partners_Forms]  WITH CHECK ADD  CONSTRAINT [FK_Partners_Forms_Form_Type] FOREIGN KEY([Form_Type_ID])
REFERENCES [dbo].[Form_Type] ([Form_Type_ID])
GO
ALTER TABLE [dbo].[Partners_Forms] CHECK CONSTRAINT [FK_Partners_Forms_Form_Type]
GO
