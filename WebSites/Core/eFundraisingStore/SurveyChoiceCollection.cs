using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Core.eFundraisingStore
{
    public class SurveyChoiceCollection : eFundraisingStoreCollectionBase
    {
        public SurveyChoiceCollection() 
		{
	
		}
        
		#region public Methods
		public void LoadAllSurveyChoices() 
		{
			List.Clear();

            Survey_choice[] choices = Survey_choice.GetSurveyChoices();
            foreach (Survey_choice surveyChoice in choices) 
			{
				List.Add(surveyChoice);
			}
		}

        #endregion

		#region Comparable Methods
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() 
		{
			// sort the collection
			SortProcess();
		}

		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) 
		{
			SurveyChoiceCollection copy =
                (SurveyChoiceCollection)eFundraisingStoreCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}
		
		#endregion
       
		#region Operators
        public static SurveyChoiceCollection operator +(SurveyChoiceCollection collection1, SurveyChoiceCollection collection2) 
		{
            return (SurveyChoiceCollection)eFundraisingStoreCollectionBase.AddCollection(collection1, collection2);
		}

        public static SurveyChoiceCollection operator +(SurveyChoiceCollection collection, Survey_choice item) 
		{
            return (SurveyChoiceCollection)eFundraisingStoreCollectionBase.AddItem(collection, item);
		}

        public static SurveyChoiceCollection operator -(SurveyChoiceCollection collection1, SurveyChoiceCollection collection2) 
		{
            return (SurveyChoiceCollection)eFundraisingStoreCollectionBase.RemoveCollection(collection1, collection2);
		}

        public static SurveyChoiceCollection operator -(SurveyChoiceCollection collection, Survey_choice item) 
		{
            return (SurveyChoiceCollection)eFundraisingStoreCollectionBase.RemoveItem(collection, item);
		}
		#endregion
    }
}
