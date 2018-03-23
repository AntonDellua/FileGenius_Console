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
 *          
 */

// Current namespace:
namespace FileGenius_Console
{
    /* @Desc:   This interface provides necesary methods for all the classes involved in file handling.
     *          
     * <Method Descriptions>
     * 
     * @CreateDirectory:
     *      @Desc:      This method creates a new directory.
     *      @Param:     string path - provides the path.
     *      
     * @DirectoryExists:
     *      @Desc:      This method determinates if a directory exists.
     *      @Param:     string path - provides the path.
     *      @Return:    bool - true if it does exist.
     *      
     * @Filter:
     *      @Desc:      The Handler that evaluates the type of filtering that it's going to be applied
     *                  to the given array of either files or directories. It must be capable of handling
     *                  n conditions. The conditions are taken from each of the file handling classes.
     *      @Param:     string[] a - An array of strings that contains the Name of either files or subdirectories
     *                  of the current directory.
     *      @Param:     bool isFile - true if it is. It helps to specify what conditions apply to the current
     *                  execution of the method.
     *      @Return:    string[] - It returns an array of string with the filtering done.
     *      
     * @SummonFilter(DirectoryInfo[]):
     *      @Desc:      Works as a bridge between Filter and the current method trying to summon it. This is
     *                  because the file handling methods in the other classes work with arrays of objects that
     *                  provide the information of the files and directories (DirectoryInfo and FileInfo) but
     *                  the Filter method and the Filter class works exclusively with arrays of strings. This
     *                  method provides the convertion of DirectoryInfo to string and viceversa.
     *      @Param:     DirectoryInfo[] di - provides the array to convert.
     *      @Param:     string sourcePath - it's the current path, needed to the convertion back to DirectoryInfo.
     *      @Return:    DirectoryInfo[] - The new filtered array.
     *      
     * @SummonFilter(FileInfo[]):
     *      @Desc:      Works as a bridge between Filter and the current method trying to summon it. This is
     *                  because the file handling methods in the other classes work with arrays of objects that
     *                  provide the information of the files and directories (DirectoryInfo and FileInfo) but
     *                  the Filter method and the Filter class works exclusively with arrays of strings. This
     *                  method provides the convertion of FileInfo to string and viceversa.
     *      @Param:     FileInfo[] fi - provides the array to convert.
     *      @Param:     string sourcePath - it's the current path, needed to the convertion back to FileInfo.
     *      @Return:    FileInfo[] - The new filtered array.
     * 
     * </Method Descriptions>
     */
    interface FGInterface
    {
        void CreateDirectory(string path);
        bool DirectoryExists(string path);
        string[] Filter(string[] a, string sourcePath, bool isFile);
        DirectoryInfo[] SummonFilter(DirectoryInfo[] di, string sourcePath);
        FileInfo[] SummonFilter(FileInfo[] fi, string sourcePath);
    }
}
