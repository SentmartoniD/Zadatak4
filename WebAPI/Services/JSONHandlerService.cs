﻿using System;
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
            // GET THE PATH TO THE JSONDb FILE IN SOLUTION FOLDER
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string targetDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\..\"));
            pathToJSONDb = targetDirectory + "JSONDb.json";
        }

        public async Task<bool> DeleteById(int id)
        {
            string jsonListString = File.ReadAllText(pathToJSONDb);
            List<Input> myList = String.IsNullOrEmpty(jsonListString) ? new List<Input>() : JsonSerializer.Deserialize<List<Input>>(jsonListString);
            int listLenght = myList.Count;
            for (int i = 0; i < listLenght; i++)
            {
                // IF ID IS IN THE LIST THEN REMOVE IT FROM THE LIST
                if (myList[i].Id == id)
                {
                    myList.RemoveAt(i);
                    break;
                }
            }
            // IF THE LIST LENGHT DIDNT CHANGE, THEN ID ISNT IN THE LIST
            if (listLenght == myList.Count)
                return false;

            // SERIALIZE THE LIST INTO A JSON STRING
            string newJSONListString = JsonSerializer.Serialize(myList, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // WRITE THE JSON TO THE JSONDb FILE
            using (StreamWriter streamWriter = new StreamWriter(pathToJSONDb))
            {
                streamWriter.Write(newJSONListString);
            }

            return true;
        }

        public async Task<List<Input>> GetAll()
        {
            // GET THE JSON OBJECTS AS A STRING
            string jsonListString = File.ReadAllText(pathToJSONDb);
            // DESERIALIZE THE STRING INTO A LIST
            List<Input> myList = String.IsNullOrEmpty(jsonListString) ? new List<Input>() : JsonSerializer.Deserialize<List<Input>>(jsonListString);
            return myList;
        }

        public async Task<Input> GetById(int id)
        {
            string jsonListString = File.ReadAllText(pathToJSONDb);
            List<Input> myList = String.IsNullOrEmpty(jsonListString) ? new List<Input>() : JsonSerializer.Deserialize<List<Input>>(jsonListString);
            Input input = null;
            for (int i = 0; i < myList.Count; i++) {
                // CHECK IF THE ID IS IN THE LIST
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
            string jsonListString = "";
            if (File.Exists(pathToJSONDb))
                jsonListString = File.ReadAllText(pathToJSONDb);
            
            List<Input> myList = String.IsNullOrEmpty(jsonListString) ? new List<Input>() : JsonSerializer.Deserialize<List<Input>>(jsonListString);

            for (int i = 0; i < myList.Count; i++) {
                // CHECK IF ITS A DUPLICATE
                if (myList[i].FirstName == input.FirstName && myList[i].LastName == input.LastName && myList[i].Telephone == input.Telephone)
                    return false;
            }

            // GET RANDOM INTEGER FOR ID
            Random rand = new Random();
            int randomNumber = rand.Next(0, 100000);

            myList.Add(new Input { 
                Id = randomNumber, FirstName = input.FirstName, LastName = input.LastName, Telephone = input.Telephone});

            // SERIALIZE THE LIST INTO A JSON STRING
            string newJSONListString  = JsonSerializer.Serialize(myList, new JsonSerializerOptions
            {
                 WriteIndented = true
            });

            // WRITE TO THE JSONDb FILE
            using (StreamWriter streamWriter = new StreamWriter(pathToJSONDb))
            {
                streamWriter.Write(newJSONListString);
            }

            return true;
        }
    }
}
