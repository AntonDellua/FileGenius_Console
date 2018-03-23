/* @Title:  PROJECT FILEGENIUS.
 * 
 * @Desc:   WELCOME TO PROJECT FILEGENIUS, A SOFTWARE PROPOSAL THAT WILL BE CAPABLE
 *          OF HANDLING THE USER'S FILES & DIRECTORIES IN AN INTELLIGENT WAY. THIS
 *          IS THE FIRST EVER VERSION CREATED. FINAL NAME WILL BE DIFERENT.
 * 
 * @Author: LUIS ALBERTO ANTON DELGADILLO (ANTON DELLUA).
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* TODO:    2.INVESTIGATE THE ACCESS THAT MUST BE GRANTED BEFORE USING CERTAIN DIRECTORIES
 *          6.MODIFY THE FILTER METHOD TO HANDLE LOGIC CONNECTIONS WITH THE CONDITIONS. IT CURRENTLY SUPPORTS
 *            "AND" BUT NOTHING ELSE. IT SHOULD HANDLE "OR" AS WELL.
 *          7.CREATE AN ARTIFICIAL INTELLIGENCE OR AN ALGORITHM THAT WILL BE CAPABLE OF SUGGESTING WAYS TO HANDLE ALL
 *            THE FILES, DIRECTORIES AND SUBDIRECTORIES OF THE USER.
 *          8.FIND A WAY TO PERIODICALLY HANDLE THE FILES AND DIRECTORIES FOR THE USER WITHOUT EXECUTING THE
 *            MAIN PROGRAM.
 *          9.UPDATE THE OVERALL COMMENTS IN THE INTERFACE AND IN SOME METHODS IN THE PROJECT BECAUSE OF THE
 *            MODIFICATIONS TO THE FILTER HANDLER METHODS IN ORDER TO INCLUDE STRUCTS.
 *          10.DEFINE WHERE TO PUT THE STRUCTS WITHIN THE PROJECT. FOR NOW THEY'LL STAY HERE AT THE MAIN PROGRAM CLASS.
 *          12.FIND A WAY TO MANAGE IF THE SOURCE PATH IS A FILE, OR IF THE USER JUST WANTS TO MOVE A SINGLE FILE OR
 *             DIRECTORY, AND TO FIND OUT IF THE SOURCE PATH IS EITHER A FILE OR A DIRECTORY.
 *          13.THERE'S A PORBLEM WHERE THE PROGRAM MOVES, COPIES OR DELETES (HANDLES) THE FILES AND DIRECTORIES WHEN
 *             YOU WORK WITH LENGTH CONDITIONS. IT HANDLES THE FILES CORRECTLY BUT IT STILL COPIES THE SUBDIRS WITHOUT
 *             SPECIFYING IF IT SHOULD OR SHOULDN'T.
 *          14.MODIFY THE RENAME CLASS SO IT CAN DO MORE.
 *          15.IN THE FINAL PROJECT, TEST ALL THE CASE SCENARIOS AND CATCH ALL THE POSSIBLE EXCEPTIONS THAT COULD
 *             HAPPEN AND, IF POSSIBLE, FIND A WAY TO SOLVE THEM WITHIN THE PROGRAM.
 */

namespace FileGenius_Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            int x;
            // @"C:\Users\anton\Downloads", @"C:\Users\anton\Desktop"
            
            // Welcoming for the user.
            Console.WriteLine("Welcome user to the first ever version of the FileGenius Project! A software proposal that aims "
                + "for a better management of the files and directories on your PC. To continue please press any key...");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            // Actual loop for the program.
            do
            {
                Console.Clear();
                Console.WriteLine("Okay, so what do you want to do?");
                Console.WriteLine();
                Console.WriteLine("0.- Exit");
                Console.WriteLine("1.- Copy");
                Console.WriteLine("2.- Move");
                Console.WriteLine("3.- Delete");
                Console.WriteLine();
                x = int.Parse(Console.ReadLine());
                switch (x)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Are you sure you want to exit?   0.-YES / 1.-NO");
                        int case0_temp = int.Parse(Console.ReadLine());
                        if (case0_temp != 0)
                        {
                            x = -1;
                        }
                        break;
                    case 1:
                        FGCopy testC = new FGCopy();
                        Console.Clear();
                        Console.WriteLine("This option can copy the files and subdirectories from a given path");
                        Console.WriteLine();
                        Console.Write("Source Path -> ");
                        string sourcePath = Console.ReadLine();
                        Console.Write("Destination Path -> ");
                        string destPath = Console.ReadLine();
                        if (!testC.DirectoryExists(sourcePath))
                        {
                            Console.Clear();
                            Console.WriteLine("The given source path does not exist, you'll go back to the beginning");
                            Console.ReadKey();
                            x = -1;
                            break;
                        }
                        Console.WriteLine();
                        Console.WriteLine("Want to add some conditions for the filtering? 0.-YES / 1.-NO");
                        int y = int.Parse(Console.ReadLine());
                        if(y == 0)
                        {
                            int z;
                            do
                            {
                                Console.WriteLine("What type of condition do you want to add?");
                                Console.WriteLine("0.-Cancel");
                                Console.WriteLine("1.-Name condition");
                                Console.WriteLine("2.-Extension");
                                Console.WriteLine("3.-Date and time");
                                Console.WriteLine("4.-Size in bytes");
                                z = int.Parse(Console.ReadLine());
                                switch (z)
                                {
                                    case 0:
                                        z = 0;
                                        break;
                                    case 1:
                                        Console.WriteLine("This are the Name options:");
                                        Console.WriteLine("1.-Name contains condition");
                                        Console.WriteLine("2.-Name starts with condition");
                                        Console.WriteLine("3.-Name ends with condition");
                                        int w = int.Parse(Console.ReadLine());
                                        if (w < 1 || w > 3)
                                        {
                                            Console.WriteLine("Error");
                                            break;
                                        }
                                        Console.WriteLine("What is the condition? (Write a string)");
                                        string temp = Console.ReadLine();
                                        testC.NameCond.Add(new Struct_NameCond(temp, w-1));
                                        break;
                                    case 2:
                                        Console.WriteLine("Write the extension");
                                        string temp1 = Console.ReadLine();
                                        testC.NameCond_Extension.Add(temp1);
                                        break;
                                    case 3:
                                        Console.WriteLine("This are the Date and Time options:");
                                        Console.WriteLine("0-> creation time less than condition.");
                                        Console.WriteLine("1-> creation time less or equal than condition.");
                                        Console.WriteLine("2-> creation time equal than condition.");
                                        Console.WriteLine("3-> creation time greater or equal than condition.");
                                        Console.WriteLine("4-> creation time greater than condition.");
                                        Console.WriteLine("5-> last access time less than condition.");
                                        Console.WriteLine("6-> last access time less or equal than condition.");
                                        Console.WriteLine("7-> last access time equal than condition.");
                                        Console.WriteLine("8-> last access time greater or equal than condition.");
                                        Console.WriteLine("9-> last access time greater than condition.");
                                        Console.WriteLine("10-> last write time less than condition.");
                                        Console.WriteLine("11-> last write time less or equal than condition.");
                                        Console.WriteLine("12-> last write time equal than condition.");
                                        Console.WriteLine("13-> last write time greater or equal than condition.");
                                        Console.WriteLine("14-> last write time greater than condition.");
                                        int case3_var = int.Parse(Console.ReadLine());
                                        if (case3_var < 0 || case3_var > 14)
                                        {
                                            Console.WriteLine("Error");
                                            break;
                                        }
                                        Console.WriteLine("Write the date:");
                                        Console.WriteLine("First the year");
                                        int case3_dt_y = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Now the month");
                                        int case3_dt_m = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Finally the day");
                                        int case3_dt_d = int.Parse(Console.ReadLine());
                                        DateTime case3_dt = new DateTime(case3_dt_y, case3_dt_m, case3_dt_d);
                                        testC.DateCond.Add(new Struct_DateCond(case3_dt, case3_var));
                                        break;
                                    case 4:
                                        Console.WriteLine("This are the Size of file options:");
                                        Console.WriteLine("0-> length less than condition.");
                                        Console.WriteLine("1-> length less or equal to condition.");
                                        Console.WriteLine("2-> length equal to condition.");
                                        Console.WriteLine("3-> length greater or equal to condition.");
                                        Console.WriteLine("4-> length greater than condition.");
                                        int case4_var = int.Parse(Console.ReadLine());
                                        if (case4_var < 0 || case4_var > 4)
                                        {
                                            Console.WriteLine("Error");
                                            break;
                                        }
                                        Console.WriteLine("Write the size in bytes");
                                        int case4_cond = int.Parse(Console.ReadLine());
                                        testC.LengthCond.Add(new Struct_LengthCond(case4_cond, case4_var));
                                        break;
                                    default:
                                        Console.WriteLine("Invalid case");
                                        z = -1;
                                        break;
                                }
                            } while (z != 0);
                        }
                        testC.DirectoriesCopy(sourcePath, destPath, false, true);
                        break;
                    case 2:
                        FGMove testM = new FGMove();
                        Console.Clear();
                        Console.WriteLine("This option can move the files and subdirectories from a given path");
                        Console.WriteLine();
                        Console.Write("Source Path -> ");
                        string case2_sourcePath = Console.ReadLine();
                        Console.Write("Destination Path -> ");
                        string case2_destPath = Console.ReadLine();
                        if (!testM.DirectoryExists(case2_sourcePath))
                        {
                            Console.Clear();
                            Console.WriteLine("The given source path does not exist, you'll go back to the beginning");
                            Console.ReadKey();
                            x = -1;
                            break;
                        }
                        Console.WriteLine();
                        Console.WriteLine("Want to add some conditions for the filtering? 0.-YES / 1.-NO");
                        int case2_y = int.Parse(Console.ReadLine());
                        if (case2_y == 0)
                        {
                            int z;
                            do
                            {
                                Console.WriteLine("What type of condition do you want to add?");
                                Console.WriteLine("0.-Cancel");
                                Console.WriteLine("1.-Name condition");
                                Console.WriteLine("2.-Extension");
                                Console.WriteLine("3.-Date and time");
                                Console.WriteLine("4.-Size in bytes");
                                z = int.Parse(Console.ReadLine());
                                switch (z)
                                {
                                    case 0:
                                        z = 0;
                                        break;
                                    case 1:
                                        Console.WriteLine("This are the Name options:");
                                        Console.WriteLine("1.-Name contains condition");
                                        Console.WriteLine("2.-Name starts with condition");
                                        Console.WriteLine("3.-Name ends with condition");
                                        int w = int.Parse(Console.ReadLine());
                                        if (w < 1 || w > 3)
                                        {
                                            Console.WriteLine("Error");
                                            break;
                                        }
                                        Console.WriteLine("What is the condition? (Write a string)");
                                        string temp = Console.ReadLine();
                                        testM.NameCond.Add(new Struct_NameCond(temp, w - 1));
                                        break;
                                    case 2:
                                        Console.WriteLine("Write the extension");
                                        string temp1 = Console.ReadLine();
                                        testM.NameCond_Extension.Add(temp1);
                                        break;
                                    case 3:
                                        Console.WriteLine("This are the Date and Time options:");
                                        Console.WriteLine("0-> creation time less than condition.");
                                        Console.WriteLine("1-> creation time less or equal than condition.");
                                        Console.WriteLine("2-> creation time equal than condition.");
                                        Console.WriteLine("3-> creation time greater or equal than condition.");
                                        Console.WriteLine("4-> creation time greater than condition.");
                                        Console.WriteLine("5-> last access time less than condition.");
                                        Console.WriteLine("6-> last access time less or equal than condition.");
                                        Console.WriteLine("7-> last access time equal than condition.");
                                        Console.WriteLine("8-> last access time greater or equal than condition.");
                                        Console.WriteLine("9-> last access time greater than condition.");
                                        Console.WriteLine("10-> last write time less than condition.");
                                        Console.WriteLine("11-> last write time less or equal than condition.");
                                        Console.WriteLine("12-> last write time equal than condition.");
                                        Console.WriteLine("13-> last write time greater or equal than condition.");
                                        Console.WriteLine("14-> last write time greater than condition.");
                                        int case3_var = int.Parse(Console.ReadLine());
                                        if (case3_var < 0 || case3_var > 14)
                                        {
                                            Console.WriteLine("Error");
                                            break;
                                        }
                                        Console.WriteLine("Write the date:");
                                        Console.WriteLine("First the year");
                                        int case3_dt_y = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Now the month");
                                        int case3_dt_m = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Finally the day");
                                        int case3_dt_d = int.Parse(Console.ReadLine());
                                        DateTime case3_dt = new DateTime(case3_dt_y, case3_dt_m, case3_dt_d);
                                        testM.DateCond.Add(new Struct_DateCond(case3_dt, case3_var));
                                        break;
                                    case 4:
                                        Console.WriteLine("This are the Size of file options:");
                                        Console.WriteLine("0-> length less than condition.");
                                        Console.WriteLine("1-> length less or equal to condition.");
                                        Console.WriteLine("2-> length equal to condition.");
                                        Console.WriteLine("3-> length greater or equal to condition.");
                                        Console.WriteLine("4-> length greater than condition.");
                                        int case4_var = int.Parse(Console.ReadLine());
                                        if (case4_var < 0 || case4_var > 4)
                                        {
                                            Console.WriteLine("Error");
                                            break;
                                        }
                                        Console.WriteLine("Write the size in bytes");
                                        int case4_cond = int.Parse(Console.ReadLine());
                                        testM.LengthCond.Add(new Struct_LengthCond(case4_cond, case4_var));
                                        break;
                                    default:
                                        Console.WriteLine("Invalid case");
                                        z = -1;
                                        break;
                                }
                            } while (z != 0);
                        }
                        testM.DirectoriesMove(case2_sourcePath, case2_destPath);
                        testM.FilesMove(case2_sourcePath, case2_destPath);
                        break;
                    case 3:
                        FGDelete testD = new FGDelete();
                        Console.Clear();
                        Console.WriteLine("This option can delete the files and subdirectories from a given path");
                        Console.WriteLine();
                        Console.Write("Source Path -> ");
                        string case3_sourcePath = Console.ReadLine();
                        if (!testD.DirectoryExists(case3_sourcePath))
                        {
                            Console.Clear();
                            Console.WriteLine("The given source path does not exist, you'll go back to the beginning");
                            Console.ReadKey();
                            x = -1;
                            break;
                        }
                        Console.WriteLine();
                        Console.WriteLine("Want to add some conditions for the filtering? 0.-YES / 1.-NO");
                        int case3_y = int.Parse(Console.ReadLine());
                        if (case3_y == 0)
                        {
                            int z;
                            do
                            {
                                Console.WriteLine("What type of condition do you want to add?");
                                Console.WriteLine("0.-Cancel");
                                Console.WriteLine("1.-Name condition");
                                Console.WriteLine("2.-Extension");
                                Console.WriteLine("3.-Date and time");
                                Console.WriteLine("4.-Size in bytes");
                                z = int.Parse(Console.ReadLine());
                                switch (z)
                                {
                                    case 0:
                                        z = 0;
                                        break;
                                    case 1:
                                        Console.WriteLine("This are the Name options:");
                                        Console.WriteLine("1.-Name contains condition");
                                        Console.WriteLine("2.-Name starts with condition");
                                        Console.WriteLine("3.-Name ends with condition");
                                        int w = int.Parse(Console.ReadLine());
                                        if (w < 1 || w > 3)
                                        {
                                            Console.WriteLine("Error");
                                            break;
                                        }
                                        Console.WriteLine("What is the condition? (Write a string)");
                                        string temp = Console.ReadLine();
                                        testD.NameCond.Add(new Struct_NameCond(temp, w - 1));
                                        break;
                                    case 2:
                                        Console.WriteLine("Write the extension");
                                        string temp1 = Console.ReadLine();
                                        testD.NameCond_Extension.Add(temp1);
                                        break;
                                    case 3:
                                        Console.WriteLine("This are the Date and Time options:");
                                        Console.WriteLine("0-> creation time less than condition.");
                                        Console.WriteLine("1-> creation time less or equal than condition.");
                                        Console.WriteLine("2-> creation time equal than condition.");
                                        Console.WriteLine("3-> creation time greater or equal than condition.");
                                        Console.WriteLine("4-> creation time greater than condition.");
                                        Console.WriteLine("5-> last access time less than condition.");
                                        Console.WriteLine("6-> last access time less or equal than condition.");
                                        Console.WriteLine("7-> last access time equal than condition.");
                                        Console.WriteLine("8-> last access time greater or equal than condition.");
                                        Console.WriteLine("9-> last access time greater than condition.");
                                        Console.WriteLine("10-> last write time less than condition.");
                                        Console.WriteLine("11-> last write time less or equal than condition.");
                                        Console.WriteLine("12-> last write time equal than condition.");
                                        Console.WriteLine("13-> last write time greater or equal than condition.");
                                        Console.WriteLine("14-> last write time greater than condition.");
                                        int case3_var = int.Parse(Console.ReadLine());
                                        if (case3_var < 0 || case3_var > 14)
                                        {
                                            Console.WriteLine("Error");
                                            break;
                                        }
                                        Console.WriteLine("Write the date:");
                                        Console.WriteLine("First the year");
                                        int case3_dt_y = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Now the month");
                                        int case3_dt_m = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Finally the day");
                                        int case3_dt_d = int.Parse(Console.ReadLine());
                                        DateTime case3_dt = new DateTime(case3_dt_y, case3_dt_m, case3_dt_d);
                                        testD.DateCond.Add(new Struct_DateCond(case3_dt, case3_var));
                                        break;
                                    case 4:
                                        Console.WriteLine("This are the Size of file options:");
                                        Console.WriteLine("0-> length less than condition.");
                                        Console.WriteLine("1-> length less or equal to condition.");
                                        Console.WriteLine("2-> length equal to condition.");
                                        Console.WriteLine("3-> length greater or equal to condition.");
                                        Console.WriteLine("4-> length greater than condition.");
                                        int case4_var = int.Parse(Console.ReadLine());
                                        if (case4_var < 0 || case4_var > 4)
                                        {
                                            Console.WriteLine("Error");
                                            break;
                                        }
                                        Console.WriteLine("Write the size in bytes");
                                        int case4_cond = int.Parse(Console.ReadLine());
                                        testD.LengthCond.Add(new Struct_LengthCond(case4_cond, case4_var));
                                        break;
                                    default:
                                        Console.WriteLine("Invalid case");
                                        z = -1;
                                        break;
                                }
                            } while (z != 0);
                        }
                        testD.DirectoriesDelete(case3_sourcePath);
                        testD.FilesDelete(case3_sourcePath);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("This is not a valid scenario, please try again.");
                        Console.ReadKey();
                        x = -1;
                        break;
                }
            } while (x != 0);

            // Exit.
            Console.Clear();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

    /**
     * @Desc:       This structs come to replace all of the previous parameters or conditions in the classes of the
     *              program. Their function is the same but they now have an integer hand by hand with the contition(s)
     *              indicating the case or comparation that it's going to be applied to the condition itself.
     */
    public struct Struct_NameCond
    {
        public string s { get; set; }
        public int i { get; set; }
       // public bool OR;
        public Struct_NameCond(string s, int i/*, bool OR*/)
        {
            this.s = s;
            this.i = i;
            //this.OR = OR;
        }
    }
    public struct Struct_NameCond_Extension
    {
        public string s;
        public bool OR;
        public Struct_NameCond_Extension(string s, bool OR)
        {
            this.s = s;
            this.OR = OR;
        }
    }
    public struct Struct_DateCond
    {
        public DateTime dt { get; set; }
        public int i { get; set; }
        //public bool OR;
        public Struct_DateCond(DateTime dt, int i/*, bool OR*/)
        {
            this.dt = dt;
            this.i = i;
            //this.OR = OR;
        }
    }
    public struct Struct_LengthCond
    {
        public int l { get; set; }
        public int i { get; set; }
        public Struct_LengthCond(int l, int i)
        {
            this.l = l;
            this.i = i;
        }
    }
    public struct Struct_DateCond_Interval
    {
        public DateTime dtA { get; set; }
        public DateTime dtB { get; set; }
        public int i { get; set; }
        public Struct_DateCond_Interval(DateTime dtA, DateTime dtB, int i)
        {
            this.dtA = dtA;
            this.dtB = dtB;
            this.i = i;
        }
    }
    public struct Struct_LengthCond_Interval
    {
        public int lA { get; set; }
        public int lB { get; set; }
        public int i { get; set; }
        public Struct_LengthCond_Interval(int lA, int lB, int i)
        {
            this.lA = lA;
            this.lB = lB;
            this.i = i;
        }
    }
}

/*FGMove testM = new FGMove();
            FGCopy testC = new FGCopy();
            FGDelete testD = new FGDelete();
            try
            {
                // User interaction simulation:

                //testC.NameCond_Extension.Add(".txt");
                //testC.NameCond_Contains.Add("Text");

                // Actual method calling:

                //testC.DirectoriesCopy(@"C:\Users\anton\Downloads", @"C:\Users\anton\Desktop", false, true);
                //testC.DirectoryCopy(@"C:\Users\anton\Downloads\folder", @"C:\Users\anton\Desktop");
                //testD.DirectoryDelete(@"C:\Users\anton\Downloads\folder");
                //testM.DirectoriesMove(@"C:\Users\anton\Downloads\folder", @"C:\Users\anton\Desktop");
                //testM.DirectoryMove(@"C:\Users\anton\Downloads\folder", @"C:\Users\anton\Desktop");
                //testM.FilesMove(@"C:\Users\anton\Downloads", @"C:\Users\anton\Desktop");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            // Keep console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();*/


/*FGReName testR = new FGReName();
            //testR.NameCond.Add(new Struct_NameCond("Txt", 0));
            try
            {
                //testR.DirectoriesRename(@"C:\Users\anton\Downloads", "Txt", "Text");
                testR.FilesRename(@"C:\Users\anton\Downloads", "Trash", "");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
*/


