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
 *          3.SPECIFY DIFERENT FILTER CONDITIONS FOR DIRECTORIES AND FILES???
 *          4.USE SWITCH CASE TO WORK WITH DIFERENT TYPE OF FILTERING THAT INVOLVES THE SAME
 *            CONDITIONS OR CONDITION.
 *          5.NEW CASE <> FOR THE DATETIME AND LENGTH METHODS.
 */

// Current namespace:
namespace FileGenius_Console
{
    /* @Desc:       This class provides the filtering methods. All of them work the same way, they receive
     *              an array of strings containing the names of either files or directories and a single
     *              condition that makes the filtering possible.
     */
    class FGFilter
    {
        // Empty constructor, there are no fields or properties in this class.
        public FGFilter()
        {

        }

        /**
         * @Desc:       This method does the filtering by name.
         * @Param:      string[] files - provides the array to filter. It could be either files or directories.
         * @Param:      string condition - It's the name that will do the filtering of the array.
         * @Param:      int index - index for the switch case where:
         *              0-> Contains.
         *              1-> Starts with.
         *              2-> Ends with. (For this particular case we need to remove the extension before comparing)
         * @Return:     string[] - The filtered array.
         * 
         * TODO:        CHECK GLOBAL TODO NUMBER 4.
         *              TEST CASE 2.
         */
        public string[] NameFilter(string[] files, string condition, int index)
        {
            // List that makes easy the filtering.
            List<string> temp = new List<string>();

            switch (index)
            {
                case 0:
                    foreach (string file in files)
                    {
                        if (file.Contains(condition))
                        {
                            temp.Add(file);
                        }
                    }
                    break;

                case 1:
                    foreach (string file in files)
                    {
                        if (file.StartsWith(condition))
                        {
                            temp.Add(file);
                        }
                    }
                    break;

                case 2:
                    foreach (string file in files)
                    {
                        //This must be done to remove the extension from the name and then evaluate.
                        int a = file.IndexOf('.');
                        file.Remove(a);

                        if (file.EndsWith(condition))
                        {
                            temp.Add(file);
                        }
                    }
                    break;

                default:
                    // TODO: An exception should be thrown here.
                    break;
            }

            files = temp.ToArray();
            return files;
        }

        /**
         * @Desc:       This method does the filtering by extension. This method will not work with directories.
         * @Param:      string[] files - provides the array to filter. It can be only with files.
         * @Param:      string condition - It's the name of the extension that will do the filtering of the array.
         * @Return:     string[] - The filtered array.
         * 
         * TODO:        CHECK GLOBAL TODO NUMBER 4.
         */
        public string[] ExtensionFilter(string[] files, string condition)
        {
            List<string> temp = new List<string>();
            foreach (string file in files)
            {
                if (file.EndsWith(condition))
                {
                    temp.Add(file);
                }
            }
            files = temp.ToArray();
            return files;
        }

        /**
         * @Desc:       This method does the filtering by date and time.
         * @Param:      string[] files - provides the array to filter. It works for both files and directories.
         * @Param:      DateTime condition - It's the date and time that will do the filtering of the array.
         * @Param:      int index - index for the switch case where:
         *              0-> creation time less than condition.
         *              1-> creation time less or equal than condition.
         *              2-> creation time equal than condition.
         *              3-> creation time greater or equal than condition.
         *              4-> creation time greater than condition.
         *              5-> last access time less than condition.
         *              6-> last access time less or equal than condition.
         *              7-> last access time equal than condition.
         *              8-> last access time greater or equal than condition.
         *              9-> last access time greater than condition.
         *              10-> last write time less than condition.
         *              11-> last write time less or equal than condition.
         *              12-> last write time equal than condition.
         *              13-> last write time greater or equal than condition.
         *              14-> last write time greater than condition.
         * @Param:      string sourcePath - needed to create a valid FileInfo or DirectoryInfo object.
         * @Param       bool isFile - Selects the proper switch case to compare files or directories.
         * @Return:     string[] - The filtered array.
         * 
         * TODO:        CHECK GLOBAL TODO NUMBER 4.
         */
        public string[] DateFilter(string[] files, DateTime condition, int index, string sourcePath, bool isFile)
        {
            List<string> temp = new List<string>();
            if (isFile)
            {

                switch (index)
                {
                    // Creation Time cases:
                    case 0:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime < condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 1:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime <= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 2:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime == condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 3:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime >= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 4:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime > condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    // Last Access Time cases:
                    case 5:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime < condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 6:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime <= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 7:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime == condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 8:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime >= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 9:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime > condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    // Last Write Time cases:
                    case 10:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime < condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 11:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime <= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 12:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime == condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 13:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime >= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 14:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime > condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    default:
                        // TODO: An exception should be thrown here.
                        break;
                }
            }
            else
            {

                switch (index)
                {
                    // Creation Time cases:
                    case 0:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime < condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 1:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime <= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 2:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime == condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 3:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime >= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 4:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime > condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    // Last Access Time cases:
                    case 5:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime < condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 6:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime <= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 7:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime == condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 8:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime >= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 9:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime > condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    // Last Write Time cases:
                    case 10:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime < condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 11:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime <= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 12:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime == condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 13:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime >= condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 14:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime > condition)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    default:
                        // TODO: An exception should be thrown here.
                        break;
                }
            }

            files = temp.ToArray();
            return files;
        }

        /**
         * @Desc:       This method does the filtering by length.
         * @Param:      string[] files - provides the array to filter. It works only for files.
         * @Param:      int condition - It's the length that will do the filtering of the array.
         * @Param:      int index - index for the switch case where:
         *              0-> length less than condition.
         *              1-> length less or equal to condition.
         *              2-> length equal to condition.
         *              3-> length greater or equal to condition.
         *              4-> length greater than condition.
         * @Param:      string sourcePath - needed to create a valid FileInfo object.
         * @Return:     string[] - The filtered array.
         * 
         * TODO:        CHECK GLOBAL TODO NUMBER 4.
         */
        public string[] LengthFilter(string[] files, int condition, int index, string sourcePath)
        {
            List<string> temp = new List<string>();
            switch (index)
            {
                case 0:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a < condition)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 1:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a <= condition)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 2:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a == condition)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 3:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a >= condition)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 4:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a > condition)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                default:
                    // TODO: Exception here.
                    break;
            }

            files = temp.ToArray();
            return files;
        }

        /**
         * @Desc:       This method does the filtering with an interval of date and time.
         * @Param:      string[] files - It's the array that contains the names of the files or directories.
         * @Param:      DateTime condA - The first condition, it MUST BE smaller than condB.
         * @Param:      DateTime condB - The second condition, it MUST BE bigger than condA.
         * @Param:      int index - index for the switch case where:
         *              0-> Creation time greater than A and smaller than B.
         *              1-> Creation time greater than A and smaller or equal to B.
         *              2-> Creation time greater than A and equal to B.
         *              3-> Creation time greater or equal to A and smaller than B.
         *              4-> Creation time greater or equal to A and smaller or equal to B.
         *              5-> Creation time greater or equal to A and equal to B.
         *              6-> Creation time equal to A and smaller than B.
         *              7-> Creation time equal to A and smaller or equal to B.
         *              8-> Creation time equal to A and equal to B.
         *              9-> Last access time greater than A and smaller than B.
         *              10-> Last access time greater than A and smaller or equal to B.
         *              11-> Last access time greater than A and equal to B.
         *              12-> Last access time greater or equal to A and smaller than B.
         *              13-> Last access time greater or equal to A and smaller or equal to B.
         *              14-> Last access time greater or equal to A and equal to B.
         *              15-> Last access time equal to A and smaller than B.
         *              16-> Last access time equal to A and smaller or equal to B.
         *              17-> Last access time equal to A and equal to B.
         *              18-> Last write time greater than A and smaller than B.
         *              19-> Last write time greater than A and smaller or equal to B.
         *              20-> Last write time greater than A and equal to B.
         *              21-> Last write time greater or equal to A and smaller than B.
         *              22-> Last write time greater or equal to A and smaller or equal to B.
         *              23-> Last write time greater or equal to A and equal to B.
         *              24-> Last write time equal to A and smaller than B.
         *              25-> Last write time equal to A and smaller or equal to B.
         *              26-> Last write time equal to A and equal to B.
         * @Param:      string sourcePath - needed to create a valid FileInfo or DirectoryInfo object.
         * @Param:      bool isFile - Selects the proper switch case to compare files or directories.
         * @Return:     string[] - The filtered array.
         */
        public string[] DateFilter(string[] files, DateTime condA, DateTime condB, int index, string sourcePath, bool isFile)
        {
            List<string> temp = new List<string>();
            if (isFile)
            {
                switch (index)
                {
                    // Creation Time cases:
                    case 0:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime > condA && fi.CreationTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 1:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime > condA && fi.CreationTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 2:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime > condA && fi.CreationTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 3:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime >= condA && fi.CreationTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 4:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime >= condA && fi.CreationTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 5:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime >= condA && fi.CreationTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 6:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime == condA && fi.CreationTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 7:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime == condA && fi.CreationTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 8:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime == condA && fi.CreationTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    // Last Access Time cases:
                    case 9:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime > condA && fi.LastAccessTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 10:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime > condA && fi.LastAccessTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 11:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime > condA && fi.LastAccessTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 12:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime >= condA && fi.LastAccessTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 13:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime >= condA && fi.LastAccessTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 14:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime >= condA && fi.LastAccessTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 15:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime == condA && fi.LastAccessTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 16:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime == condA && fi.LastAccessTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 17:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime == condA && fi.LastAccessTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    // Last Write Time cases:
                    case 18:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime > condA && fi.LastWriteTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 19:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime > condA && fi.LastWriteTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 20:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime > condA && fi.LastWriteTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 21:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime >= condA && fi.LastWriteTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 22:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime >= condA && fi.LastWriteTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 23:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime >= condA && fi.LastWriteTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 24:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime == condA && fi.LastWriteTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 25:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime == condA && fi.LastWriteTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 26:
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime == condA && fi.LastWriteTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    default:
                        // TODO: Exception here.
                        break;
                }
            }
            else
            {
                switch (index)
                {
                    // Creation Time cases:
                    case 0:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime > condA && fi.CreationTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 1:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime > condA && fi.CreationTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 2:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime > condA && fi.CreationTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 3:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime >= condA && fi.CreationTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 4:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime >= condA && fi.CreationTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 5:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime >= condA && fi.CreationTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 6:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime == condA && fi.CreationTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 7:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime == condA && fi.CreationTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 8:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.CreationTime == condA && fi.CreationTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    // Last Access Time cases:
                    case 9:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime > condA && fi.LastAccessTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 10:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime > condA && fi.LastAccessTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 11:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime > condA && fi.LastAccessTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 12:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime >= condA && fi.LastAccessTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 13:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime >= condA && fi.LastAccessTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 14:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime >= condA && fi.LastAccessTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 15:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime == condA && fi.LastAccessTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 16:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime == condA && fi.LastAccessTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 17:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastAccessTime == condA && fi.LastAccessTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    // Last Write Time cases:
                    case 18:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime > condA && fi.LastWriteTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 19:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime > condA && fi.LastWriteTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 20:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime > condA && fi.LastWriteTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 21:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime >= condA && fi.LastWriteTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 22:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime >= condA && fi.LastWriteTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 23:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime >= condA && fi.LastWriteTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 24:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime == condA && fi.LastWriteTime < condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 25:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime == condA && fi.LastWriteTime <= condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    case 26:
                        foreach (string file in files)
                        {
                            DirectoryInfo fi = new DirectoryInfo(Path.Combine(sourcePath, file));
                            if (fi.LastWriteTime == condA && fi.LastWriteTime == condB)
                            {
                                temp.Add(file);
                            }
                        }
                        break;
                    default:
                        // TODO: Exception here.
                        break;
                }
            }
            files = temp.ToArray();
            return files;
        }

        /**
         * @Desc:       This method does the filtering by length
         * @Param:      string[] files - The array of names that it's goinf to be filtered.
         * @Param:      int condA - The first condition, it MUST BE smaller than condB.
         * @Param:      int condB - The second condition, it MUST BE bigger than condA.
         * @Param:      int index - index for the switch case where:
         *              0-> Length bigger than A and smaller than B.
         *              1-> Length bigger than A and smaller or equal to B.
         *              2-> Length bigger than A and equal to B.
         *              3-> Length bigger or equal to A and smaller than B.
         *              4-> Length bigger or equal to A and smaller or equal to B.
         *              5-> Length bigger or equal to A and equal to B.
         *              6-> Length equal to A and smaller than B.
         *              7-> Length equal to A and smaller or equal to B.
         *              8-> Length equal to A and equal to B.
         * @Param:      string sourcePath - needed to generate a valid File or Directory object.
         * @Return:     string[] - The filtered array.
         */
        public string[] LengthFilter(string[] files, int condA, int condB, int index, string sourcePath)
        {
            List<string> temp = new List<string>();
            switch (index)
            {
                case 0:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a > condA && a < condB)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 1:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a > condA && a <= condB)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 2:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a > condA && a == condB)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 3:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a >= condA && a < condB)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 4:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a >= condA && a <= condB)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 5:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a >= condA && a == condB)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 6:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a == condA && a < condB)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 7:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a == condA && a <= condB)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                case 8:
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(sourcePath, file));
                        int a = Convert.ToInt32(fi.Length);
                        if (a == condA && a == condB)
                        {
                            temp.Add(file);
                        }
                    }
                    break;
                default:
                    // TODO: Exception here.
                    break;
            }
            files = temp.ToArray();
            return files;
        }
    }
}