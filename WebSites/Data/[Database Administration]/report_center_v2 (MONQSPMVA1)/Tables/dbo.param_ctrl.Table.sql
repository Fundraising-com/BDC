USE [report_center_v2]
GO
/****** Object:  Table [dbo].[param_ctrl]    Script Date: 02/14/2014 16:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[param_ctrl](
	[param_ctrl_id] [tinyint] IDENTITY(1,1) NOT NULL,
	[param_ctrl_name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_param_ctrl] PRIMARY KEY CLUSTERED 
(
	[param_ctrl_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
