USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Competitor_Advertising_Support]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Competitor_Advertising_Support](
	[Advertising_Support_ID] [int] NOT NULL,
	[Competitor_Advertising_ID] [int] NOT NULL,
 CONSTRAINT [PK_Competitor_Advertising_Support] PRIMARY KEY NONCLUSTERED 
(
	[Advertising_Support_ID] ASC,
	[Competitor_Advertising_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Competitor_Advertising_Support]  WITH CHECK ADD  CONSTRAINT [fk_CAS_Advertising_Support_ID] FOREIGN KEY([Advertising_Support_ID])
REFERENCES [dbo].[Advertising_Support] ([Advertising_Support_ID])
GO
ALTER TABLE [dbo].[Competitor_Advertising_Support] CHECK CONSTRAINT [fk_CAS_Advertising_Support_ID]
GO
ALTER TABLE [dbo].[Competitor_Advertising_Support]  WITH CHECK ADD  CONSTRAINT [fk_CAS_Competitor_Advertising_ID] FOREIGN KEY([Competitor_Advertising_ID])
REFERENCES [dbo].[Competitor_Advertising] ([Competitor_Advertising_ID])
GO
ALTER TABLE [dbo].[Competitor_Advertising_Support] CHECK CONSTRAINT [fk_CAS_Competitor_Advertising_ID]
GO
