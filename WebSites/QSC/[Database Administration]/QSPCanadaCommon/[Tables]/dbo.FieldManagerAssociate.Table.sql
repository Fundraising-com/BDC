USE [QSPCanadaCommon]
GO

/****** Object:  Table [dbo].[FieldManagerAssociate]    Script Date: 7/2/2018 10:34:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FieldManagerAssociate](
	[FieldManagerAssociateID] [int] IDENTITY(1,1) NOT NULL,
	[AssociateFMID] [varchar](4) NOT NULL,
	[FMID] [varchar](4) NOT NULL,
	[Percentage] [numeric](5, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[EffectiveToDate] [datetime] NOT NULL,
 CONSTRAINT [PK_FieldManagerAssociate] PRIMARY KEY CLUSTERED 
(
	[FieldManagerAssociateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FieldManagerAssociate]  WITH CHECK ADD  CONSTRAINT [FK_FieldManagerAssociate_FieldManager] FOREIGN KEY([FMID])
REFERENCES [dbo].[FieldManager] ([FMID])
GO

ALTER TABLE [dbo].[FieldManagerAssociate] CHECK CONSTRAINT [FK_FieldManagerAssociate_FieldManager]
GO

ALTER TABLE [dbo].[FieldManagerAssociate]  WITH CHECK ADD  CONSTRAINT [FK_FieldManagerAssociate_FieldManager_Associate] FOREIGN KEY([AssociateFMID])
REFERENCES [dbo].[FieldManager] ([FMID])
GO

ALTER TABLE [dbo].[FieldManagerAssociate] CHECK CONSTRAINT [FK_FieldManagerAssociate_FieldManager_Associate]
GO