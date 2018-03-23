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

/* TODO:        1.CREATE THE ALGORITHM THAT MAKES POSSIBLE THE RENAMING. APPARENTLY, IT CAN BE DONE ONLY WITH
 *                FILE.MOVE
 *              2.HANDLE THE NAME CONDITIONS IN THIS CLASS AND THE OLD NAME PARAMETER FOR THE RENAME METHODS AS
 *                ONE. THEY MUST BE HANDLED AS ONE OR AS EQUALS IN THE PROGRAM.
 *              3.IT IS no longer LIKELY THAT THIS CLASS WILL BE THE ONLY ONE THAT IMPLEMENTS THE INTERFACE METHODS DIFFERENTLY.
 *                THIS BECAUSE THE NAME CONDITION DO NOT APPLY LIKE THE OTHER CLASSES SINCE EACH NAME CONDITION NEEDS
 *                TO CALL THE RENAME METHODS. HOWEVER I STILL DON'T KNOW IF THE REST OF THE CONDITIONS ARE GOING TO BE
 *                APPLIED THE SAME WAY SINCE THEY'RE GOING TO ACT FOR EACH NAME CONDITION APPLIED IN THE RENAME METHODS.
 *                THIS COULD BE SOLVED BY THE WAY THE FINAL PROGRAM IT'S GOING TO HANDLE THE METHOD CALLS OUTSIDE THIS
 *                CLASS. BUT THAT MAY NOT BE A GOOD PRACTICE.
 *                
 *                ***HOWEVER THIS COULD BE LEFT THIS WAY WITH THE LOGIC THAT THE NAME CONDITIONS AND THE PARAMETER
 *                   FOR THE METHOD COULD BE TREATED AS DIFFERENT***
 */

// Current namespace:
namespace FileGenius_Console
{
    /* @Desc:       This class provides the methods to do the renaming of files and directories.
     * 
     */
    class FGReName : FGFilter, FGInterface
    {
        // Fields - All the fields represent the conditions that the user can use to filter the files or directories.
        public List<Struct_NameCond> NameCond { get; set; }
        public List<string> NameCond_Extension { get; set; }
        public List<Struct_DateCond> DateCond { get; set; }
        public List<Struct_LengthCond> LengthCond { get; set; }
        public List<Struct_DateCond_Interval> DateCond_Interval { get; set; }
        public List<Struct_LengthCond_Interval> LengthCond_Interval { get; set; }

        // Constructor
        public FGReName()
        {
            NameCond = new List<Struct_NameCond>();
            NameCond_Extension = new List<string>();
            DateCond = new List<Struct_DateCond>();
            LengthCond = new List<Struct_LengthCond>();
            DateCond_Interval = new List<Struct_DateCond_Interval>();
            LengthCond_Interval = new List<Struct_LengthCond_Interval>();
        }

        public void FileRename(string sourcePath, string newName)
        {
            File.Move(sourcePath, newName);
        }

        public void DirectoryRename(string sourcePath, string newName)
        {
            Directory.Move(sourcePath, newName);
        }

        public void FilesRename(string sourcePath, string oldName, string newName)
        {
            // YOU HAVE TO CONSIDER THAT THERE'S ALWAYS GOING TO BE A NAME.CONTAINS CONDITION THAT IT'S
            // THE OLD NAME STRING THAT IT'S GOING TO BE REPLACED. YOU HAVE TO FIND A WAY TO ALWAYS ACCESS
            // THE CONDITION IN ORDER TO USE IT AS THE OLD NAME OR FIND A WAY TO PASS THE SAME PARAMETER 
            // THE CONDITION AND TO THE METHOD HERE.
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            FileInfo[] files = dir.GetFiles();
            files = SummonFilter(files, sourcePath);
            foreach (FileInfo file in files)
            {
                string temp = RenameHandler(file.Name, oldName, newName);
                FileRename(file.FullName, Path.Combine(sourcePath, temp));
            }
        }

        public void DirectoriesRename(string sourcePath, string oldName, string newName)
        {
            // YOU HAVE TO CONSIDER THAT THERE'S ALWAYS GOING TO BE A NAME.CONTAINS CONDITION THAT IT'S
            // THE OLD NAME STRING THAT IT'S GOING TO BE REPLACED. YOU HAVE TO FIND A WAY TO ALWAYS ACCESS
            // THE CONDITION IN ORDER TO USE IT AS THE OLD NAME OR FIND A WAY TO PASS THE SAME PARAMETER 
            // THE CONDITION AND TO THE METHOD HERE.
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dirs = dir.GetDirectories();
            dirs = SummonFilter(dirs, sourcePath);
            foreach (DirectoryInfo subdir in dirs)
            {
                string temp = RenameHandler(subdir.Name, oldName, newName);
                DirectoryRename(subdir.FullName, Path.Combine(sourcePath, temp));
            }
        }

        public string RenameHandler(string sourcePath, string oldName, string newName)
        {
            // Rigth now, this method can replace a certain part of the string or remove it.
            // TODO: Make it possible to add a new substring in any part of the original string.
            sourcePath = sourcePath.Replace(oldName, newName);
            return sourcePath;
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
