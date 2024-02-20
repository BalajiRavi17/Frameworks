using Newtonsoft.Json;
using OfficeOpenXml;
using System.Xml.Linq;

namespace WebAutomation.Common
{
    public class DataReader
    {
        
        public static T ReadDataFromJson<T>(string path)
        {
            string jsonData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public static string ReadDataFromTextFile(string filePath, int line) 
        {
            try
            {
                
                string[] lines = File.ReadAllLines(filePath);
                return lines[line];
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading text file: {ex.Message}");
            }
            return "";
        }
        public static string ReadDataFromXML(string filePath, string key)
        {
            try
            {
                // Load the XML document using LINQ to XML
                XDocument xmlDoc = XDocument.Load(filePath);

                // Access XML elements and attributes
                var elements = xmlDoc.Descendants(key);

                foreach (var element in elements)
                {
                    // Access element data as needed
                    return element.Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading XML file: {ex.Message}");
            }
            return "";
        }

        public static string ReadDataFromExcel(string filePath, int workSheet, int row, int col)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[workSheet];

                    // Assuming your data starts from cell A1
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;
                    return (string)worksheet.Cells[row, col].Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading Excel file: {ex.Message}");
            }
            return "";
        }
    }
}
