USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[targeted_market_type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[targeted_market_type](
	[targeted_market_type_id] [int] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[decision_maker] [bit] NOT NULL,
	[group_type_id] [tinyint] NOT NULL,
	[comments] [varchar](255) NULL,
 CONSTRAINT [PK_targeted_market_type] PRIMARY KEY CLUSTERED 
(
	[targeted_market_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[targeted_market_type]  WITH NOCHECK ADD  CONSTRAINT [FK_targeted_market_type_group_type] FOREIGN KEY([group_type_id])
REFERENCES [dbo].[group_type] ([group_type_id])
GO
ALTER TABLE [dbo].[targeted_market_type] CHECK CONSTRAINT [FK_targeted_market_type_group_type]
GO
ALTER TABLE [dbo].[targeted_market_type] ADD  CONSTRAINT [DF_Targeted_Market_Type_Decision_Maker]  DEFAULT (0) FOR [decision_maker]
GO
