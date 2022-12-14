using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadSharp.FileHandlers {
    public class FileManager {

        public static string WorkingPath { get; set; }

        public static string[] ReadFile(string path) {
            try {
                WorkingPath = path;
                return File.ReadAllLines(path);
            } catch (ArgumentException) {
                return new string[0];
            }
        }

        public static void WriteFile(string path, string contents) {
            try {
                WorkingPath = path;
                File.WriteAllText(path, contents);
            } catch (ArgumentException) {
                return;
            }
        }
    }
}
