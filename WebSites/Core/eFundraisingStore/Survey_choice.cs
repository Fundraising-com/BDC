using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Core.eFundraisingStore
{
    public class Survey_choice : eFundraisingStoreDataObject
    {

        private int surveyId;
        private int choiceId;

        public Survey_choice() : this(int.MinValue) { }
        public Survey_choice(int surveyId) : this(surveyId, int.MinValue) { }
        public Survey_choice(int surveyId, int choiceId)
        {
            this.surveyId = surveyId;
            this.choiceId = choiceId;
        }

        #region Properties
        public int SurveyId
        {
            set { surveyId = value; }
            get { return surveyId; }
        }

        public int ChoiceId
        {
            set { choiceId = value; }
            get { return choiceId; }
        }
        #endregion

        #region Data Source Methods
        
        public static Survey_choice[] GetSurveyChoices()
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetSurveyChoices();
        }

        public static SurveyChoiceCollection GetSurveyChoicesBySurveyID(int id)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetSurveyChoicesBySurveyID(id);
        }



        #endregion
                
    }
}
