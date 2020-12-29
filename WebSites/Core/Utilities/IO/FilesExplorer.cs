using System;
using System.IO;
using System.Collections;

namespace GA.BDC.Core.Utilities.IO {

	/// <summary>
	/// 
	/// </summary>
	public sealed class FilesExplorer {
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pObjectType"></param>
		/// <param name="pAction"></param>
		/// <param name="pParams"></param>
		public static void PerformAction(ObjectType pObjectType, ActionType pAction, params object[] pParams) {
			switch(pAction) {
				case ActionType.AddNew:
					if(pParams.Length <= 0 || pParams == null)
						throw new NullReferenceException("The parameter pParams (params object[]) should contains a valid parameters");
					else if(pParams.Length != 2)
						throw new IndexOutOfRangeException("The parameters pParams (params object[] should contains a two value");
					try {
						AddNew(pObjectType, (string)pParams[0], (string)pParams[1]);
					} catch(Exception ex) {
						throw new Exception("An exception have been throw in PerformAction method of efundraising.Utilities.IO.FilesExplorer",ex);
					}
					break;
				case ActionType.Delete:
					if(pParams.Length <= 0 || pParams == null)
						throw new NullReferenceException("The parameter pParams (params object[]) should contains a valid parameters");
					else if(pParams.Length != 1)
						throw new IndexOutOfRangeException("The parameters pParams (params object[] should contains a single value");
					try {
						DeleteObject(pObjectType, (string)pParams[0]);
					} catch(Exception ex) {
						throw new Exception("An exception have been throw in PerformAction method of efundraising.Utilities.IO.FilesExplorer");
					}
					break;
				case ActionType.MoveTo:
					if(pParams.Length <= 0 || pParams == null)
						throw new NullReferenceException("The parameter pParams (params object[]) should contains a valid parameters");
					else if(pParams.Length != 3)
						throw new IndexOutOfRangeException("The parameters pParams (params object[] should contains a three values");
					try {
						MoveTo(pObjectType, (string)pParams[0], (string)pParams[1],(bool)pParams[2]);
					} catch(Exception ex) {
						throw new Exception("An exception have been throw in PerformAction method of efundraising.Utilities.IO.FilesExplorer");
					}
					break;
				case ActionType.Replace:
					if(pParams.Length <= 0 || pParams == null)
						throw new NullReferenceException("The parameter pParams (params object[]) should contains a valid parameters");
					else if(pParams.Length != 2)
						throw new IndexOutOfRangeException("The parameters pParams (params object[] should contains a two values");
					try {
						ReplaceObject(pObjectType, (string)pParams[0], (string)pParams[1]);
					} catch(Exception ex) {
						throw new Exception("An exception have been throw in PerformAction method of efundraising.Utilities.IO.FilesExplorer");
					}
					break;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pObjectType"></param>
		/// <param name="pSourceOfObject"></param>
		/// <param name="pAddToPath"></param>
		private static void AddNew(ObjectType pObjectType, string pSourceOfObject, string pAddToPath) {
			if(CheckIfExist(pObjectType, pSourceOfObject) && CheckIfExist(pObjectType, pAddToPath))
				MoveTo(pObjectType, pSourceOfObject, pAddToPath, false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pObjectType"></param>
		/// <param name="pOldObjectPath"></param>
		/// <param name="pNewSourceObject"></param>
        private static void ReplaceObject(ObjectType pObjectType, string pOldObjectPath, string pNewSourceObject) {
			if(CheckIfExist(pObjectType, pOldObjectPath) && CheckIfExist(pObjectType, pNewSourceObject)) {
				switch(pObjectType) {
					case ObjectType.File:
						System.IO.File.Copy(pNewSourceObject, pOldObjectPath, true);
						break;
					case ObjectType.Folder:
						System.IO.DirectoryInfo oDirSource = new DirectoryInfo(pNewSourceObject);
						oDirSource.MoveTo(pOldObjectPath);
						break;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pObjectType"></param>
		/// <param name="pObjectPathToRemove"></param>
		private static void DeleteObject(ObjectType pObjectType, string pObjectPathToRemove) {
			if(CheckIfExist(pObjectType, pObjectPathToRemove)) {
				switch(pObjectType) {
					case ObjectType.File:
						System.IO.File.Delete(pObjectPathToRemove);
						break;
					case ObjectType.Folder:
						System.IO.Directory.Delete(pObjectPathToRemove);
						break;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pObjectType"></param>
		/// <param name="pPathToMove"></param>
		/// <param name="pMoveObjectTo"></param>
		/// <param name="pDeleteAfter"></param>
		private static void MoveTo(ObjectType pObjectType, string pPathToMove, string pMoveObjectTo, bool pDeleteAfter) {
			if(CheckIfExist(pObjectType,pPathToMove) && CheckIfExist(pObjectType,pMoveObjectTo)) {
				switch(pObjectType) {
					case ObjectType.File:
						System.IO.File.Move(pPathToMove,pMoveObjectTo);
						break;
					case ObjectType.Folder:
						System.IO.Directory.Move(pPathToMove,pMoveObjectTo);
						break;
				}
				if(pDeleteAfter) {
					DeleteObject(pObjectType, pPathToMove);
				}
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pObjectType"></param>
		/// <param name="pPathToCheck"></param>
		/// <returns></returns>
		private static bool CheckIfExist(ObjectType pObjectType, string pPathToCheck) {
			bool oExist = false;
			switch(pObjectType) {
				case ObjectType.File:
					if(!System.IO.File.Exists(pPathToCheck))
						throw new System.IO.FileNotFoundException("The file: " + pPathToCheck + " doesn't exist", pPathToCheck);
					else
						oExist = true;
					break;
				case ObjectType.Folder:
					if(!System.IO.Directory.Exists(pPathToCheck))
						throw new System.IO.DirectoryNotFoundException("The Directory" + pPathToCheck + " doesn't exist");
					else
						oExist = true;
					break;
			}
			return oExist;
		}

		private void GetFilesFromDirectory(ref ArrayList files, string directory) {
			string[] filesInDir = System.IO.Directory.GetFiles(directory);
			foreach(string file in filesInDir) {
				files.Add(file);
			}
			string[] directories = System.IO.Directory.GetDirectories(directory);
			foreach(string dir in directories) {
				GetFilesFromDirectory(ref files, dir);
			}
		}

		public string[] GetFilesFromDirectory(string directory) {
			ArrayList files = new ArrayList();
			GetFilesFromDirectory(ref files, directory);
			string[] rfiles = new string[files.Count];
			for(int i=0;i<files.Count;i++) {
				rfiles[i] = (string)files[i];
			}
			return rfiles;
		}

	}
}
