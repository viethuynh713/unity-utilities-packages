
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Kozits.ZooCoffee.Config
{
    public static class WriteScriptConfigHelper
    {
        private static StringBuilder GenerateDataItemScript(string csvName,Dictionary<string,string> fieldDictionary, string path)
        {
            // Create a StringBuilder to build the script content
            StringBuilder scriptBuilder = new StringBuilder();

            // Start the script with the class declaration
            scriptBuilder.AppendLine($"public class {csvName}Item : DataItemBase");
            scriptBuilder.AppendLine("{");

            // Add each field to the class
            foreach (var field in fieldDictionary)
            {
                string fieldType = field.Value;
                string fieldName = field.Key;
                if(fieldName.ToLower().Contains("id")) continue;
                scriptBuilder.AppendLine(string.Concat( "    public ", fieldType," ", fieldName,";").Replace("\"", ""));
            }

            // Close the class declaration
            scriptBuilder.AppendLine("}");

            return scriptBuilder;

        }
        
        public static void GenerateConfigDataScript(TextAsset csvData, string path)
        {
            // Create a StringBuilder to build the script content
            StringBuilder scriptBuilder = new StringBuilder();

            // Start the script with the class declaration
            scriptBuilder.AppendLine("using System;");
            scriptBuilder.AppendLine("using UnityEngine;");
            scriptBuilder.AppendLine("using Kozits.ZooCoffee.Config;");
            scriptBuilder.AppendLine();
            scriptBuilder.AppendLine($"public class {csvData.name}Config : ConfigBase<{csvData.name}Item>");
            scriptBuilder.AppendLine("{");

            scriptBuilder.AppendLine();

            // Close the class declaration
            scriptBuilder.AppendLine("}");
            
            scriptBuilder.AppendLine();
            scriptBuilder.AppendLine();
            

            // Define the file path where the script will be saved
            string filePath = Path.Combine(path, $"{csvData.name}Config.cs");

            // Write the script to a .cs file
            File.WriteAllText(filePath, scriptBuilder.ToString() +GenerateDataItemScript(csvData.name,ReadFieldInCsv(csvData), path).ToString());
        }
        
        private static Dictionary<string, string> ReadFieldInCsv(TextAsset csvData)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            
            if (csvData == null)
            {
                Debug.LogError("TextAsset data is null.");
                return result;
            }
            // Split the CSV file content into lines
            string[] lines = csvData.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 2)
            {
                Debug.LogError("CSV file does not contain enough rows.");
                return result;
            }

            // Split the first line to get field names
            string[] fieldNames = lines[0].Split(',');

            // Split the second line to get field types
            string[] fieldTypes = lines[1].Split(',');

            // Ensure that the number of field names matches the number of field types
            if (fieldNames.Length != fieldTypes.Length)
            {
                Debug.LogError("Mismatch between the number of field names and field types.");
                return result;
            }

            // Populate the dictionary with field names as keys and field types as values
            for (int i = 0; i < fieldNames.Length; i++)
            {
                string fieldName = fieldNames[i].Trim();
                string fieldType = fieldTypes[i].Trim();
                result[fieldName] = fieldType;
            }

            return result;
        }
    }
}