/* @Title:  PROJECT FILEGENIUS.
 * 
 * @Desc:   WELCOME TO PROJECT FILEGENIUS, A SOFTWARE PROPOSAL THAT WILL BE CAPABLE
 *          OF HANDLING THE USER'S FILES & DIRECTORIES IN AN INTELLIGENT WAY. THIS
 *          IS THE FIRST EVER VERSION CREATED. FINAL NAME WILL BE DIFERENT.
 * 
 * @Author: LUIS ALBERTO ANTON DELGADILLO (ANTON DELLUA).
 * 
 */

// Namespaces:
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* TODO:    1.EVALUATE THE POSSIBILITY TO WORK WITHOUT A NAMESPACE FOR THE PROJECT.
 *          2.INVESTIGATE THE ACCESS THAT MUST BE GRANTED BEFORE USING CERTAIN DIRECTORIES
 */
 
// Current namespace:
namespace FileGenius_Console
{
    /* @Desc:       This class provides the methods to handle the copying of both directories and files. It
     *              also contains the fields or properties of the conditions needed to do the filtering. This
     *              class in particular differs a little from the rest because the methods are recursive in
     *              some cases since the handling of the copy it's different from both the moving and deleting.
     */
    class FGCopy : FGFilter, FGInterface
    {
        // Fields - All the fields represent the conditions that the user can use to filter the files or directories.
        public List<Struct_NameCond> NameCond { get; set; }
        public List<string> NameCond_Extension { get; set; }
        //public List<Struct_NameCond_Extension> NameCond_Extension { get; set; }
        public List<Struct_DateCond> DateCond { get; set; }
        public List<Struct_LengthCond> LengthCond { get; set; }
        public List<Struct_DateCond_Interval> DateCond_Interval { get; set; }
        public List<Struct_LengthCond_Interval> LengthCond_Interval { get; set; }

        // Constructor:
        public FGCopy()
        {
            NameCond = new List<Struct_NameCond>();
            //NameCond_Extension = new List<Struct_NameCond_Extension>();
            NameCond_Extension = new List<string>();
            DateCond = new List<Struct_DateCond>();
            LengthCond = new List<Struct_LengthCond>();
            DateCond_Interval = new List<Struct_DateCond_Interval>();
            LengthCond_Interval = new List<Struct_LengthCond_Interval>();
        }

        /**
         * @Desc:       This method copies a single file.
         * @Param:      string sourcePath - It provides the path of the file.
         * @Param:      string destPath - It provides the new path of the file.
         */
        public void FileCopy(string sourcePath, string destPath)
        {
            // Copy the file.
            File.Copy(sourcePath, destPath, true);
        }

        /**
         * @Desc:       This method copies multiple files of a given directory.
         * @Param:      string sourcePath - it's the path where the files to copy are.
         * @Param:      string destPath - it's the new path of the files.
         * @Param:      bool recFlag - indicates wheter the conditions are being applied (if there's any) since
         *              it's the first time that it enters the method.
         */
        public void FilesCopy(string sourcePath, string destPath, bool recFlag)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();
            
            if (!recFlag)
            {
                files = SummonFilter(files, sourcePath);
            }

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string sourceFile = Path.Combine(sourcePath, file.Name);
                string destFile = Path.Combine(destPath, file.Name);
                FileCopy(sourceFile, destFile);
            }
        }

        /**
         * @Desc:       This method copies a single directory and then recursively copies everything on it.
         * @Param:      string sourcePath - The path of the directory to copy.
         * @Param:      string destPath - The path where the directory is being copied.
         */
        public void DirectoryCopy(string sourcePath, string destPath)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            string destDir = Path.Combine(destPath, sourceDir.Name);
            CreateDirectory(destDir);
            DirectoriesCopy(sourcePath, destDir, true, true);
        }

        /**
         * @Desc:       This method recursively copies the subdirectories and files of a given directory.
         * @Param:      string sourcePath - The path of the directory where the subdirectories are.
         * @Param:      string destPath - The new path for the subdirectories.
         * @Param:      bool recFlag - indicates whether the conditions are being applied (if there's any) since
         *              it's the first time that it enters the method.
         * @Param:      bool copyFiles - if false, it only copies the directories.
         */
        public void DirectoriesCopy(string sourcePath, string destPath, bool recFlag, bool copyFiles)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dirs = dir.GetDirectories();
            
            if (!recFlag)
            {
                dirs = SummonFilter(dirs, sourcePath);
            }

            // If the source directory does not exist, throw an exception.
            if (!DirectoryExists(sourcePath))
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourcePath);
            }

            // If the destination directory does not exist, create it.
            CreateDirectory(destPath);

            // Get the file contents of the directory to copy.
            if (copyFiles)
            {
                FilesCopy(sourcePath, destPath, recFlag);
            }

            // Copy the subdirectories.
            foreach (DirectoryInfo subdir in dirs)
            {
                // Create the subdirectory.
                string temppath = Path.Combine(destPath, subdir.Name);

                // Copy the subdirectories.
                DirectoriesCopy(subdir.FullName, temppath, true, true);
            }
        }

        // Interface implementation. Descriptions for this methods are given in the interface.
        public void CreateDirectory(string path)
        {
            // Create a new target folder, if necessary.
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public bool DirectoryExists(string path)
        {
            // Check if a directory exists.
            if (Directory.Exists(path))
            {
                return true;
            }
            return false;
        }

        public string[] Filter(string[] a, string sourcePath, bool isFile)
        {
            //List<string> aList = a.ToList();
            //List<string> tempList;
            //string[] temp;
            //string[] temp2;

            if (isFile)
            {
                if (NameCond.Any<Struct_NameCond>())
                {
                    foreach (Struct_NameCond s in NameCond)
                    {
                        a = NameFilter(a, s.s, s.i);
                        /*
                        if (s.OR)
                        {
                            temp2 = NameFilter(a, s.s, s.i);
                            tempList = temp2.ToList();
                            aList = aList.Union(tempList).ToList();
                        }
                        else
                        {
                            temp = aList.ToArray();
                            temp = NameFilter(temp, s.s, s.i);
                            aList = temp.ToList();
                        }*/
                    }
                }
                if (NameCond_Extension.Any<string>())
                {
                    foreach (string s in NameCond_Extension)
                    {
                        a = ExtensionFilter(a, s);
                        /*
                        if (s.OR)
                        {
                            temp2 = ExtensionFilter(a, s.s);
                            tempList = temp2.ToList();
                            aList = aList.Union(tempList).ToList();
                        }
                        else
                        {
                            temp = aList.ToArray();
                            temp = ExtensionFilter(temp, s.s);
                            aList = temp.ToList();
                        }*/
                    }
                }
                if (DateCond.Any<Struct_DateCond>())
                {
                    foreach (Struct_DateCond dt in DateCond)
                    {
                        a = DateFilter(a, dt.dt, dt.i, sourcePath, isFile);
                    }
                }
                if (LengthCond.Any<Struct_LengthCond>())
                {
                    foreach (Struct_LengthCond i in LengthCond)
                    {
                        a = LengthFilter(a, i.l, i.i, sourcePath);
                    }
                }
                if (DateCond_Interval.Any<Struct_DateCond_Interval>())
                {
                    foreach (Struct_DateCond_Interval dt in DateCond_Interval)
                    {
                        a = DateFilter(a, dt.dtA, dt.dtB, dt.i, sourcePath, isFile);
                    }
                }
                if (LengthCond_Interval.Any<Struct_LengthCond_Interval>())
                {
                    foreach (Struct_LengthCond_Interval i in LengthCond_Interval)
                    {
                        a = LengthFilter(a, i.lA, i.lB, i.i, sourcePath);
                    }
                }
            }
            else
            {
                if (NameCond.Any<Struct_NameCond>())
                {
                    foreach (Struct_NameCond s in NameCond)
                    {
                        a = NameFilter(a, s.s, s.i);
                    }
                }
                if (DateCond.Any<Struct_DateCond>())
                {
                    foreach (Struct_DateCond dt in DateCond)
                    {
                        a = DateFilter(a, dt.dt, dt.i, sourcePath, isFile);
                    }
                }
                if (DateCond_Interval.Any<Struct_DateCond_Interval>())
                {
                    foreach (Struct_DateCond_Interval dt in DateCond_Interval)
                    {
                        a = DateFilter(a, dt.dtA, dt.dtB, dt.i, sourcePath, isFile);
                    }
                }
            }
            //a = aList.ToArray();
            return a;
        }

        public DirectoryInfo[] SummonFilter(DirectoryInfo[] di, string sourcePath)
        {
            List<string> tempA = new List<string>();
            foreach (DirectoryInfo a in di)
            {
                tempA.Add(a.Name);
            }
            string[] tempB = tempA.ToArray();
            tempB = Filter(tempB, sourcePath, false);

            List<DirectoryInfo> tempC = new List<DirectoryInfo>();
            foreach (string a in tempB)
            {
                string b = Path.Combine(sourcePath, a);
                DirectoryInfo tempD = new DirectoryInfo(b);
                tempC.Add(tempD);
            }
            DirectoryInfo[] tempE = tempC.ToArray();
            return tempE;
        }

        public FileInfo[] SummonFilter(FileInfo[] fi, string sourcePath)
        {
            List<string> tempA = new List<string>();
            foreach (FileInfo a in fi)
            {
                tempA.Add(a.Name);
            }
            string[] tempB = tempA.ToArray();
            tempB = Filter(tempB, sourcePath, true);

            List<FileInfo> tempC = new List<FileInfo>();
            foreach (string a in tempB)
            {
                string b = Path.Combine(sourcePath, a);
                FileInfo tempD = new FileInfo(b);
                tempC.Add(tempD);
            }
            FileInfo[] tempE = tempC.ToArray();
            return tempE;
        }
    }
}
