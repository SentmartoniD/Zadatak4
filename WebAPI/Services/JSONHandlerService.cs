using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class JSONHandlerService : IJSONHandler
    {
        public async Task<bool> Save(InputDTO input)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string targetDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\..\"));
            string targetJSONFilePath = targetDirectory + "JSONDb.json";

            string jsonListString = File.ReadAllText(targetJSONFilePath);
            
            List<Input> myList = String.IsNullOrEmpty(jsonListString) ? new List<Input>() : JsonSerializer.Deserialize<List<Input>>(jsonListString);

            for (int i = 0; i < myList.Count; i++) {
                if (myList[i].FirstName == input.FirstName && myList[i].LastName == input.LastName && myList[i].Telephone == input.Telephone)
                    return false;
            }

            myList.Add(new Input { 
                Id = myList.Count, FirstName = input.FirstName, LastName = input.LastName, Telephone = input.Telephone});

             string newJSONListString  = JsonSerializer.Serialize(myList, new JsonSerializerOptions
             {
                 WriteIndented = true
             });

            using (StreamWriter streamWriter = new StreamWriter(targetJSONFilePath))
            {
                streamWriter.Write(newJSONListString);
            }

            return true;
        }
    }
}
