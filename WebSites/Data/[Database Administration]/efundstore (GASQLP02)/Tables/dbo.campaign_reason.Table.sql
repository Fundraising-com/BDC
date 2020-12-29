USE [eFundstore]
GO
/****** Object:  Table [dbo].[campaign_reason]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[campaign_reason](
	[campaign_reason_id] [tinyint] NOT NULL,
	[party_type_id] [tinyint] NOT NULL,
	[description] [varchar](50) NULL,
 CONSTRAINT [PK_campaign_reason] PRIMARY KEY CLUSTERED 
(
	[campaign_reason_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
