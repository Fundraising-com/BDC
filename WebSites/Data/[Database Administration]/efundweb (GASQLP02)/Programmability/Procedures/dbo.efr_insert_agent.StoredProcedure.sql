USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_insert_agent]    Script Date: 02/14/2014 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_insert_agent]
@agentID int
,@url varchar(50)
,@company varchar(75)
,@agentName varchar(75)
,@email varchar(50)
,@address varchar(100)
,@city varchar(50)
,@state varchar(2)
,@country varchar (20)
,@zip varchar (10)
,@phone varchar (20) 
,@fax varchar (20)
,@tollFree varchar (20)
,@mobile varchar (20)

AS

INSERT INTO dbo.Agent (url, company, agent_name, add_by_user) VALUES (@url, @company, @agentName 

,'script') 
select @agentID = @@IDENTITY

INSERT INTO dbo.Agent_Address VALUES (@agentID,@address, @city, @state, @country, @zip)

INSERT INTO dbo.Agent_Email VALUES (@email, @agentID)

if len(@phone) >= 10
BEGIN
INSERT INTO dbo.Agent_Reach_Number values (1,@agentID,@phone)
END

if len(@fax) >= 10
BEGIN
INSERT INTO dbo.Agent_Reach_Number values (2,@agentID,@fax)
END

if len(@tollFree) >= 10
BEGIN
INSERT INTO dbo.Agent_Reach_Number values (3,@agentID,@tollFree)
END

if len(@mobile) >= 10
BEGIN
INSERT INTO dbo.Agent_Reach_Number values (5,@agentID,@mobile)
END
GO
