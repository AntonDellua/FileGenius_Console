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
 *          
 */

// Current namespace:
namespace FileGenius_Console
{
    /* @Desc:       This class provides the methods to handle the moving of both directories and files. It
     *              also contains the fields or properties of the conditions needed to do the filtering.
     */
    class FGMove : FGFilter, FGInterface
    {
        // Fields - All the fields represent the conditions that the user can use to filter the files or directories.
        public List<Struct_NameCond> NameCond { get; set; }
        public List<string> NameCond_Extension { get; set; }
        public List<Struct_DateCond> DateCond { get; set; }
        public List<Struct_LengthCond> LengthCond { get; set; }
        public List<Struct_DateCond_Interval> DateCond_Interval { get; set; }
        public List<Struct_LengthCond_Interval> LengthCond_Interval { get; set; }

        // Constructor:
        public FGMove()
        {
            NameCond = new List<Struct_NameCond>();
            NameCond_Extension = new List<string>();
            DateCond = new List<Struct_DateCond>();
            LengthCond = new List<Struct_LengthCond>();
            DateCond_Interval = new List<Struct_DateCond_Interval>();
            LengthCond_Interval = new List<Struct_LengthCond_Interval>();
        }

        /**
         * @Desc:       This method moves a single file.
         * @Param:      string sourcePath - It provides the path of the file.
         * @Param:      string destPath - It's where the file is moving.
         */
        public void FileMove(string sourcePath, string destPath)
        {
            // To move a file or folder to a new location:
            File.Move(sourcePath, destPath);
        }

        /**
         * @Desc:       This method moves a single directory.
         * @Param:      string sourcePath - The path of the directory to move.
         * @Param:      string destPath - It's where the directory is moving.
         */
        public void DirectoryMove(string sourcePath, string destPath)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            string destDir = Path.Combine(destPath, sourceDir.Name);
            // To move an entire directory. To programmatically modify or combine
            // path strings, use the System.IO.Path class.
            Directory.Move(sourcePath, destDir);
        }

        /**
         * @Desc:       This method moves multiple files of a given directory.
         * @Param:      string sourcePath - it's the path where the files to move are.
         * @Param:      string destPath - it's where the files are moving to.
         */
        public void FilesMove(string sourcePath, string destPath)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            FileInfo[] files = dir.GetFiles();

            files = SummonFilter(files, sourcePath);

            foreach (FileInfo file in files)
            {
                string sourceFile = Path.Combine(sourcePath, file.Name);
                string destFile = Path.Combine(destPath, file.Name);
                FileMove(sourceFile, destFile);
            }
            
            //TODO: MOVE FILE (WITH THE USER CONSENT) EVEN IF THE FILE ALREADY EXISTS
        }

        /**
         * @Desc:       This method moves the subdirectories of a given directory.
         * @Param:      string sourcePath - The path of the directory where the subdirectories are.
         * @Param:      string destPath - it's where the directories are moving to.
         */
        public void DirectoriesMove(string sourcePath, string destPath)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dirs = dir.GetDirectories();

            dirs = SummonFilter(dirs, sourcePath);

            foreach (DirectoryInfo subDir in dirs)
            {
                string sourceDir = Path.Combine(sourcePath, subDir.Name);
                DirectoryMove(sourceDir, destPath);
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
            if (isFile)
            {
                if (NameCond.Any<Struct_NameCond>())
                {
                    foreach (Struct_NameCond s in NameCond)
                    {
                        a = NameFilter(a, s.s, s.i);
                    }
                }
                if (NameCond_Extension.Any<string>())
                {
                    foreach (string s in NameCond_Extension)
                    {
                        a = ExtensionFilter(a, s);
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
