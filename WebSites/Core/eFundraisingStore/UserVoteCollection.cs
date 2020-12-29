using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Core.eFundraisingStore
{
    public class UserVoteCollection : eFundraisingStoreCollectionBase
    {

        public UserVoteCollection() 
		{
	
		}


        #region public Methods
        public void LoadAllUserVotes()
        {
            List.Clear();

            User_vote[] userVotes = User_vote.GetUserVotes();
            foreach (User_vote vote in userVotes)
            {
                List.Add(vote);
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
            UserVoteCollection copy =
                (UserVoteCollection)eFundraisingStoreCollectionBase.SortWithComparable(this, comparer);
            ReplaceByCollection(copy);
        }

        #endregion

        #region Operators
        public static UserVoteCollection operator +(UserVoteCollection collection1, UserVoteCollection collection2)
        {
            return (UserVoteCollection)eFundraisingStoreCollectionBase.AddCollection(collection1, collection2);
        }

        public static UserVoteCollection operator +(UserVoteCollection collection, User_vote item)
        {
            return (UserVoteCollection)eFundraisingStoreCollectionBase.AddItem(collection, item);
        }

        public static UserVoteCollection operator -(UserVoteCollection collection1, UserVoteCollection collection2)
        {
            return (UserVoteCollection)eFundraisingStoreCollectionBase.RemoveCollection(collection1, collection2);
        }

        public static UserVoteCollection operator -(UserVoteCollection collection, User_vote item)
        {
            return (UserVoteCollection)eFundraisingStoreCollectionBase.RemoveItem(collection, item);
        }
        #endregion

    }
}
