USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[payment_comment]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment_comment](
	[payment_comment_id] [int] IDENTITY(1,1) NOT NULL,
	[payment_id] [int] NULL,
	[comment] [varchar](4000) NULL,
	[nt_login] [varchar](256) NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_payment_comment] PRIMARY KEY CLUSTERED 
(
	[payment_comment_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[payment_comment]  WITH CHECK ADD  CONSTRAINT [FK_payment_comment_payment] FOREIGN KEY([payment_id])
REFERENCES [dbo].[payment] ([payment_id])
GO
ALTER TABLE [dbo].[payment_comment] CHECK CONSTRAINT [FK_payment_comment_payment]
GO
