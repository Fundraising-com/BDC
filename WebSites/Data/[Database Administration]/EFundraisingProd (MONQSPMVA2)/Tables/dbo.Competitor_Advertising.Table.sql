USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Competitor_Advertising]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Competitor_Advertising](
	[Competitor_Advertising_ID] [int] NOT NULL,
	[Competitor_ID] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Publicity_Duration] [varchar](25) NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_Competitor_Advertising] PRIMARY KEY NONCLUSTERED 
(
	[Competitor_Advertising_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Competitor_Advertising]  WITH CHECK ADD  CONSTRAINT [fk_CA_Competitor_ID] FOREIGN KEY([Competitor_ID])
REFERENCES [dbo].[Competitor] ([Competitor_ID])
GO
ALTER TABLE [dbo].[Competitor_Advertising] CHECK CONSTRAINT [fk_CA_Competitor_ID]
GO
