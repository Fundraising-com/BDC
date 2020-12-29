USE [EFRCommon]
GO
/****** Object:  Table [dbo].[listing]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[listing](
	[listing_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[description] [varchar](50) NULL,
	[listing_amount] [float] NULL,
	[listing_period] [int] NULL,
	[is_visible] [bit] NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_listing] PRIMARY KEY CLUSTERED 
(
	[listing_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[listing] ADD  CONSTRAINT [DF_listing_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
