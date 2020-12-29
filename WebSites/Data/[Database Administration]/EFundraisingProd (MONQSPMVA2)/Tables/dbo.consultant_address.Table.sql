USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[consultant_address]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[consultant_address](
	[consultant_address_id] [smallint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[consultant_id] [int] NOT NULL,
	[country_code] [varchar](10) NOT NULL,
	[state_code] [varchar](10) NOT NULL,
	[street_address] [varchar](100) NOT NULL,
	[city] [varchar](25) NOT NULL,
	[zip_code] [varchar](15) NOT NULL,
	[date_inserted] [datetime] NOT NULL,
 CONSTRAINT [PK_consultant_address] PRIMARY KEY CLUSTERED 
(
	[consultant_address_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[consultant_address]  WITH CHECK ADD  CONSTRAINT [FK_consultant_address_consultant] FOREIGN KEY([consultant_id])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[consultant_address] CHECK CONSTRAINT [FK_consultant_address_consultant]
GO
ALTER TABLE [dbo].[consultant_address] ADD  CONSTRAINT [DF_consultant_address_date_inserted]  DEFAULT (getdate()) FOR [date_inserted]
GO
