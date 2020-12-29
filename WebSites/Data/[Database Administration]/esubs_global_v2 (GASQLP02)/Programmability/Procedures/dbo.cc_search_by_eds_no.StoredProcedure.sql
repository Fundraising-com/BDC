USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_search_by_eds_no]    Script Date: 02/14/2014 13:04:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--dbo.cc_search_by_member_name 'mel'

CREATE   PROCEDURE [dbo].[cc_search_by_eds_no]

      @valeur int

AS

BEGIN

 

 

Select 

         g.group_name

        ,e.active

        ,e.event_name

      ,e.event_id

      ,m.first_name + ' ' + m.last_name as [name]

      

From

      [group] g

        inner join event_group eg

           on g.group_id = eg.group_id

      inner join event e

         on e.event_id = eg.event_id

    inner join event_participation ep

         on ep.event_id = e.event_id

    

      inner join member_hierarchy mh

         on mh.member_hierarchy_id = ep.member_hierarchy_id and mh.active = 1

      inner join member m

         on m.member_id = mh.member_id

    inner join payment_info pi

           on g.group_id = pi.group_id

    inner join payment p

           on pi.payment_info_id = p.payment_info_id

    inner join QSPECommerce.dbo.EfundraisingTransaction et

           on ep.event_participation_id = et.suppID

     inner JOIN QSPECommerce.dbo.Cart AS cart

        ON et.orderid = cart.x_order_id 

     

where cart.eds_order_id = @valeur

 

group by

         g.group_name

        ,e.active

        ,e.event_name

      ,e.event_id

        ,m.first_name + ' ' + m.last_name

 

 

end
GO
