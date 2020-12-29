USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_onetime_update_group_type]    Script Date: 02/14/2014 13:03:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efr_onetime_update_group_type]
AS
BEGIN
	
	SET NOCOUNT ON;

    --28	3	Badminton
    print '28	3	Badminton'
    update lead set group_type_id = 28 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Badminton%'
    )

    --1	4	Band
    print '1	4	Band'
    update lead set group_type_id = 1 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Band%'
    )
    --2	3	Baseball
    print '2	3	Baseball'
    update lead set group_type_id = 2 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%baseball%'
    )

    --3	3	Basketball
    print '3	3	Basketball'
    update lead set group_type_id = 3 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Basketball%'
    )
    --36	4	Booster
    print '36	4	Booster'
    update lead set group_type_id = 36 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Booster%'
    )
    --29	3	Bowling
    print '29	3	Bowling'
    update lead set group_type_id = 29 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Bowling%'
    )
    --4	3	Cheerleading
    print '4	3	Cheerleading'
    update lead set group_type_id = 4 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Cheerleading%'
    )

    --43	3	Dance
    print '43	3	Dance'
    update lead set group_type_id = 43 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Dance%'
    )

    --45	NULL	Daycare
    print '45	NULL	Daycare'
    update lead set group_type_id = 45 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Daycare%'
    )

    --33	3	Golf
    print '33	3	Golf'
    update lead set group_type_id = 33 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Golf%'
    )

    --7	3	Gymnastics
    print '7	3	Gymnastics'
    update lead set group_type_id = 7 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Gymnastics%'
    )

    --44	3	Handball
    print '44	3	Handball'
    update lead set group_type_id = 44 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Handball%'
    )

    --48	NULL	Headstart
    print '48	NULL	Headstart'
    update lead set group_type_id = 48 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Headstart%'
    )

    --8	3	Hockey
    print '8	3	Hockey'
    update lead set group_type_id = 8 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Hockey%'
    )

    --23	3	Lacrosse
    print '23	3	Lacrosse'
    update lead set group_type_id = 23 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Lacrosse%'
    )

    --53	NULL	Martial Arts
    print '53	NULL	Martial Arts'
    update lead set group_type_id = 53 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Martial Arts%'
    )

    --9	4	Music & Art
    print '9	4	Music & Art'
    update lead set group_type_id = 9 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Music & Art%'
    )

    --14	3	Swimming
    print '14	3	Swimming'
    update lead set group_type_id = 14 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Swimming%'
    )

    --35	3	Tennis
    print '35	3	Tennis'
    update lead set group_type_id = 35 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Tennis%'
    )

    --17	3	Track & Field
    print '17	3	Track & Field'
    update lead set group_type_id = 17 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Track & Field%'
    )

    --15	3	Volleyball
    print '15	3	Volleyball'
    update lead set group_type_id = 15 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Volleyball%'
    )

    --24	3	Wrestling
    print '24	3	Wrestling'
    update lead set group_type_id = 24 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Wrestling%'
    )

    --22	NULL	Youth Support
    print '22	NULL	Youth Support'
    update lead set group_type_id = 22 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Youth Support%'
    )

    --30	3	Crew
    print '30	3	Crew'
    update lead set group_type_id = 30 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Crew%'
    )

    --31	3	Drill Team
    print '31	3	Drill Team'
    update lead set group_type_id = 31 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Drill Team%'
    )

    --32	3	Field Hockey
    print '32	3	Field Hockey'
    update lead set group_type_id = 32 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Field Hockey%'
    )

    --49	NULL	Fireman or Firefighter
    print '49	NULL	Fireman or Firefighter'
    update lead set group_type_id = 49 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Fireman%'
    )

    update lead set group_type_id = 49 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Firefighter%'
    )

    --6	3	Football
    print '6	3	Football'
    update lead set group_type_id = 6 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Football%'
    )

    --34	3	Pom Pon
    print '34	3	Pom Pon'
    update lead set group_type_id = 34 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Pom Pon%'
    )

    --52	NULL	Prison
    print '52	NULL	Prison'
    update lead set group_type_id = 52 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Prison%'
    )

    --26	3	Ringette
    print '26	3	Ringette'
    update lead set group_type_id = 26 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Ringette%'
    )

    --11	4	Scouts
    print '11	4	Scouts'
    update lead set group_type_id = 11 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Scouts%'
    )

    --25	3	Skating
    print '25	3	Skating'
    update lead set group_type_id = 25 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Skating%'
    )

    --12	3	Soccer
    print '12	3	Soccer'
    update lead set group_type_id = 12 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%Soccer%'
    )

    --13	3	Softball
    print '13	3	Softball'
    update lead set group_type_id = 13 where lead_id in 
    (
    select lead_id from lead 
    where group_type_id = 99
    and organization like '%softball%'
    )

END
GO
