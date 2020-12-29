USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Proposal_Tags]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proposal_Tags](
	[Proposal_ID] [int] NOT NULL,
	[Tags_ID] [int] NOT NULL,
 CONSTRAINT [PK_Proposal_Tags] PRIMARY KEY NONCLUSTERED 
(
	[Proposal_ID] ASC,
	[Tags_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Proposal_Tags]  WITH CHECK ADD  CONSTRAINT [fk_pt_proposal_id] FOREIGN KEY([Proposal_ID])
REFERENCES [dbo].[Proposal] ([Proposal_ID])
GO
ALTER TABLE [dbo].[Proposal_Tags] CHECK CONSTRAINT [fk_pt_proposal_id]
GO
ALTER TABLE [dbo].[Proposal_Tags]  WITH CHECK ADD  CONSTRAINT [fk_pt_tags_id] FOREIGN KEY([Tags_ID])
REFERENCES [dbo].[Tags] ([Tags_ID])
GO
ALTER TABLE [dbo].[Proposal_Tags] CHECK CONSTRAINT [fk_pt_tags_id]
GO
