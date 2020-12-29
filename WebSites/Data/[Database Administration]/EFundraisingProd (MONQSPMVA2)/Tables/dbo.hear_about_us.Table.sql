USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[hear_about_us]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[hear_about_us](
	[hear_id] [tinyint] NOT NULL,
	[party_type_id] [tinyint] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[order_on_web] [tinyint] NOT NULL,
	[is_active] [bit] NOT NULL,
 CONSTRAINT [PK_hear_about_us] PRIMARY KEY CLUSTERED 
(
	[hear_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[hear_about_us]  WITH CHECK ADD  CONSTRAINT [FK_hear_about_us_party_type] FOREIGN KEY([party_type_id])
REFERENCES [dbo].[party_type] ([party_type_id])
GO
ALTER TABLE [dbo].[hear_about_us] CHECK CONSTRAINT [FK_hear_about_us_party_type]
GO
ALTER TABLE [dbo].[hear_about_us] ADD  CONSTRAINT [DF_hear_about_us_is_active]  DEFAULT (1) FOR [is_active]
GO
