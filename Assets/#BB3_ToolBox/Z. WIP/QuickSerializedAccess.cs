using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
//using System;
//using BlackBox.Beans.Basic;
//using BlackBox.Tools.*;

#region \,,/(◣_◢)\,,/
/// Twitter description:
/// No description has been set yet. 
/// Please proceed !
/// 
/// Proud:     ?? % 
/// Clean:     ?? %
/// Reusable:  ?? %
/// Readable:  ?? %
/// Quick Tested: none
/// Stress Tested: none
/// 
/// Improve list: 
/// - finish first version,
/// - finish refactor,
/// - documentation,
/// - test & verify,
/// - plugify.
///
/// Code by: Eloi Strée
/// Code for: Ouat (HakoBioVR) / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Created:  1/26/2016 12:29:26 PM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace BlackBox.Tools.IO { 
	public class QuickSerializedAccess : MonoBehaviour {

        public static void SaveFile<T>(T toSave, ProjectDirectoryPath directoryPath, string filename = "Undefined",string fileFormat="txt")
        {
            if (directoryPath == null) return ;
            ProjectFilePath file = ProjectFilePath.CreateFrom(directoryPath);
            file.SetFileName(filename);
            file.SetFileType(fileFormat);
            string serializedText = JsonConvert.SerializeObject(toSave);
            file.SaveFile(serializedText);
        }
        public static List<T> LoadFile<T>(ProjectDirectoryPath directoryPath )
        {
            List<T> result= new List<T>();
            if (directoryPath == null) return result;
            List<ProjectFilePath> filesFound = directoryPath.GetFilesIn();
            for (int i = 0; i < filesFound.Count; i++)
            {
                try {
                    T obj = JsonConvert.DeserializeObject<T>(filesFound[i].LoadText());
                    if (obj != null)
                        result.Add(obj);
                }
                catch (Exception e) { Debug.LogException(e); }

            }
            return result;
        }
        
    }
}