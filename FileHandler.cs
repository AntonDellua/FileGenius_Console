using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO:
/*
 *CHANGE EVERY MSDN SITE COMMENTS TO CUSTOM ONES.
 * 
 * GET RID OF SINGLE FILE ATRIBUTES AND METHODS IF NOT NEEDED (MAYBE NOT, BEING USED BY MULTIPLE FILES METHODS)
 * GET RID OF USELESS CODE
 * 
 * EVALUATE OPTIONS TO HANDLE MULTIPLE FILES
 * EVALUATE OPTIONS TO HANDLE DIRECTORIES
 * EVALUATE OPTIONS TO HANDLE BOTH FILES AND DIRECTORIES
 */

namespace FileGenius_Console
{
    class FileHandler
    {
        //Atributes:
        public string file;
        public string subDir;
        public string[] files;
        public string[] subDirs;
        public string sourcePath;
        public string destPath;
        public string sourceFile;
        public string sourceSubDir;
        public string destFile;
        public string destSubDir;

        //Constructors:

        public FileHandler()
        {
            return;
        }

        //Constructor for a single file
        public FileHandler(string newFile, string newSourcePath, string newDestPath)
        {
            setFile(newFile);
            setSourcePath(newSourcePath);
            setDestPath(newDestPath);
            setSourceFile();
            setDestFile();
        }

        //Constructor for multiple files
        public FileHandler(string newSourcePath, string newDestPath)
        {
            setSourcePath(newSourcePath);
            setDestPath(newDestPath);
            setFiles();
            setSubDirs();
        }

        //Methods:
        public bool createDirectory()
        {
            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
                return true;
            }
            return false;
        }

        public void copyFiles(bool ow)
        {
            // Copy the files and overwrite destination files if they already exist.
            //foreach (string s in filesToCopy)
            if (ow == true)
            {
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    file = Path.GetFileName(s);
                    destFile = Path.Combine(destPath, file);
                    //Console.WriteLine(destFile);
                    File.Copy(s, destFile, true);
                }
            }
            else
            {
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    file = Path.GetFileName(s);
                    destFile = Path.Combine(destPath, file);
                    //Console.WriteLine(destFile);
                    File.Copy(s, destFile, false);
                }
            }
        }

        public void moveFiles(/*string[] filesToMove*/)
        {
            //foreach (string s in filesToMove)
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.
                file = Path.GetFileName(s);
                sourceFile = Path.Combine(sourcePath, file);
                destFile = Path.Combine(destPath, file);
                if (!File.Exists(destFile))
                {
                    File.Move(sourceFile, destFile);
                }
            }

            //TODO: MOVE FILE (WITH THE USER CONSENT) EVEN IF THE FILE ALREADY EXISTS
        }

        public void deleteFiles(/*string[] filesToDelete*/)
        {
            //foreach (string s in filesToDelete)
            foreach (string s in files)
            {
                file = Path.GetFileName(s);
                sourceFile = Path.Combine(sourcePath, file);
                // Delete a file by using File class static method...
                if (File.Exists(sourceFile))
                {
                    // Use a try block to catch IOExceptions, to
                    // handle the case of the file already being
                    // opened by another process.
                    try
                    {
                        File.Delete(sourceFile);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
            }
        }

        public void copyAll(bool recFlag, bool ow)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            if (recFlag == false)
            {
                recFlag = true;
            }
            else
            {
                setFiles();
            }
            copyFiles(ow);
            foreach (DirectoryInfo subdir in dirs)
            {
                // Create the subdirectory.
                sourcePath = Path.Combine(sourcePath, subdir.Name);
                destPath = Path.Combine(destPath, subdir.Name);
                // Copy the subdirectories.
                //destPath = temppath;
                copyAll(recFlag, ow);
            }
            //TODO: RECURSIVELY COPY FOLDER AND SUBFOLDERS ACCORDING TO: http://stackoverflow.com/questions/1974019/folder-copy-in-c-sharp
        }

        public void moveAll()
        {
            moveSubDirs();
            moveFiles();
        }

        public void deleteAll()
        {
            deleteSubDirs();
            deleteFiles();
        }

        public void moveSubDirs()
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo di in dirs)
            {
                sourceSubDir = di.FullName;
                destSubDir = Path.Combine(destPath, di.Name);
                if (!Directory.Exists(destSubDir))
                {
                    //Directory.Move(sourceSubDir, destSubDir);
                    Directory.Move(sourceSubDir, destSubDir);
                }
            }

            /*
            foreach (string s in subDirs)
            {
                sourceSubDir = s;
                subDir = Path.GetFileName(s);
                destSubDir = Path.Combine(destPath, subDir);
                if (!Directory.Exists(destSubDir))
                {
                    //Directory.Move(sourceSubDir, destSubDir);
                    Directory.Move(sourceSubDir, destSubDir);
                }
            }*/

            //TODO: MOVE DIRECTORY (WITH THE USER CONSENT) EVEN IF THE DIRECTORY ALREADY EXISTS
        }

        public void deleteSubDirs()
        {
            foreach (string s in subDirs)
            {
                sourceSubDir = s;
                try
                {
                    Directory.Delete(sourceSubDir);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void copyFile()
        {
            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            File.Copy(sourceFile, destFile, true);

            //TODO: OPTION TO DON'T OVERWRITE CURRENT LOCATION      "File.Copy(sourceFile, destFile, false);"
        }

        public void moveFile()
        {
            // To move a file or folder to a new location:
            File.Move(sourceFile, destFile);
        }

        public void deleteFile()
        {
            // Delete a file by using File class static method...
            if (File.Exists(sourceFile))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    File.Delete(sourceFile);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }

        //Setters and Getters:

        public void setSubDirs()
        {
            if (Directory.Exists(sourcePath))
            {
                subDirs = Directory.GetDirectories(sourcePath);
            }
        }

        public void setSubDirs(string[] newSubDirs)
        {
            subDirs = newSubDirs;
        }

        public void setFiles()
        {
            //
            if (Directory.Exists(sourcePath))
            {
                files = Directory.GetFiles(sourcePath);
            }
            //else Console.WriteLine("U STUPID");
        }

        public void setFiles(string[] newFiles)
        {
            files = newFiles;
        }

        public void setFile(string newFile)
        {
            file = newFile;
        }

        public void setSourcePath(string newSourcePath)
        {
            sourcePath = newSourcePath;
        }

        public void setDestPath(string newDestPath)
        {
            destPath = newDestPath;
        }

        public void setSourceFile()
        {
            sourceFile = Path.Combine(sourcePath, file);
            //sourceFile = sourcePath + @"\" + file;
            //sourceFile = newSourceFile;
        }

        public void setDestFile()
        {
            destFile = Path.Combine(destPath, file);
            //destFile = destPath + @"\" + file;
            //destFile = newDestFile;
        }

        public string[] getSubDirs()
        {
            return subDirs;
        }

        public string getSubDir()
        {
            return subDir;
        }

        public string[] getFiles()
        {
            return files;
        }

        public string getFile()
        {
            return file;
        }

        public string getSourcePath()
        {
            return sourcePath;
        }

        public string getDestPath()
        {
            return destPath;
        }

        public string getSourceFile()
        {
            return sourceFile;
        }

        public string getDestFile()
        {
            return destFile;
        }
        
    }
}
