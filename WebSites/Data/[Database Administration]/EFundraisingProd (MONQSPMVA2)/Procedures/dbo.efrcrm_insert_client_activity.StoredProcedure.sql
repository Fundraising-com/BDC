USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_client_activity]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Client_activity
CREATE PROCEDURE [dbo].[efrcrm_insert_client_activity] @Client_activity_id int OUTPUT, @Client_id int, @Client_sequence_code char(2), @Client_activity_type_id tinyint, @Client_activity_date datetime, @Completed_date datetime, @Comments text, @Is_contacted bit AS

declare @id int
exec @id = sp_NewID  'Client_Activity_Id', 'All'

begin

insert into Client_activity(Client_Activity_id, Client_id, Client_sequence_code, Client_activity_type_id, Client_activity_date, Completed_date, Comments, Is_contacted) values(@id, @Client_id, @Client_sequence_code, @Client_activity_type_id, @Client_activity_date, @Completed_date, @Comments, @Is_contacted)


end
GO
