using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Core.eFundraisingStore
{
    public class User_vote : eFundraisingStoreDataObject
    {

        private String sessionId;
        private int choiceId;
        private int surveyId;

        public User_vote() : this(null, int.MinValue) { }
        public User_vote(String sessionId) : this(sessionId, int.MinValue) { }
        public User_vote(String sessionId, int choiceId): this(sessionId, choiceId, int.MinValue) { }
        public User_vote(String sessionId, int choiceId, int surveyId)
        {
            this.sessionId = sessionId;
            this.choiceId = choiceId;
            this.surveyId = surveyId;
        }


        #region Properties
        public String SessionId
        {
            set { sessionId = value; }
            get { return sessionId; }
        }

        public int ChoiceId
        {
            set { choiceId = value; }
            get { return choiceId; }
        }

        public int SurveyId
        {
            set { surveyId = value; }
            get { return surveyId; }
        }
        #endregion

        #region Data Source Methods
        public static User_vote[] GetUserVotes()
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetUserVotes();
        }
        
        public static UserVoteCollection GetUserVotesBySessionID(String id)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetUserVotesBySessionID(id);
        }

        public static UserVoteCollection GetUserVotesByChoiceID(int id)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetUserVotesByChoiceID(id);
        }

        public static UserVoteCollection GetUserVotesBySurveyID(int surveyID)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetUserVotesBySurveyID(surveyID);
        }

        public static UserVoteCollection GetUserVotesByChoiceIDAndSurveyID(int choiceID,int surveyID)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetUserVotesByChoiceIDAndSurveyID(choiceID, surveyID);
        }

        public static void InsertUserVote(User_vote vote)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            dbo.InsertUserVote(vote);
        }

        #endregion

    }
}
