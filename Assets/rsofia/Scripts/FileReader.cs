using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace GameArcade
{
    public class FileReader : ParentOfAll
    {
        //Returns a list of all the files found at a certain path
        public static List<string> ReadAllFilesAtPath(string _path)
        {
            DirectoryInfo directory = new DirectoryInfo(_path);
            FileInfo[] files = directory.GetFiles();
            print("Files length: " + files.Length + " at path: " + _path);
            List<string> listFiles = new List<string>();
            foreach(FileInfo file in files)
            {
                UnityEngine.Debug.Log("File: " + file.Name);
                listFiles.Add(file.Name);
            }

            return listFiles;
        }

        //Returns an array of all the .exes inside a folder
        public static string[] GetExeInsideFolder(string _path)
        {

            return Directory.GetFiles(_path, "*.exe");
        }

        public static void WriteTextAtPath(string _path, string _filaName, string _text, bool _append)
        {
            if(!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

           TextWriter textWriter = new StreamWriter(_path + _filaName, _append);
            textWriter.WriteLine(_text);
            textWriter.Close();
        }

        //Returns JPGs inside folder or if none found, PNGs
        public static Texture2D[] GetImageInsideFolder(string _path)
        {
            Texture2D[] img = new Texture2D[] { new Texture2D(2, 2), new Texture2D(2,2)};
            string[] result = System.IO.Directory.GetFiles(_path, "*.jpg");
            if(result.Length == 0)
            {
                result = System.IO.Directory.GetFiles(_path, "*.png");
            }

            if(result.Length >= 1)
            {
                if (File.Exists(result[0]))
                {
                    byte[] fileData = File.ReadAllBytes(result[0]);
                    img[0].LoadImage(fileData);
                }
                if(result.Length >= 2)
                {
                    if (File.Exists(result[1]))
                    {
                        byte[] fileData = File.ReadAllBytes(result[1]);
                        img[1].LoadImage(fileData);
                    }
                }
            }
            return img;
        }

        //Returns path of of the first video mp4 or wav found
        public static string GetVideoPathFrom(string _path)
        {
            string[] result = System.IO.Directory.GetFiles(_path, "*.mp4");
            if (result.Length == 0)
            {
                result = System.IO.Directory.GetFiles(_path, "*.wav");
            }

            if (result.Length >= 1)
            {
                return result[0];
            }
            return null;
        }

        //Returns a list of all folders at a certain path
        public static List<string> ReadAllFoldersAtPath(string _path)
        {
            DirectoryInfo directory = new DirectoryInfo(_path);
            DirectoryInfo[] subfolders = directory.GetDirectories();
            List<string> folders = new List<string>();
            foreach (DirectoryInfo info in subfolders)
            {
                //UnityEngine.Debug.Log("Folder: " + info.Name);
                folders.Add(info.Name);
            }

            return folders;
        }

        //Returns all the content from a txt file from a path
        public static string[] ReadAllLinesFromTxtAtPath(string _path)
        {
            string[] files = System.IO.Directory.GetFiles(_path, "*.txt");
            //UnityEngine.Debug.Log("files length: " + files.Length + " path" + _path);
            if (files.Length >= 1)
            {
                return File.ReadAllLines(files[0]);
            }
            else
                return null;
        }

        //Executes a command from the comand window
        public static int ExecuteCommand(string _command, int _timeout = 1500)
        {
            int exitCode = 0;
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + _command)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process process = Process.Start(processInfo);
            process.WaitForExit(_timeout);
            exitCode = process.ExitCode;
            process.Close();
            return exitCode;
        }

    }
}

