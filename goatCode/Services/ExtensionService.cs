﻿using System.Collections.Generic;
using System.Linq;


namespace goatCode.Services
{
    public class ExtensionService
    {
        /// <summary>
        /// Returning a list of all the extensions into a dropdownlist in alphabetical order
        /// </summary>
        /// <returns></returns>
        public List<string> PopulateDropDownList()
        {
            return Extensions.OrderBy(x => x).ToList();
        }
        /// <summary>
        /// Retreiving the "hello world" content for each file extension in the system
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string GetStartContentForExtension(string extension)
        {
            if (StartContent.ContainsKey(extension))
            {
                return StartContent[extension];
            }
            return "";
        }
        /// <summary>
        /// Retreiving the aceEditor extension for each extension we have an option for.
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string GetAceSettingsValueForExtension(string extension)
        {
            if (AceMap.ContainsKey(extension))
            {
                return AceMap[extension];
            }
            return "txt";
        }
        /// <summary>
        /// Using a hash map to get the appropriate extension from user selection to Ace extension
        /// </summary>
        private static Dictionary<string, string> AceMap = new Dictionary<string, string>
        {
            ["c"] = "c_cpp",
            ["cpp"] = "c_cpp",
            ["cs"] = "csharp",
            ["html"] = "html",
            ["java"] = "java",
            ["py"] = "python",
            ["sql"] = "sql",
            ["txt"] = "text",
            ["js"] = "js",
            ["go"] = "golang",
            ["hs"] = "haskell",
            ["scm"] = "lisp",
            ["md"] = "markdown",
            ["pas"] = "pascal",
            ["php"] = "php",
            ["pl"] = "prolog",
            ["rb"] = "ruby",
            ["tex"] = "tex"
        };
        /// <summary>
        /// Using a hash map to get the appropriate code sample from user selection to Ace editor
        /// </summary>
        private static Dictionary<string, string> StartContent = new Dictionary<string, string>
        {
            ["c"] = "#include <stdio.h>\n\nint main() {\n\tprintf(\"Hello World\\n\");\n\treturn 0;\n}",
            ["cpp"] = "#include <iostream>\n\nusing namespace std;\n\nint main()\n{\n\tcout << \"Hello World\" << endl;\n\n\treturn 0;\n}",
            ["cs"] = "using System.IO;\nusing System;\n\nclass Program\n{\n\tstatic void Main()\n{\n\tConsole.WriteLine(\"Hello, World!\");\n}\n}",
            ["html"] = "<!DOCTYPE html>\n<html>\n\t<head>\n\t\t<title>Web Page Design</title>\n\t\t<style type=\"text / css\">\n\t\tdiv\n\t\t{\n\t\t\twidth:100px;\n\t\t\theight:75px;\n\t\t\tbackground-color:red;\n\t\t\tborder:1px solid black;\n\t\t}\n\t\t</style>\n\t</head>\n\t<body>\n\t\t<div>Hello, World!</div>\n\t</body>\n</html>",
            ["java"] = "public class HelloWorld{\n\n\tpublic static void main(String []args){\n\t\tSystem.out.println(\"Hello World\");\n\t}\n}",
            ["py"] = "# Hello World program in Python\n\nprint \"Hello World!\\n\"",
            ["sql"] = "BEGIN TRANSACTION;\n\n/* Create a table called NAMES */\nCREATE TABLE NAMES(Id integer PRIMARY KEY, Name text);\n\n/* Create few records in this table */\nINSERT INTO NAMES\nVALUES(1,'Anton');\nINSERT INTO NAMES VALUES(2,'Mani');\nINSERT INTO NAMES VALUES(3,'Maggi');\nINSERT INTO NAMES VALUES(4,'Manni');\nINSERT INTO NAMES VALUES(5,'Arnar');\nCOMMIT;\n\n/* Display all the records from the table */\nSELECT * FROM NAMES;",
            ["txt"] = "",
            ["js"] = "<!DOCTYPE html>\n<html>\n\t<head>\n\t\t<title>Web Page Design</title>\n\t\t<script src=\"script.js\"></script>\n\t</head>\n\t<body>\n\t</body>\n</html>",
            ["go"] = "package main\n\nimport \"fmt\"\n\nfunc main() {\n\tfmt.Printf(\"hello, world\\n\")\n}",
            ["hs"] = "main = putStrLn \"hello world\"",
            ["scm"] = "(write-line \"Hello World\")",
            ["md"] = "A First Level Header\n====================\n\nA Second Level Header\n---------------------\n\nNow is the winter of our discontent, made glorious summer by this sun of York. This is just a regular paragraph.\n\nGood, better, best. Never let it rest. 'Til your good is better and your better is best.\n\n### Header 3\n\n> This is a blockquote.\n> \n> This is the second paragraph in the blockquote.\n>\n> ## This is an H2 in a blockquote",
            ["pas"] = "Program HelloWorld(output);\nbegin\n\twriteln(\'Hello, world!\');\nend.",
            ["php"] = "<html>\n\t<head>\n\t\t<title>Online PHP Script Execution</title>\n\t</head>\n\t<body>\n\t\t<?php\n\t\t\techo \" < h1 > Hello, PHP!</ h1 > \";\n\t\t?>\n\t</body>\n</html>",
            ["pl"] = ":- initialization(main).\nmain :- write(\'Hello World!\').",
            ["rb"] = "# Hello World Program in Ruby\nputs \"Hello World!\";",
            ["tex"] = "\\documentclass{article}\n\\usepackage{graphicx}\n\n\\begin{document}\n\n\t\\author{Author's Name}\n\n\t\\begin{abstract}\n\t\tThe abstract text goes here.\n\t\\end{abstract}\n\t\\begin{equation}\n\t\t\\label{simple_equation}\n\t\t\\alpha = \\sqrt{ \\beta }\n\t\\end{equation}\n\\end{document}"
        };
        /// <summary>
        /// The list of extension as displayed in the dropdownlist
        /// </summary>
        private static List<string> Extensions = new List<string>()
        {
            "c",
            "cpp",
            "cs",
            "html",
            "java",
            "py",
            "sql",
            "txt",
            "js",
            "go",
            "hs",
            "scm",
            "md",
            "pas",
            "php",
            "pl",
            "rb",
            "tex"
        };
    }
}
