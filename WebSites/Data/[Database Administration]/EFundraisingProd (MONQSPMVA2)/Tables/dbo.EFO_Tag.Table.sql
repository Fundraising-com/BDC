USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Tag]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFO_Tag](
	[Email_Type_ID] [int] NOT NULL,
	[Tag_Name] [varchar](50) NOT NULL,
	[Tag_ID] [int] NOT NULL,
 CONSTRAINT [PK_EFO_Tag] PRIMARY KEY NONCLUSTERED 
(
	[Email_Type_ID] ASC,
	[Tag_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[EFO_Tag]  WITH NOCHECK ADD  CONSTRAINT [fk_efot_email_type_id] FOREIGN KEY([Email_Type_ID])
REFERENCES [dbo].[EFO_Email_Type] ([Email_Type_ID])
GO
ALTER TABLE [dbo].[EFO_Tag] CHECK CONSTRAINT [fk_efot_email_type_id]
GO
