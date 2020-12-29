USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Template_Set]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Template_Set](
	[Template_Set_ID] [int] IDENTITY(2,1) NOT FOR REPLICATION NOT NULL,
	[QSP_Program_ID] [int] NOT NULL,
	[Supporter_Path] [varchar](100) NULL,
	[Generic_Path] [varchar](100) NULL,
	[Edit_Path] [varchar](100) NULL,
 CONSTRAINT [PK_Template_Set] PRIMARY KEY NONCLUSTERED 
(
	[Template_Set_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Template_Set]  WITH NOCHECK ADD  CONSTRAINT [fk_ts_qsp_program_id] FOREIGN KEY([QSP_Program_ID])
REFERENCES [dbo].[QSP_Program] ([QSP_Program_ID])
GO
ALTER TABLE [dbo].[Template_Set] CHECK CONSTRAINT [fk_ts_qsp_program_id]
GO
