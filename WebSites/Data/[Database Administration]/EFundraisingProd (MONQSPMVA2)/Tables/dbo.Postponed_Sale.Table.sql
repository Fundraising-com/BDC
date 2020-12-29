USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Postponed_Sale]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Postponed_Sale](
	[Sales_ID] [int] NOT NULL,
	[Postponed_Status_ID] [int] NOT NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_Postponed_Sale] PRIMARY KEY NONCLUSTERED 
(
	[Sales_ID] ASC,
	[Postponed_Status_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Postponed_Sale]  WITH CHECK ADD  CONSTRAINT [fk_ps_postponed_status_ID] FOREIGN KEY([Postponed_Status_ID])
REFERENCES [dbo].[Postponed_Status] ([Postponed_Status_ID])
GO
ALTER TABLE [dbo].[Postponed_Sale] CHECK CONSTRAINT [fk_ps_postponed_status_ID]
GO
