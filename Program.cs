// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace NotesCommandLine
{
    class Program
    {
        static void Main(string[] args) //Main method
        {
            ReadCommand();
            Console.ReadLine();
        }

        //create a string with the note's path directory which is
        //the path of the special folder found in MyDocuments called "Notes"
        private static string NoteDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/Notes/";

        //function to read in console user input; specified as the "command"
        private static void ReadCommand()
        {
            Console.Write(Directory.GetDirectoryRoot(NoteDirectory));    //write to console the note directory string
            String Command = Console.ReadLine();                        //read user input command

            //Main(null) sends us back to main method for an infinite loop (until user types exit)
            switch (Command.ToLower())
            {
                case "new":
                    NewNote();
                    Main(null);
                    break;

                case "edit":
                    EditNote();
                    Main(null);
                    break;

                case "read":
                    ReadNote();
                    Main(null);
                    break;
        
                case "delete":
                    DeleteNote();
                    Main(null);
                    break;
        
                case "shownotes":
                    ShowNotes();
                    Main(null);
                    break;

                case "dir":
                    NotesDirectory();
                    Main(null);
                    break;
        
                case "cls":
                    Console.Clear();
                    Main(null);
                    break;
        
                case "exit":
                    Exit();
                    break;

                default:
                    CommandsAvailable();
                    Main(null);
                    break;
            }
        }

        private static void NewNote()
        {
           Console.WriteLine("Please Enter Note:\n");
           string input = Console.ReadLine();//Read user input

           XmlWriterSettings NoteSettings = new XmlWriterSettings();//Adds XML settings. Change as you wish.

           NoteSettings.CheckCharacters = false;
           NoteSettings.ConformanceLevel = ConformanceLevel.Auto;
           NoteSettings.Indent = true;

           string FileName = DateTime.Now.ToString("dd-MM-yy") + ".xml";//File name. I chose date format.

          //Writes file
           using (XmlWriter NewNote = XmlWriter.Create(NoteDirectory + FileName, NoteSettings))
           {
               NewNote.WriteStartDocument();
               NewNote.WriteStartElement("Note");
               NewNote.WriteElementString("body", input);
               NewNote.WriteEndElement();

               NewNote.Flush();
               NewNote.Close();
           }
        }

        private static void EditNote()
        {
           Console.WriteLine("Please enter file name.\n");
           string FileName = Console.ReadLine().ToLower();//Read user input

           if (File.Exists(NoteDirectory + FileName))
           {
               XmlDocument doc = new XmlDocument();

               //Load the document
               try
               {
                   doc.Load(NoteDirectory + FileName);

                   Console.Write(doc.SelectSingleNode("//body").InnerText);//Store Note

                   string ReadInput = Console.ReadLine();

                   if (ReadInput.ToLower() == "cancel") Main(null);
                   else
                   {
                       string newText = doc.SelectSingleNode("//body").InnerText = ReadInput;
                       doc.Save(NoteDirectory + FileName);
                   }
               }
               catch (Exception ex)
               {
                   Console.WriteLine("Could not edit note following error occurred: " + ex.Message);
               }
           }
           else
           {
               Console.WriteLine("File not found\n");
           }
        }

        private static void ReadNote()
        {
           Console.WriteLine("Please enter file name.\n");
           string FileName = Console.ReadLine().ToLower();

           if (File.Exists(NoteDirectory + FileName))
           {
               XmlDocument Doc = new XmlDocument();
               Doc.Load(NoteDirectory + FileName);

               Console.WriteLine(Doc.SelectSingleNode("//body").InnerText);
           }
           else
           {
               Console.WriteLine("File not found");
           }
        }

        private static void DeleteNote()
        {
           Console.WriteLine("Please enter file name\n");
           string FileName = Console.ReadLine();

           if (File.Exists(NoteDirectory + FileName))
           {
               Console.WriteLine(Environment.NewLine + "Are you sure you wish to delete this file? Y/N\n");
               string Confirmation = Console.ReadLine().ToLower();

               if (Confirmation == "y")
               {
                   try
                   {
                       File.Delete(NoteDirectory + FileName);
                       Console.WriteLine("File has been deleted\n");
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine("File not deleted following error occured: " + ex.Message);
                   }
               }
               else if (Confirmation == "n") Main(null);
               else
               {
                   Console.WriteLine("Invalid command\n");
                   DeleteNote();
               }
           }
           else
           {
               Console.WriteLine("File does not exist\n");
               DeleteNote();
           }
        }

        private static void ShowNotes()
        {
          string NoteLocation = NoteDirectory;

           DirectoryInfo Dir = new DirectoryInfo(NoteLocation);

           if (Directory.Exists(NoteLocation))
           {
               FileInfo[] NoteFiles = Dir.GetFiles("*.xml");

               if (NoteFiles.Count() != 0)
               {
                   Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop + 2);
                   Console.WriteLine("+------------+");
                   foreach (var item in NoteFiles)
                   {
                        Console.WriteLine("  " +item.Name);
                   }
                   Console.WriteLine(Environment.NewLine);
               }
               else
               {
                   Console.WriteLine("No notes found.\n");
               }
           }
           else
           {
               Console.WriteLine("Directory does not exist.....creating directory\n");
               Directory.CreateDirectory(NoteLocation);
               Console.WriteLine("Directory: " + NoteLocation + " created successfully.\n");
           }
        }

        private static void Exit()
        {
           Environment.Exit(0);
        }

        private static void CommandsAvailable()
        {
           Console.WriteLine(" New - Create a new note\n Edit - Edit a note\n Read -  Read a note\n Delete - Delete a file\n ShowNotes - List all notes\n Exit - Exit the application\n Dir - Opens note directory\n Help - Shows this help message\n");
        }

        private static void NotesDirectory()
        {
           System.Diagnostics.Process.Start("open", $"-R /" + NoteDirectory );
        }
    }
}

