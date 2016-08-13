using UnityEngine;
using System.Collections;
using BlackBox.Tools.IO;
using System;
using System.Collections.Generic;

public class LookPathToJSON : MonoBehaviour {

    public ProjectDirectoryPath _directoryPath;


    public void RecordPath(LookPath path) {

        QuickSerializedAccess.SaveFile<LookPath>(path, _directoryPath, "path_" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),"json");
   }

    public List<LookPath> RecoverAllPaths() {
       return  QuickSerializedAccess.LoadFile<LookPath>(_directoryPath);
    }
}
