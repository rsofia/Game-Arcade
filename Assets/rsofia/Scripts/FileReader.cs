using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace GameArcade
{
    public class FileReader : ParentOfAll
    {
        public List<string> ReadAllFilesAtPath(string _path)
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

        public List<string> ReadAllFoldersAtPath(string _path)
        {
            DirectoryInfo directory = new DirectoryInfo(_path);
            DirectoryInfo[] subfolders = directory.GetDirectories();
            List<string> folders = new List<string>();
            foreach (DirectoryInfo info in subfolders)
            {
                UnityEngine.Debug.Log("Folder: " + info.Name);
                folders.Add(info.Name);
            }

            return folders;
        }

        public static int ExecuteCommand(string _command, int _timeout)
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

