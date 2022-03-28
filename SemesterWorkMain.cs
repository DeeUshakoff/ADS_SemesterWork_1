﻿namespace SemesterWork;

public class SemesterWorkMain
{
    public static void Main()
    {
        
        var path = Directory.GetCurrentDirectory() + "\\data.txt"; // path to the file
        
        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // alphabet, from the symbols of that file will be generated

        Filework.GenerateFile(path, alphabet); // file generating

        var file = Filework.ReadFile(path); // file reading

        var find = "GH"; // string, that must be finded in file's strings

        var results = new double[file.Length][];

        for (int i = 0; i < file.Length; i++)
        {
            System.Diagnostics.Stopwatch localTimer = new();
            localTimer.Start();
            var localRes = BM_Algorithm.BM_Search(file[i], find);  // algorithm 
            localTimer.Stop();

            results[i] = new[] { localRes[0], localRes[1], localTimer.Elapsed.TotalSeconds, file[i].Length };
        }

        Filework.ExportDataToExcel(results);
    }
}
