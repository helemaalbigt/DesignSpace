using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
//using System;
using BlackBox.Beans.Basic;
using System;
//using BlackBox.Tools.*;

///     \,,/(◣_◢)\,,/       
/// 
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
/// Code for: Ouat / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Last update:  12/01/2016  )
/// (Version: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	




namespace BlackBox.Tools.IO
{
    /// <summary>
    /// WIP
    /// </summary>
    [System.Serializable]
    public class LocalFile
    {

        public LocalFile()
        {
            _applicationVersion = Beans.Basic.Version.Create();
            _filePath = new ProjectFilePath();
            _linkedProperties = new Beans.Basic.Properties();
        }

		[SerializeField]
        [Tooltip("Version of the application when the file is saved")]
        private Beans.Basic.Version _applicationVersion;
        #region Setter/Getter (applicationVersion)
            /// <summary>
            /// Getter of applicationVersion
            /// Twitter description: Version of the application when the file is saved
            /// </summary>
            public Beans.Basic.Version GetFileApplicationVersion() {
                return _applicationVersion;
            }
            /// <summary>
            /// Setter of applicationVersion
            /// Twitter description: Version of the application when the file is saved
            /// </summary>
            public void SetFileApplicationVersion(Beans.Basic.Version applicationVersion){
                //if (applicationVersion != null) return;
                _applicationVersion = applicationVersion;
            }
        #endregion
		

		[SerializeField]
        [Tooltip("Path of the file in the project with root adapted at the platforme")]
        private ProjectFilePath _filePath;
        #region Setter/Getter (filePath)
            /// <summary>
            /// Getter of filePath
            /// Twitter description: Path of the file in the project with root adapted at the platforme
            /// </summary>
            public ProjectFilePath GetFilePath() {
                return _filePath;
            }
            /// <summary>
            /// Setter of filePath
            /// Twitter description: Path of the file in the project with root adapted at the platforme
            /// </summary>
            public void SetFilePath(ProjectFilePath filePath){
                //if (filePath != null) return;
                _filePath = filePath;
            }
        #endregion
		
        
		[SerializeField]
        [Tooltip("Properties linked to the file.")]
        private Beans.Basic.Properties _linkedProperties;
        #region Setter/Getter (linkedProperties)
            /// <summary>
            /// Getter of linkedProperties
            /// Twitter description: Properties linked to the file.
            /// </summary>
            public Properties GetPropertiesLinkedToTheFile() {
                return _linkedProperties;
            }
            /// <summary>
            /// Setter of linkedProperties
            /// Twitter description: Properties linked to the file.
            /// </summary>
            public void SetPropertiesLinkedToTheFile(Beans.Basic.Properties linkedProperties){
                //if (linkedProperties != null) return;
                _linkedProperties = linkedProperties;
            }

        internal static LocalFile Create(ProjectFilePath fileInfo, Beans.Basic.Version refVersion, Beans.Basic.Properties linkedProperties)
        {
            LocalFile localFile = new LocalFile();
            localFile.SetFileApplicationVersion(refVersion);
            localFile.SetFilePath(fileInfo);
            localFile.SetPropertiesLinkedToTheFile(linkedProperties);
            return localFile;
        }
        #endregion




    }



    [System.Serializable]
    public class ProjectDirectoryPath
    {//: MonoBehaviour {



        public ProjectDirectoryPath()
        {
            Init();
        }
        public ProjectDirectoryPath(string projectName, string directory)
        {
            Init();
            SetSubDirectoryPath(directory);
            SetProjectName(projectName);

        }
        void Init()
        {
            _separator = @"/"; //Path.DirectorySeparatorChar;

        }

        public virtual string GetPath(bool withPlatformRoot)
        {
            return GetDirectoryParts(withPlatformRoot,true,true);
        }
        
        public virtual string GetDirectoryParts(bool withPlatform, bool withProjectName, bool withLocalPath) {
            string result = "";
            string s = GetSeparator();
            if (withLocalPath && string.IsNullOrEmpty( GetSubDirectoryPath()))
                withLocalPath = false;


            //000
            if (!withPlatform && !withProjectName && !withLocalPath)
                result = "";
            //001
            else if (!withPlatform && !withProjectName && withLocalPath)
                result = GetSubDirectoryPath();

            //010
            else if (!withPlatform && withProjectName && !withLocalPath)
                result = GetProjectName();

            //011
            else if (!withPlatform && withProjectName && withLocalPath)
                result = GetProjectName() + s + GetSubDirectoryPath();

            //100
            else if (withPlatform && !withProjectName && !withLocalPath)
                result = AbstractRootPath.GetRootPath();

            //101
            else if (withPlatform && !withProjectName && withLocalPath)
                result = AbstractRootPath.GetRootPath() + s + GetSubDirectoryPath();

            //110
            else if (withPlatform && withProjectName && !withLocalPath) {
                result = AbstractRootPath.GetRootPath() + s+GetProjectName();
            }
            //111
            else if (withPlatform && withProjectName && withLocalPath)
                result = AbstractRootPath.GetRootPath() + s + GetProjectName() + s + GetSubDirectoryPath();

            return result;
        }

        internal void AddSubDirectoryPath(string pathPart)
        {
           pathPart = CheckParamsValidity(pathPart, true);
            SetSubDirectoryPath(GetSubDirectoryPath() + GetSeparator() + pathPart);
        }

        [Header("Allow only: [^A-Za-z0-9_/\\]")]

        private string _separator = @"/";
        #region Setter/Getter (separator)
        /// <summary>
        /// Getter of separator
        /// Twitter description: Do you wanna use / or \\
        /// </summary>
        public string GetSeparator()
        {
            return _separator;
        }
        public enum Separator { Slash, BackSlash }
        /// <summary>
        /// Setter of separator
        /// Twitter description: Do you wanna use / or \\
        /// </summary>
        public void SetSeparator(Separator separator)
        {

            if (separator == Separator.Slash)
                _separator =  "/";
            else
                _separator = "\\";
        }
        #endregion





        [SerializeField]
        [Tooltip("What is the name of the project ?")]
        private string _projectName;
        #region Setter/Getter (projectName)
        /// <summary>
        /// Getter of projectName
        /// Twitter description: What is the name of the project ?
        /// </summary>
        public string GetProjectName()
        {
            return _projectName;
        }
        /// <summary>
        /// Setter of projectName
        /// Twitter description: What is the name of the project ?
        /// </summary>
        public void SetProjectName(string projectName)
        {

            projectName = CheckParamsValidity(projectName, false);
            _projectName = projectName;
        }
        #endregion




        [SerializeField]
        [Tooltip("Directory Path: 'sub1/sub2' , 'sub1', ''  ")]
        private string _directoryPath = "";
        #region Setter/Getter (directoryPath)
        /// <summary>
        /// Getter of directoryPath
        /// Twitter description: Directory Path: 'sub1/sub2' , 'sub1', ''  
        /// </summary>
        public string GetSubDirectoryPath()
        {
            return _directoryPath;
        }
        /// <summary>
        /// Setter of directoryPath
        /// Twitter description: Directory Path: 'sub1/sub2' , 'sub1', ''  
        /// </summary>
        public void SetSubDirectoryPath(string directoryPath)
        {
            directoryPath = CheckParamsValidity(directoryPath, true);
            _directoryPath = directoryPath;
        }
        public string RemoveUnWantedChar(string pathToClean)
        {
            string result = pathToClean;

            string allowCharacter = @"A-Za-z0-9_." + (_separator=="\\" ? "\\\\" : ""+_separator) ;
            result = Regex.Replace(result, @"/\\", "" + _separator);
            result = Regex.Replace(result, allowCharacter,"");
            return result;
        }
        
        public string CheckParamsValidity(string value, bool allowToBeEmpty)
        {
            string result = value;
            if (!allowToBeEmpty && string.IsNullOrEmpty(value))
                throw new System.ArgumentNullException();
            if (result == null)
                result = "";
            RemoveUnWantedChar(result);
            result = result.Trim(new char[] { '/', '\\' });
            return result;
        }
        #endregion

        public virtual bool IsInitiaze(bool refactorDataToBeValideFirst=true)
        {
            if (refactorDataToBeValideFirst)
                CheckValidityOfMemberVariable();
            return !string.IsNullOrEmpty(_projectName);

        }

        private void CheckValidityOfMemberVariable()
        {
           _projectName = CheckParamsValidity(_projectName, false);
            _directoryPath = CheckParamsValidity(_directoryPath, true);
        }

        internal void RemoveAll()
        {
            ProjectPathTools.RemoveAllIn(this);
        }

        internal List<ProjectFilePath> GetFilesIn()
        {
            return ProjectPathTools.GetFilesIn(this);
        }

        internal void CreateFolder()
        {
            string path = GetPath(true);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        
    }


    [System.Serializable]
    public class ProjectFilePath : ProjectDirectoryPath
    {//: MonoBehaviour {


            public ProjectFilePath(): base()
            {
            }
            public ProjectFilePath(string projectName, string localPath, string fileName, string fileType)
             :  base(projectName, localPath)
            {
                SetFileName(fileName);
                SetFileType(fileType);
            }


        public override string GetPath(bool withPlatformRoot)
        {
            return GetFilePathParts(true, true, true, true, true);
        }
        public string GetFilePathParts(bool withRootPart, bool withProject, bool withLocalPath, bool withName, bool withExtensionType)
        {

            string s =""+ GetSeparator();
            string result = "";
            //Debug.Log(string.Format("Come on ! : {0},{1},{2},{3},{4} ", AbstractRootPath.GetRootPath(), GetProjectName(), GetSubDirectoryPath(), GetFileName(), GetFileType()));
            //Debug.Log(string.Format("Come on ! : {0}{5}{1}{5}{2}{5}{3}.{4} ", AbstractRootPath.GetRootPath(), GetProjectName(), GetSubDirectoryPath(), GetFileName(), GetFileType(),GetSeparator()));
            //result = AbstractRootPath.GetRootPath() + s + GetProjectName() + s + GetSubDirectoryPath() + s + GetFileName() + s + GetFileType();
            //Debug.Log("COOOME ONN:"+result);
            //return result;
            if (withLocalPath && string.IsNullOrEmpty( GetSubDirectoryPath()))
                withLocalPath = false;
            if (withName && string.IsNullOrEmpty(GetFileName()))
                withName = false;
            if (withExtensionType && string.IsNullOrEmpty(GetFileType()))
                withExtensionType = false;

            bool hasFolderParts = withRootPart || withProject || withLocalPath;
            bool hasFileParts = withName || withExtensionType;
            
            result += base.GetDirectoryParts(withRootPart, withProject, withLocalPath);
            
            if ( (hasFolderParts && hasFileParts) )
                result += s;


             if (withName && withExtensionType)
            {
                result = result + GetFileName() + "." + GetFileType();
            }
             else if (withName && !withExtensionType)
            {
                result = result + GetFileName();
            }
              else if (!withName && withExtensionType)
            {
                result = result + GetFileType();
            }
              else if (!withName && !withExtensionType)
            {
                result = result + "";
            }

            return result;
        }


        [SerializeField]
            [Tooltip("Your file Name: 'untitled', 'data' ...")]
            private string _fileName="untitled";
            #region Setter/Getter (fileName)
                /// <summary>
                /// Getter of fileName
                /// Twitter description: Your file Name: 'untitled', 'data' ...
                /// </summary>
                public string GetFileName() {
                    return _fileName;
                }
                /// <summary>
                /// Setter of fileName
                /// Twitter description: Your file Name: 'untitled', 'data' ...
                /// </summary>
                public void SetFileName(string fileName){
                    fileName = CheckParamsValidity(fileName, false);
                    _fileName = fileName;
                }
            #endregion
        
            [SerializeField]
            [Tooltip("Your file type: 'txt', 'xml', 'myOpg' ...")]
            private string _fileType="txt";
            #region Setter/Getter (fileType)
            /// <summary>
            /// Getter of fileType
            /// Twitter description: Your file type: 'txt', 'xml', 'myOpg' ...
            /// </summary>
            public string GetFileType()
            {
                return _fileType;
            }
            /// <summary>
            /// Setter of fileType
            /// Twitter description: Your file type: 'txt', 'xml', 'myOpg' ...
            /// </summary>
            public void SetFileType(string fileType)
            {
                fileType = CheckParamsValidity(fileType, false);
                _fileType = fileType;
            }


        public override bool IsInitiaze(bool refactorDataToBeValideFirst = true)
        {
            if(refactorDataToBeValideFirst)
                CheckValidityOfMemberVariable();
            return base.IsInitiaze() && !string.IsNullOrEmpty(_fileName) && !string.IsNullOrEmpty(_fileType);

        }

        private void CheckValidityOfMemberVariable()
        {
            _fileName = CheckParamsValidity(_fileName, false);
            _fileType = CheckParamsValidity(_fileType, false);
        }

        public void SaveFile(string value)
        {
            ProjectPathTools.SaveFileAt(value, this);
        }
        

        public static ProjectFilePath CreateFrom(ProjectDirectoryPath directoryPath)
        {
            return new ProjectFilePath(directoryPath.GetProjectName(), directoryPath.GetSubDirectoryPath(), "undefined", "lol");
        }

        internal string LoadText()
        {
            return ProjectPathTools.LoadFileAt(this);
        }


        internal bool Exist()
        {
            return File.Exists(GetPath(true));
        }
        #endregion




    }


    public class AbstractRootPath
    {
        


        public static void DeleteFolderContents(ProjectDirectoryPath directoryPath)
        {
            string rootPath = directoryPath.GetDirectoryParts(true,true,true);
            Debug.Log("Delete folder: " + rootPath);
            if (Directory.Exists(rootPath))
            {
                Directory.Delete(rootPath, true);
                Directory.CreateDirectory(rootPath);
            }

        }

        public static string GetRootPathWith(ProjectFilePath filePath)
        {
            return filePath.GetPath(true);
        }
   
        public static string GetRootPathWith(ProjectDirectoryPath directoryPath)
        {
            return directoryPath.GetPath(true); 
        }

        public static string GetRootPath( char separator = '/')
        {
            return GetRootPath(GetPlatformType(),separator);
        }
        public static string GetRootPath(PlatformType platform, char separator='/')
        {
 //           char separator = Path.DirectorySeparatorChar;
            switch (platform)
            {
                case PlatformType.Android:
                    return separator + "sdcard";
                case PlatformType.UnityEditor:
                    return Application.dataPath + separator + "..";
                case PlatformType.Windows:
                    return Application.dataPath + separator + "..";

                default: return Application.dataPath;

            }
        }

      


        #region Platform Type
        public enum PlatformType { Android, Windows, Mac, UnityEditor, NotSupported }
        public static PlatformType GetPlatformType()
        {
            PlatformType result = PlatformType.NotSupported;
#if UNITY_EDITOR
            result = PlatformType.UnityEditor;
#endif
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        result= PlatformType.Windows;
#endif
#if UNITY_STANDALONE_OSX && !UNITY_EDITOR
        result= PlatformType.Mac;
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        result= PlatformType.Android;
#endif
            return result;
        }
        #endregion
    }

    public class ProjectPathTools
    {

        public static string SaveFileAt(string textData, ProjectFilePath filePath, bool withOverride=true)
        {


            if (!filePath.IsInitiaze())
            {
                Debug.Log("Params not valide");
                return null;
            }
            string folderDirPath = filePath.GetDirectoryParts(true,true,true);

            if (!Directory.Exists(folderDirPath))
                Directory.CreateDirectory(folderDirPath);
            string fPath = filePath.GetPath(true);
            if( ! File.Exists(fPath) || (File.Exists(fPath) && withOverride))
            File.WriteAllText(fPath, textData);
            return fPath;
        }
        public static string LoadFileAt(ProjectFilePath filePath)
        {

            if (!filePath.IsInitiaze())
            {
                Debug.Log("Params not valide");
                return null;
            }

            string fileDirPath = filePath.GetPath(true);

            if (!File.Exists(fileDirPath)) return null;
            return File.ReadAllText(fileDirPath);
        }

        internal static void RemoveAllIn(ProjectDirectoryPath projectDirectoryPath)
        {
            DeleteDirectoryContents(projectDirectoryPath.GetPath(true));
        }

        internal static void DeleteDirectoryContents(string path)
        {
            Debug.Log("Delete folder: " + path);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }

        }

        internal static List<ProjectFilePath> GetFilesIn(ProjectDirectoryPath directory)
        {
            List<ProjectFilePath> result = new List<ProjectFilePath>();
            string folderPath = directory.GetPath(true);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string[] filesPath =  Directory.GetFiles(folderPath);
            for (int i = 0; i < filesPath.Length; i++)
            {
                string fPath = filesPath[i];
                ProjectFilePath pFile = ProjectFilePath.CreateFrom(directory);
                pFile.SetFileName(Path.GetFileNameWithoutExtension(fPath));
                pFile.SetFileType(Path.GetExtension(fPath).Replace(".",""));
                result.Add(pFile);
            }
            return result;

        }
    }


}


