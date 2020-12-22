USE QSPFulfillment
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Create index for faster look up of AppItems
-------------------------------------------------
CREATE INDEX [IX_CM_AppItems1] ON [dbo].[CM_AppItems] ([NoAppItem]) ON [PRIMARY]
GO

-- Create index for faster look up by zip codes
--------------------------------------------------
CREATE INDEX [IX_zip_prefix1] ON [dbo].[zip_prefix]([zip_prefix]) ON [PRIMARY]
GO


-- Create user_role table, indexes and foreign keys
--------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[user_role]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[user_role]
GO

CREATE TABLE [dbo].[user_role] (
	[user_role_id] [int] IDENTITY (1, 1) NOT NULL ,
	[user_id] [int] NOT NULL ,
	[role_id] [int] NOT NULL ,
	[create_date] [datetime] NOT NULL ,
	[create_user_id] [int] NOT NULL ,
	[update_date] [datetime] NOT NULL ,
	[update_user_id] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[user_role] WITH NOCHECK ADD 
	CONSTRAINT [PK_user_role] PRIMARY KEY  CLUSTERED 
	(
		[user_role_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[user_role] ADD 
	CONSTRAINT [DF_user_role_create_date] DEFAULT (getdate()) FOR [create_date],
	CONSTRAINT [DF_user_role_update_date] DEFAULT (getdate()) FOR [update_date]
GO

 CREATE  UNIQUE  INDEX [IX_user_role1] ON [dbo].[user_role]([user_id], [role_id]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[user_role] ADD 
	CONSTRAINT [FK_user_role_CM_Roles] FOREIGN KEY 
	(
		[role_id]
	) REFERENCES [dbo].[CM_Roles] (
		[Role_ID]
	),
	CONSTRAINT [FK_user_role_user] FOREIGN KEY 
	(
		[user_id]
	) REFERENCES [dbo].[user] (
		[user_id]
	)
GO


-- Create form_permission table, indexes and foreign keys
--------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[form_permission]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[form_permission]
GO

CREATE TABLE [dbo].[form_permission] (
	[form_permission_id] [int] IDENTITY (1, 1) NOT NULL ,
	[form_id] [int] NULL ,
	[role_id] [int] NULL ,
	[allow_read] [bit] NOT NULL ,
	[allow_write] [bit] NOT NULL ,
	[create_date] [datetime] NOT NULL ,
	[create_user_id] [int] NOT NULL ,
	[update_date] [datetime] NOT NULL ,
	[update_user_id] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[form_permission] WITH NOCHECK ADD 
	CONSTRAINT [PK_form_permission] PRIMARY KEY  CLUSTERED 
	(
		[form_permission_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[form_permission] ADD 
	CONSTRAINT [DF_form_permission_allow_read] DEFAULT (1) FOR [allow_read],
	CONSTRAINT [DF_form_permission_allow_write] DEFAULT (1) FOR [allow_write],
	CONSTRAINT [DF_form_permission_create_date] DEFAULT (getdate()) FOR [create_date],
	CONSTRAINT [DF_form_permission_update_date] DEFAULT (getdate()) FOR [update_date]
GO

 CREATE  UNIQUE  INDEX [IX_form_permission1] ON [dbo].[form_permission]([role_id], [form_id]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[form_permission] ADD 
	CONSTRAINT [FK_form_permission_CM_Roles] FOREIGN KEY 
	(
		[role_id]
	) REFERENCES [dbo].[CM_Roles] (
		[Role_ID]
	),
	CONSTRAINT [FK_form_permission_form] FOREIGN KEY 
	(
		[form_id]
	) REFERENCES [dbo].[form] (
		[form_id]
	)
GO


-- Create form_permission_region table, indexes and foreign keys
--------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[form_permission_region]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[form_permission_region]
GO

CREATE TABLE [dbo].[form_permission_region] (
	[form_permission_region_id] [int] IDENTITY (1, 1) NOT NULL ,
	[form_id] [int] NULL ,
	[zip] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[description] [varchar] (400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[allow_read] [bit] NOT NULL ,
	[allow_write] [bit] NOT NULL ,
	[create_date] [datetime] NOT NULL ,
	[create_user_id] [int] NOT NULL ,
	[update_date] [datetime] NOT NULL ,
	[update_user_id] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[form_permission_region] WITH NOCHECK ADD 
	CONSTRAINT [PK_form_permission_region] PRIMARY KEY  CLUSTERED 
	(
		[form_permission_region_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[form_permission_region] ADD 
	CONSTRAINT [DF_form_permission_region_allow_read] DEFAULT (1) FOR [allow_read],
	CONSTRAINT [DF_form_permission_region_allow_write] DEFAULT (1) FOR [allow_write],
	CONSTRAINT [DF_form_permission_region_CreateDate] DEFAULT (getdate()) FOR [create_date],
	CONSTRAINT [DF_form_permission_region_UpdateDate] DEFAULT (getdate()) FOR [update_date]
GO

 CREATE  UNIQUE  INDEX [IX_form_permission_region1] ON [dbo].[form_permission_region]([zip], [form_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_form_permission2] ON [dbo].[form_permission_region]([form_id]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[form_permission_region] ADD 
	CONSTRAINT [FK_form_permission_region_form] FOREIGN KEY 
	(
		[form_id]
	) REFERENCES [dbo].[form] (
		[form_id]
	)
GO


-- Load initial users & roles into user_role
--------------------------------
INSERT INTO user_role
(user_id, role_id, create_user_id, update_user_id)
SELECT user_id, role_id, 101563, 101563 
FROM [user] WHERE role_id IS NOT NULL

GO

-- Disallow FM role to access PE forms
-----------------------------
INSERT INTO form_permission
(form_id, role_id, allow_read, allow_write, create_user_id, update_user_id)
SELECT form_id, 1, 0, 0, 101563, 101563 FROM form where description = 'PE FY09' AND deleted = 0


-- Load all restricted states into form_permission_region
-----------------------------
DECLARE @PineValleyFormID INT, @OtisFormID INT
SET @PineValleyFormID = 61	-- Production database value
SET @OtisFormID = 63		-- Production database value

TRUNCATE TABLE form_permission_region

INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '460', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '461', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '462', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '463', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '464', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '465', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '466', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '467', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '468', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '469', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '470', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '471', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '472', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '473', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '474', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '475', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '476', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '477', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '478', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '479', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '010', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '011', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '012', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '013', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '014', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '015', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '016', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '017', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '018', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '019', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '020', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '021', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '022', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '023', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '024', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '025', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '026', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '027', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '550', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '551', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '552', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '553', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '554', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '555', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '556', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '557', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '558', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '559', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '560', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '561', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '562', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '563', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '564', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '565', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '566', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '567', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '590', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '591', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '592', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '593', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '594', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '595', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '596', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '597', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '598', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '599', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '570', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '571', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '572', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '573', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '574', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '575', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '576', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '577', 'SD', 0, 0, 101563, 101563)


-- Load all restricted ID state except Lemhi, Clearwater, Comas, Lincoln into form_permission_region
--------------------------------------------------------
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8320', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8321', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8322', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8323', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8324', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83250', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83251', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83252', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83254', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83255', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83256', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83257', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83258', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83259', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8326', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8327', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8328', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8329', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8330', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8331', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83320', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83321', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83323', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83325', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83326', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83328', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8333', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83340', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83341', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83342', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83343', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83344', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83345', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83346', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83347', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83348', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83350', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83351', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83353', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83354', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83355', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83356', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83357', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83358', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83359', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83360', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83361', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83362', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83365', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83368', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8337', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8338', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8339', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8340', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8341', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8342', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8343', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8344', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8345', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83460', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83461', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83463', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83464', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83465', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83466', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83467', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83468', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83469', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8347', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8348', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8349', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8350', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8351', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83521', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83522', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83523', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83524', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83525', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83526', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83527', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83528', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83529', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8353', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83540', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83542', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83543', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83545', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83547', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83548', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83549', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83550', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83551', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83552', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83554', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83555', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83556', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83557', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83558', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '83559', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8356', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8357', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8358', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8359', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '836', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '837', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '838', 'ID', 0, 0, 101563, 101563)



-- Load all restricted ME state except Arostock, Piscataquis, Penobscott, Washington, Hancock into form_permission_region
--------------------------------------------------------
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '039', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '040', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '041', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '042', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '043', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04400', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04402', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04403', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04404', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04405', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04407', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04408', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04409', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04413', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04415', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04416', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04420', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04421', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04423', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04424', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04425', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04431', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04432', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04433', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04436', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04437', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04438', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04439', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04440', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04445', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04446', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04447', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04452', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04454', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04458', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04465', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04466', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04467', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04469', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04470', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04472', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04477', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04478', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04480', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04481', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04482', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04483', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04484', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04485', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04486', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04489', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04490', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04491', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04492', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04494', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04496', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04498', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04499', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '045', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '046', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0470', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0471', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0472', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04731', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04734', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04738', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04741', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04744', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04748', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04749', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04751', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04752', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04753', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04754', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04755', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04759', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04761', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04767', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04768', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04770', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04771', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04775', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04778', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04782', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04784', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04788', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04789', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0479', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '048', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0490', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0491', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0492', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04931', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04933', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04934', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04935', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04936', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04937', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04938', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0494', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04950', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04951', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04952', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04954', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04955', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04956', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04957', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04958', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04959', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04960', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04961', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04962', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04963', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04964', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04965', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04966', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04967', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '04968', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0497', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0498', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '0499', 'ME', 0, 0, 101563, 101563)





-- Load all restricted ND state except Adams, Bowman, Golden Valley, Hettinger, Sioux, Slope into form_permission_region
--------------------------------------------------------
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '580', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '581', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '582', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '583', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '584', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5850', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5851', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5852', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5853', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5854', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5855', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58560', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58561', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58562', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58563', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58564', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58565', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58566', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58567', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58569', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58571', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58572', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58573', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58574', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58575', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58576', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58577', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58578', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58579', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5858', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5859', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5860', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5861', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58622', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58624', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58625', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58626', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58627', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58628', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58629', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58630', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58631', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58633', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58634', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58635', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58636', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58637', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58638', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58640', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58641', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58642', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58644', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58645', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58648', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58652', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58655', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58656', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58657', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58658', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '58659', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5866', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5867', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5868', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '5869', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '587', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '588', 'ND', 0, 0, 101563, 101563)



-- Load all restricted WY state except Converse, Crook, Weston, Niobrara, Johnson into form_permission_region
--------------------------------------------------------
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '820', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '821', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8220', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8221', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8223', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8225', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8226', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8227', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8228', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8229', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '823', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '824', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '825', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8260', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8261', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8262', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8264', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8265', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8266', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8267', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8268', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8269', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8274', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8275', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8276', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8277', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8278', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '8279', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '828', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '829', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '830', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@PineValleyFormID, '831', 'WY', 0, 0, 101563, 101563)



-- Load all restricted states into form_permission_region
-----------------------------
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '460', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '461', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '462', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '463', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '464', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '465', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '466', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '467', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '468', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '469', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '470', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '471', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '472', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '473', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '474', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '475', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '476', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '477', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '478', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '479', 'IN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '010', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '011', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '012', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '013', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '014', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '015', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '016', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '017', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '018', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '019', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '020', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '021', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '022', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '023', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '024', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '025', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '026', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '027', 'MA', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '550', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '551', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '552', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '553', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '554', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '555', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '556', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '557', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '558', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '559', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '560', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '561', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '562', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '563', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '564', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '565', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '566', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '567', 'MN', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '590', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '591', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '592', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '593', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '594', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '595', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '596', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '597', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '598', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '599', 'MT', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '570', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '571', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '572', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '573', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '574', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '575', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '576', 'SD', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '577', 'SD', 0, 0, 101563, 101563)


-- Load all restricted ID state except Lemhi, Clearwater, Comas, Lincoln into form_permission_region
--------------------------------------------------------
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8320', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8321', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8322', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8323', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8324', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83250', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83251', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83252', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83254', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83255', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83256', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83257', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83258', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83259', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8326', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8327', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8328', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8329', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8330', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8331', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83320', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83321', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83323', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83325', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83326', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83328', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8333', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83340', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83341', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83342', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83343', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83344', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83345', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83346', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83347', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83348', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83350', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83351', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83353', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83354', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83355', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83356', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83357', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83358', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83359', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83360', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83361', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83362', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83365', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83368', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8337', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8338', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8339', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8340', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8341', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8342', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8343', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8344', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8345', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83460', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83461', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83463', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83464', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83465', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83466', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83467', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83468', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83469', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8347', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8348', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8349', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8350', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8351', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83521', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83522', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83523', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83524', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83525', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83526', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83527', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83528', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83529', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8353', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83540', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83542', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83543', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83545', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83547', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83548', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83549', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83550', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83551', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83552', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83554', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83555', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83556', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83557', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83558', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '83559', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8356', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8357', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8358', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8359', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '836', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '837', 'ID', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '838', 'ID', 0, 0, 101563, 101563)



-- Load all restricted ME state except Arostock, Piscataquis, Penobscott, Washington, Hancock into form_permission_region
--------------------------------------------------------
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '039', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '040', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '041', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '042', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '043', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04400', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04402', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04403', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04404', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04405', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04407', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04408', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04409', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04413', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04415', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04416', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04420', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04421', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04423', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04424', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04425', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04431', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04432', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04433', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04436', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04437', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04438', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04439', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04440', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04445', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04446', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04447', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04452', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04454', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04458', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04465', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04466', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04467', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04469', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04470', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04472', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04477', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04478', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04480', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04481', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04482', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04483', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04484', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04485', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04486', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04489', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04490', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04491', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04492', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04494', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04496', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04498', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04499', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '045', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '046', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0470', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0471', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0472', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04731', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04734', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04738', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04741', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04744', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04748', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04749', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04751', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04752', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04753', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04754', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04755', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04759', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04761', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04767', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04768', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04770', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04771', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04775', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04778', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04782', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04784', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04788', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04789', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0479', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '048', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0490', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0491', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0492', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04931', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04933', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04934', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04935', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04936', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04937', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04938', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0494', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04950', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04951', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04952', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04954', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04955', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04956', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04957', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04958', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04959', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04960', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04961', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04962', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04963', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04964', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04965', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04966', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04967', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '04968', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0497', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0498', 'ME', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '0499', 'ME', 0, 0, 101563, 101563)





-- Load all restricted ND state except Adams, Bowman, Golden Valley, Hettinger, Sioux, Slope into form_permission_region
--------------------------------------------------------
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '580', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '581', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '582', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '583', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '584', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5850', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5851', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5852', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5853', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5854', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5855', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58560', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58561', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58562', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58563', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58564', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58565', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58566', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58567', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58569', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58571', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58572', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58573', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58574', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58575', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58576', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58577', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58578', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58579', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5858', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5859', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5860', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5861', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58622', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58624', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58625', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58626', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58627', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58628', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58629', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58630', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58631', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58633', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58634', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58635', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58636', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58637', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58638', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58640', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58641', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58642', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58644', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58645', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58648', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58652', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58655', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58656', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58657', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58658', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '58659', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5866', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5867', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5868', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '5869', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '587', 'ND', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '588', 'ND', 0, 0, 101563, 101563)



-- Load all restricted WY state except Converse, Crook, Weston, Niobrara, Johnson into form_permission_region
--------------------------------------------------------
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '820', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '821', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8220', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8221', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8223', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8225', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8226', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8227', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8228', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8229', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '823', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '824', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '825', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8260', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8261', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8262', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8264', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8265', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8266', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8267', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8268', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8269', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8274', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8275', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8276', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8277', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8278', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '8279', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '828', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '829', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '830', 'WY', 0, 0, 101563, 101563)
INSERT INTO form_permission_region (form_id, zip, description, allow_read, allow_write, create_user_id, update_user_id) VALUES (@OtisFormID, '831', 'WY', 0, 0, 101563, 101563)

 




 