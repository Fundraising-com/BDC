USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[associate_mentor_commission]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[associate_mentor_commission](
	[associate_id] [int] NOT NULL,
	[mentor_id] [int] NOT NULL,
	[product_class_id] [tinyint] NOT NULL,
	[commission_rate] [float] NULL,
	[comments] [varchar](255) NULL,
 CONSTRAINT [PK_associate_mentor_commission] PRIMARY KEY CLUSTERED 
(
	[associate_id] ASC,
	[mentor_id] ASC,
	[product_class_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[associate_mentor_commission]  WITH CHECK ADD  CONSTRAINT [FK_associate_mentor_commission_associate_mentor] FOREIGN KEY([associate_id], [mentor_id])
REFERENCES [dbo].[Associate_Mentor] ([Associate_ID], [Mentor_ID])
GO
ALTER TABLE [dbo].[associate_mentor_commission] CHECK CONSTRAINT [FK_associate_mentor_commission_associate_mentor]
GO
