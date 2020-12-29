USE [EFRCommon]
GO
/****** Object:  Table [dbo].[promotion]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[promotion](
	[promotion_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[promotion_type_code] [char](3) NOT NULL,
	[promotion_destination_id] [int] NULL,
	[promotion_name] [varchar](255) NOT NULL,
	[script_name] [varchar](255) NULL,
	[active] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[cookie_content] [varchar](255) NULL,
	[keyword] [varchar](255) NULL,
	[is_displayable] [bit] NULL,
 CONSTRAINT [PK_promotion] PRIMARY KEY CLUSTERED 
(
	[promotion_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_promotion_destination] FOREIGN KEY([promotion_destination_id])
REFERENCES [dbo].[promotion_destination] ([promotion_destination_id])
GO
ALTER TABLE [dbo].[promotion] CHECK CONSTRAINT [FK_promotion_promotion_destination]
GO
ALTER TABLE [dbo].[promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_promotion_type] FOREIGN KEY([promotion_type_code])
REFERENCES [dbo].[promotion_type] ([promotion_type_code])
GO
ALTER TABLE [dbo].[promotion] CHECK CONSTRAINT [FK_promotion_promotion_type]
GO
