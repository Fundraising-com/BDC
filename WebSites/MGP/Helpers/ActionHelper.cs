using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
namespace GA.BDC.Web.MGP.Helpers
{
    /// <summary>
    /// Admins the Participant actions
    /// </summary>
    public static class ActionHelper
    {
        

        /// <summary>
        /// Creates an action for the event participant received
        /// </summary>
        /// <param name="actionEnum">Action to be created</param>
        /// <param name="eventParticipationId">Event Participation Id</param>
        public static void CreateAction(ActionEnum actionEnum, int eventParticipationId)
        {
            var thread = new Thread(() => Send(actionEnum, eventParticipationId));
            thread.Start();
        }
        /// <summary>
        /// Saves the action to the DB async
        /// </summary>
        /// <param name="actionEnum"></param>
        /// <param name="eventParticipationId"></param>
        private static void Send(ActionEnum actionEnum, int eventParticipationId)
        {
         using (var dataProvider = new DataProvider())
         {
            var action = new event_participation_action { event_participation_id = eventParticipationId, action = (int)actionEnum, create_date = DateTime.Now };
            dataProvider.event_participation_actions.Add(action);
            dataProvider.SaveChanges();
         }
      }
        /// <summary>
        /// Returns the actions associated to the participant received
        /// </summary>
        /// <param name="eventParticipationId">Event Participation Id</param>
        /// <returns>Actions found</returns>
        public static IEnumerable<event_participation_action> FindActions(int eventParticipationId)
        {
            //var result = new List<event_participation_action>();
            using (var dataProvider = new DataProvider())
            {
                return (from n in dataProvider.event_participation_actions
                        where n.event_participation_id == eventParticipationId
                        select n).ToList();
            }
            //return result;
        }
    }
    /// <summary>
    /// Allowed actions
    /// </summary>
    public enum ActionEnum
    {
        /// <summary>
        /// Default, no action
        /// </summary>
        NoAction = 0,
        /// <summary>
        /// Participant Imported Contacts
        /// </summary>
        ImportedContacts = 1
    }
}