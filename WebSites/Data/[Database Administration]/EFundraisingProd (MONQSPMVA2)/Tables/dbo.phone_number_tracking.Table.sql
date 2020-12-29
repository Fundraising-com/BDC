USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[phone_number_tracking]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[phone_number_tracking](
	[phone_number_tracking_id] [int] NOT NULL,
	[phone_number_tracking_desc] [varchar](50) NULL,
 CONSTRAINT [PK_phone_number_tracking] PRIMARY KEY CLUSTERED 
(
	[phone_number_tracking_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
