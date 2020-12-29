USE [eFundweb]
GO
/****** Object:  Table [dbo].[promotion_default]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[promotion_default](
	[promotion_id] [int] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[datestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_promotion_default] PRIMARY KEY CLUSTERED 
(
	[promotion_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[promotion_default] ADD  CONSTRAINT [DF_promotion_default_datestamp]  DEFAULT (getdate()) FOR [datestamp]
GO
