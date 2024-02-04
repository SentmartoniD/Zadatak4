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
        private readonly string pathToJSONDb;
        public JSONHandlerService()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string targetDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\..\"));
            pathToJSONDb = targetDirectory + "JSONDb.json";
        }

        public async Task<bool> DeleteById(int id)
        {
            string jsonListString = File.ReadAllText(pathToJSONDb);
            List<Input> myList = String.IsNullOrEmpty(jsonListString) ? new List<Input>() : JsonSerializer.Deserialize<List<Input>>(jsonListString);
            Input input = new Input();
            for (int i = 0; i < myList.Count; i++)
            {
                if (myList[i].Id == id)
                {
                    myList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public async Task<List<Input>> GetAll()
        {
            string jsonListString = File.ReadAllText(pathToJSONDb);
            List<Input> myList = String.IsNullOrEmpty(jsonListString) ? new List<Input>() : JsonSerializer.Deserialize<List<Input>>(jsonListString);
            return myList;
        }

        public async Task<Input> GetById(int id)
        {
            string jsonListString = File.ReadAllText(pathToJSONDb);
            List<Input> myList = String.IsNullOrEmpty(jsonListString) ? new List<Input>() : JsonSerializer.Deserialize<List<Input>>(jsonListString);
            Input input = new Input();
            for (int i = 0; i < myList.Count; i++) {
                if (myList[i].Id == id)
                {
                    input = myList[i];
                    break;
                }
            }
            return input;
        }

        public async Task<bool> Save(InputDTO input)
        {
            string jsonListString = File.ReadAllText(pathToJSONDb);
            
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

            using (StreamWriter streamWriter = new StreamWriter(pathToJSONDb))
            {
                streamWriter.Write(newJSONListString);
            }

            return true;
        }
    }
}
