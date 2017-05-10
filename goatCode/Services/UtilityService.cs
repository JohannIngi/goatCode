using goatCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Services
{
    public class UtilityService
    {
        private IAppDataContext _db;
        public UtilityService()
        {
            _db = new ApplicationDbContext();
        }
        public UtilityService(IAppDataContext context)
        {
            _db = context;
        }
        public string GetAceSettingsValueForExtension(string extension)
        {
            if (AceMap.ContainsKey(extension))
            {
                return AceMap[extension];
            }
            return "txt";
        }

        static Dictionary<string, string> AceMap = new Dictionary<string, string>
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
        public List<string> PopulateDropDownList()
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

        public string GetStartContentForExtension(string extension)
        {
            if (startContent.ContainsKey(extension))
            {
                return startContent[extension];
            }
            return "";
        }

    }
}