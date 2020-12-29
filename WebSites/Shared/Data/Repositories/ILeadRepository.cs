using System;
using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface ILeadRepository : IRepository<Lead>
   {
        IList<Lead> GetByEmail(string email);
        
        IList<Lead> GetAllByDateRange(DateTime start, DateTime end);

      /// <summary>
      /// Returns all the Leads that match the Email but is not the Lead ID received
      /// </summary>
      /// <param name="id"></param>
      /// <param name="email"></param>
      /// <returns></returns>
      IList<Lead> GetAllByEmail(int id, string email);

      /// <summary>
      /// Returns all the Leads that match the Day or Evenning Phone but is not the Lead ID received
      /// </summary>
      /// <param name="id"></param>
      /// <param name="phone"></param>
      /// <returns></returns>
      IList<Lead> GetAllByPhone(int id, string phone);
      /// <summary>
      /// Inserts a row in the Leads Consultant Unassigment table to keep a record when a Consultant gets unnasigned from a Lead because is not active anymore
      /// </summary>
      /// <param name="lead"></param>
      void CreateUnassigmentLog(Lead lead);
      /// <summary>
      /// Creates a Lead Activity assigned to the Lead received as a parameter
      /// </summary>
      /// <param name="lead"></param>
      /// <param name="activityType"></param>
      /// <param name="isCompleted">If True, sets the Complete date</param>
      /// <param name="comments"></param>
      void CreateLeadActivity(Lead lead, LeadActivityType activityType, bool isCompleted, string comments);
      /// <summary>
      /// Creates a Leads comments to the assigned Lead
      /// </summary>
      /// <param name="lead"></param>
      /// <param name="comments"></param>
      void CreateLeadComment(Lead lead, string comments);
      /// <summary>
      /// Creates a Lead visit
      /// </summary>
      /// <param name="lead"></param>
      void CreateLeadVisit(Lead lead);
   }
}
