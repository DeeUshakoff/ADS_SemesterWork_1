using Microsoft.Office.Interop.Excel;
using System.Text;
using SemesterWork.Extensions;
namespace SemesterWork;

internal class Filework
{
    /// <summary>
    /// Reading the generated file from storage
    /// </summary>
    /// <param name="path">Path to the file</param>
    /// <returns>string[] read</returns>
    public static string[] ReadFile(string path) =>
        new StreamReader(path).
        ReadToEnd()
        .Split('\n')
        .Where(line => !String.IsNullOrWhiteSpace(line))
        .ToArray();

    /// <summary>
    /// Reading the generated file from storage
    /// </summary>
    /// <param name="path">Path to the file</param>
    /// <returns>List<string> read</returns>
    public static List<string> ReadFileList(string path) =>
        new StreamReader(path)
        .ReadToEnd()
        .Split('\n')
        .Where(line => !String.IsNullOrWhiteSpace(line))
        .ToList();

    /// <summary>
    /// Generates file for the task
    /// </summary>
    /// <param name="path">Path to the file</param>
    /// <param name="alphabet">Alphabet to generate the lines from them</param>
    /// <returns></returns>
    public static void GenerateFile(string path, string alphabet)
    {
        if (string.IsNullOrWhiteSpace(alphabet))
        { "Alphabet is null or empty, files doesnt generated".Print(ConsoleColor.Red); return; }

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        { "Directory not found".Print(ConsoleColor.Red); return; }
        
        System.Diagnostics.Stopwatch timer = new();
        timer.Start();

        var length = new Random().Next(50, 100);
        var array = new string[length];

        for (int i = 0; i < length; i++)
        {
            Random r_symbol = new();
            StringBuilder generated_string = new();

            for (int r_index = 0; r_index < new Random().Next(5000, 10001); r_index++) // i < random line length
                generated_string.Append(alphabet[r_symbol.Next(0, alphabet.Length - 1)]); // adding a random symbol from a-bet to the line
            array[i] = generated_string.ToString();
        }

        File.WriteAllLines(path, array);

        timer.Stop();

        $"{length} lines was added to file {path} \n{timer.Elapsed.TotalSeconds} seconds spent".Print(ConsoleColor.Green);
    }

    /// <summary>
    /// Method to generate statistics
    /// </summary>
    /// <param name="data">double[][], [i][0] = result, [i][1] = spended iterations, [i][2] = spended time</param>
    public static void ExportDataToExcel(double[][] data)
    {
        "Creating Excel file...".Print();
        Application app = new();
        
        var workSheet = (Worksheet)app.Workbooks.Add().Worksheets.get_Item(1);

        #region Filling info
        workSheet.Cells[1, 1] = "Result";
        workSheet.Cells[1, 2] = "Iterations";
        workSheet.Cells[1, 3] = "Time, sec";
        workSheet.Cells[1, 4] = "Data size";
        workSheet.Cells[1, 5] = "Total time";
        workSheet.Cells[39, 8] = "As you see, the charts are ~the same";
        workSheet.Columns[2].AutoFit();
        workSheet.Columns[3].ColumnWidth = 10;
        #endregion

        double totalTime = 0;

        data = data.OrderBy(x => x[1]).ThenBy(x => x[2]).ToArray();

        for (int i = 1; i <= data.Length; i++)
        {
            totalTime += data[i - 1][2];
            workSheet.Cells[i + 1, 1] = data[i - 1][0];
            workSheet.Cells[i + 1, 2] = data[i - 1][1];
            workSheet.Cells[i + 1, 3] = data[i - 1][2];
            workSheet.Cells[i + 1, 4] = data[i - 1][3];
        }

        workSheet.Cells[2, 5] = totalTime;

        CreateTable('B', 'D', "Data size / Iterations", 25);
        CreateTable('B', 'C' , "Data size / Time", 275);
        
        app.Visible = true;
        app.UserControl = true;

        void CreateTable(char col_1, char col_2, string title, int pos_y)
        {
            var chart = workSheet.ChartObjects(Type.Missing).Add(325, pos_y, 500, 250).Chart;
            var seriesCollection = (SeriesCollection)chart.SeriesCollection(Type.Missing);
            var series = seriesCollection.NewSeries();

            series.Values = workSheet.get_Range($"{col_1}2", $"{col_1}{data.Length}");
            series.XValues = workSheet.get_Range($"{col_2}2", $"{col_2}{data.Length}");
            chart.ChartType = XlChartType.xlLine;
            series.Name = title;
        }
        "Opened in Excel".Print(ConsoleColor.Green);
    }
}

