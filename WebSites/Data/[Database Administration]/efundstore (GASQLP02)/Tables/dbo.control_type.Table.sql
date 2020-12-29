USE [eFundstore]
GO
/****** Object:  Table [dbo].[control_type]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[control_type](
	[control_type_id] [int] IDENTITY(1,1) NOT NULL,
	[assembly_name] [varchar](200) NOT NULL,
	[namespace] [varchar](200) NOT NULL,
	[class_name] [varchar](100) NOT NULL,
	[display_attribute] [varchar](100) NOT NULL,
	[binding_name] [varchar](100) NULL,
	[event_handler_name] [varchar](100) NOT NULL,
	[auto_post_back] [bit] NOT NULL,
	[datestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_controls_types] PRIMARY KEY CLUSTERED 
(
	[control_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[control_type] ADD  CONSTRAINT [DF_controls_types_datestamp]  DEFAULT (getdate()) FOR [datestamp]
GO
