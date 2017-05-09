using goatCode.Models;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace goatCode.Services
{
    public class FileService
    {
        /// <summary>
        /// Instance of database.
        /// </summary>

        static Dictionary<string, string> aceMap = new Dictionary<string, string>
        {
            ["c"] = "c_cpp",
            ["cpp"] = "c_cpp",
            ["cc"] = "c_cpp",
            ["cs"] = "csharp",
            ["html"] = "html",
            ["java"] = "java",
            ["py"] = "python",
            ["sql"] = "sql",
            ["txt"] = "text",
            ["js"] = "js",
            ["go"] = "golang",
            ["hs"] = "haskell",
            ["lhs"] = "haskell",
            ["scm"] = "lisp",
            ["ss"] = "lisp",
            ["md"] = "markdown",
            ["pp"] = "pascal",
            ["pas"] = "pascal",
            ["inc"] = "pascal",
            ["php"] = "php",
            ["phps"] = "php",
            ["pl"] = "prolog",
            ["p"] = "prolog",
            ["rb"] = "ruby",
            ["tex"] = "tex"
        };
        static Dictionary<string, string> startContent = new Dictionary<string, string>
        {
            ["c"] = "#include <stdio.h>\n\nint main() {\n\tprintf(\"Hello World\\n\")\n\treturn 0;\n}",
            ["cpp"] = "#include <iostream>\n\nusing namespace std;\n\nint main()\n{\n\tcout << \"Hello World\" << endl;\n\nreturn 0;\n}",
            ["cc"] = "#include <iostream>\n\nusing namespace std;\n\nint main()\n{\n\tcout << \"Hello World\" << endl;\n\nreturn 0;\n}",
            ["cs"] = "using System.IO;\nusing System;\n\nclass Program\n{\n\tstatic void Main()\n{\n\tConsole.WriteLine(\"Hello, World!\");\n}\n}",
            ["html"] = "<!DOCTYPE html>\n<html>\n\t<head>\n\t\t<title>Web Page Design</title>\n\t\t<style type=\"text / css\">\n\t\tdiv\n\t\t{\n\t\t\twidth:100px;\n\t\t\theight:75px;\n\t\t\tbackground-color:red;\n\t\t\tborder:1px solid black;\n\t\t}\n\t\t</style>\n\t</head>\n\t<body>\n\t\t<div>Hello, World!</div>\n\t</body>\n</html>",
            ["java"] = "public class HelloWorld{\n\n\tpublic static void main(String []args){\n\t\tSystem.out.println(\"Hello World\");\n\t}\n}",
            ["py"] = "# Hello World program in Python\n\nprint \"Hello World!\\n\"",
            ["sql"] = "BEGIN TRANSACTION;\n\n/* Create a table called NAMES */\nCREATE TABLE NAMES(Id integer PRIMARY KEY, Name text);\n\n/* Create few records in this table */\nINSERT INTO NAMES\nVALUES(1,'Anton');\nINSERT INTO NAMES VALUES(2,'Mani');\nINSERT INTO NAMES VALUES(3,'Maggi');\nINSERT INTO NAMES VALUES(4,'Manni');\nINSERT INTO NAMES VALUES(5,'Arnar');\nCOMMIT;\n\n/* Display all the records from the table */\nSELECT * FROM NAMES;",
            ["txt"] = "",
            ["js"] = "<!DOCTYPE html>\n<html>\n\t<head>\n\t\t<title>Web Page Design</title>\n\t\t<script src=\"script.js\"></script>\n\t</head>\n\t<body>\n\t</body>\n</html>",
            ["go"] = "package main\n\nimport \"fmt\"\n\nfunc main() {\n\tfmt.Printf(\"hello, world\\n\")\n}",
            ["hs"] = "main = putStrLn \"hello world\"",
            ["lhs"] = "main = putStrLn \"hello world\"",
            ["scm"] = "(write-line \"Hello World\")",
            ["ss"] = "(write-line \"Hello World\")",
            ["md"] = "A First Level Header\n====================\n\nA Second Level Header\n---------------------\n\nNow is the winter of our discontent, made glorious summer by this sun of York. This is just a regular paragraph.\n\nGood, better, best. Never let it rest. 'Til your good is better and your better is best.\n\n### Header 3\n\n> This is a blockquote.\n> \n> This is the second paragraph in the blockquote.\n>\n> ## This is an H2 in a blockquote",
            ["pp"] = "Program HelloWorld(output);\nbegin\n\twriteln(\'Hello, world!\');\nend.",
            ["pas"] = "Program HelloWorld(output);\nbegin\n\twriteln(\'Hello, world!\');\nend.",
            ["inc"] = "Program HelloWorld(output);\nbegin\n\twriteln(\'Hello, world!\');\nend.",
            ["php"] = "<html>\n\t<head>\n\t\t<title>Online PHP Script Execution</title>\n\t</head>\n\t<body>\n\t\t<?php\n\t\t\techo \" < h1 > Hello, PHP!</ h1 > \";\n\t\t?>\n\t</body>\n</html>",
            ["phps"] = "<html>\n\t<head>\n\t\t<title>Online PHP Script Execution</title>\n\t</head>\n\t<body>\n\t\t<?php\n\t\t\techo \" < h1 > Hello, PHP!</ h1 > \";\n\t\t?>\n\t</body>\n</html>",
            ["pl"] = ":- initialization(main).\nmain :- write(\'Hello World!\').",
            ["p"] = ":- initialization(main).\nmain :- write(\'Hello World!\').",
            ["rb"] = "# Hello World Program in Ruby\nputs \"Hello World!\";",
            ["tex"] = "\\documentclass{article}\n\\usepackage{graphicx}\n\n\\begin{document}\n\n\t\\author{Author's Name}\n\n\t\\begin{abstract}\n\t\tThe abstract text goes here.\n\t\\end{abstract}\n\t\\begin{equation}\n\t\t\\label{simple_equation}\n\t\t\\alpha = \\sqrt{ \\beta }\n\t\\end{equation}\n\\end{document}"
        };
        public List<string> PopulateDropDownList ()
        {
            List<string> extensions = new List<string>();

            extensions.Add("c");
            extensions.Add("cpp");
            extensions.Add("cc");
            extensions.Add("html");
            extensions.Add("java");
            extensions.Add("py");
            extensions.Add("sql");
            extensions.Add("sql");
            extensions.Add("txt");
            extensions.Add("js");
            extensions.Add("go");
            extensions.Add("hs");
            extensions.Add("lhs");
            extensions.Add("scm");
            extensions.Add("ss");
            extensions.Add("md");
            extensions.Add("pp");
            extensions.Add("pas");
            extensions.Add("inc");
            extensions.Add("php");
            extensions.Add("phps");
            extensions.Add("pl");
            extensions.Add("p");
            extensions.Add("rb");
            extensions.Add("tex");

            return extensions.OrderBy(x => x).ToList();
        }

        private readonly IAppDataContext _db;

        public FileService()
        {
            _db = new ApplicationDbContext();
        }

        public FileService(IAppDataContext context)
        {
            _db = context;
        }

        /// <summary>
        /// If projectID is in Projects table. It searches for the same projectID in ProjectFiles table.
        /// When the projectID is found in ProjectFiles we take the fileID from the same ProjectFilesID.
        /// </summary>
        /// <param name="id">To match Projects.ID with the value of this id</param>
        /// <returns>Returns a list of all files in this projectID.</returns>
        public string getAceSettingsValueForExtension(string extension)
        {
            if (aceMap.ContainsKey(extension))
            {
                return aceMap[extension];
            }
            return "txt";
        }

        public string getStartContentForExtension(string extension)
        {
            if (startContent.ContainsKey(extension))
            {
                return startContent[extension];
            }
            return "";
        }

        public List<File> GetFilesByProjectId(int id)
        {
            return (from p in _db.Projects
                    where p.ID == id
                    join pf in _db.ProjectFiles on p.ID equals pf.projectId
                    join f in _db.Files on pf.fileId equals f.ID
                    orderby f.name
                    select f).ToList();
        }

        /// <summary>
        /// Add new file to selected project. The new file is added to the Files table aswell as 
        /// both the selected projectID and fileID is added to the ProjectFiles table.
        /// </summary>
        /// <param name="model">Instance of NewFileViewModel to get parameters from the ViewModel</param>
        public List<File> GetAllFiles()
        {
            return _db.Files.ToList();
        }

        public void AddNewFile(NewFileViewModel model)
        {
            File file = new File { name = model.name, content = getStartContentForExtension(model.extension), extension = model.extension };
            _db.Files.Add(file);
            _db.SaveChanges();

            _db.ProjectFiles.Add(new ProjectFile { fileId = file.ID, projectId = model.ProjectId });
            _db.SaveChanges();
        }

        /// <summary>
        /// Gets a file from the Files table
        /// </summary>
        /// <param name="id">To let ID in Files get the same value as the parameter id</param>
        /// <returns>Returns single file from a known ID</returns>
        public File GetSingleFileById(int id)
        {
            return _db.Files.Where(x => x.ID == id).SingleOrDefault();
        }

        /// <summary>
        /// Searches by projectID to find a project, then removes all the files in specific project.
        /// </summary>
        /// <param name="projectId">So projectId in ProjectFiles table gets the same value as parameter projectId</param>

        public void DeleteAllFilesinProject(int projectId)
        {
            var files = _db.ProjectFiles.Where(x => x.projectId == projectId).ToList();
            foreach (var file in files)
            {
                _db.ProjectFiles.Remove(file);
            }
            _db.SaveChanges();
        }

        /// <summary>
        /// Updates content. The content in Files is something. After this the content will be the same as the user 
        /// writes in editor.
        /// </summary>
        /// <param name="content">Lets the content be same as the content in the parameter (from the editor)</param>

        public void UpdateFile(File file)
        {
            //TODO : Þetta virkar ekki þarf að skoða betur seinna.
            _db.setModified(file);

            _db.SaveChanges();
        }

        /// <summary>
        /// Finds a file with some ID and removes it from Files table.
        /// </summary>
        /// <param name="id">Lets the ID in file be the same as the parameter</param>
        public void DeleteFile(int id)
        {
            File file = new File { ID = id };
            _db.Files.Attach(file);
            _db.Files.Remove(file);
            _db.SaveChanges();
        }
        public void RemoveFileProjectConnection(int fileId)
        {
            var projectfile = (from pf in _db.ProjectFiles
                               where pf.fileId == fileId
                               select pf).ToList();
            foreach(var pfile in projectfile)
            {
                _db.ProjectFiles.Remove(pfile);
            }
            _db.SaveChanges();
        }
        public bool DoesFileNameExistInProject(int projectId, string fileName)
        {
            var file = (from p in _db.Projects
            where p.ID == projectId
            join pf in _db.ProjectFiles on p.ID equals pf.projectId
            join f in _db.Files on pf.fileId equals f.ID
            where f.name == fileName
            select f.name).SingleOrDefault();

            if(file == fileName)
            {
                return true;
            }
            
            return false;
        }
    }

}
