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
    /* @Desc:       This class provides the methods to handle the deleting of both directories and files. It
     *              also contains the fields or properties of the conditions needed to do the filtering.
     */
    class FGDelete : FGFilter, FGInterface
    {
        // Fields - All the fields represent the conditions that the user can use to filter the files or directories.
        public List<Struct_NameCond> NameCond { get; set; }
        public List<string> NameCond_Extension { get; set; }
        public List<Struct_DateCond> DateCond { get; set; }
        public List<Struct_LengthCond> LengthCond { get; set; }
        public List<Struct_DateCond_Interval> DateCond_Interval { get; set; }
        public List<Struct_LengthCond_Interval> LengthCond_Interval { get; set; }

        // Constructor:
        public FGDelete()
        {
            NameCond = new List<Struct_NameCond>();
            NameCond_Extension = new List<string>();
            DateCond = new List<Struct_DateCond>();
            LengthCond = new List<Struct_LengthCond>();
            DateCond_Interval = new List<Struct_DateCond_Interval>();
            LengthCond_Interval = new List<Struct_LengthCond_Interval>();
        }

        /**
         * @Desc:       This method deletes a single file.
         * @Param:      string sourcePath - It provides the path of the file.
         */
        public void FileDelete(string sourcePath)
        {
            // Use a try block to catch IOExceptions, to
            // handle the case of the file already being
            // opened by another process.
            try
            {
                File.Delete(sourcePath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        /**
         * @Desc:       This method deletes multiple files of a given directory.
         * @Param:      string sourcePath - it's the path where the files to delete are.
         */
        public void FilesDelete(string sourcePath)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            files = SummonFilter(files, sourcePath);

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(sourcePath, file.Name);
                if (File.Exists(temppath))
                {
                    FileDelete(temppath);
                }
            }
        }

        /**
         * @Desc:       This method deletes a single directory.
         * @Param:      string sourcePath - The path of the directory to delete.
         */
        public void DirectoryDelete(string sourcePath)
        {
            // Delete a directory. Must be writable or empty.
            try
            {
                Directory.Delete(sourcePath, true);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /**
         * @Desc:       This method deletes the subdirectories of a given directory.
         * @Param:      string sourcePath - The path of the directory where the subdirectories are.
         */
        public void DirectoriesDelete(string sourcePath)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dirs = dir.GetDirectories();

            dirs = SummonFilter(dirs, sourcePath);

            foreach (DirectoryInfo subDir in dirs)
            {
                // Create the path to the new copy of the directory.
                string temppath = Path.Combine(sourcePath, subDir.Name);
                if (Directory.Exists(temppath))
                {
                    DirectoryDelete(temppath);
                }
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
