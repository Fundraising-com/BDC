USE [EFRCommon]
GO
/****** Object:  Table [dbo].[advertising_type]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[advertising_type](
	[advertising_type_id] [int] NOT NULL,
	[description] [varchar](50) NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_advertising_type] PRIMARY KEY CLUSTERED 
(
	[advertising_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[advertising_type] ADD  CONSTRAINT [DF_advertising_type_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
