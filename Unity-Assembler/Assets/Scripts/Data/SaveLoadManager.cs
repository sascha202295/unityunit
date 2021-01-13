﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System;
using Valve.Newtonsoft.Json;

public class SaveLoadManager : MonoBehaviour
{
    /// Handle saving data to File
    /// 
    private void Start()
    {
        

        //////   ******* Test ******///



        // create list of stations
        /*
        List<Station> Stations = new List<Station>();

        List<Part> parts = new List<Part>();

        parts.Add(new Part("test1"));

        List<Station> stations0 = new List<Station>();
        stations0.Add(new Station(parts));
        List<Station> stations = new List<Station>();

        stations.Add(new Station(parts, stations0));


        Station station = new Station(parts, stations);
        station.Position = new Vector3(1, 2, 3);
        station.Rotation = new Quaternion();
        station.Name = "station0";

        Station station1 = new Station(parts, stations);
        station1.Position = new Vector3(1, 2, 3);
        station1.Rotation = new Quaternion();
        station1.Name = "station1";


        Stations.Add(station);
        Stations.Add(station1);
        station.PreviousStations.Add(station);


        //  stationSerializer

        string json = Station2Json.stationSerializer(Stations);

        SaveLoadManager.SaveToFile(json, "test");


        //  stationDeserializer

        List<Station> stations1 = Station2Json.stationDeserializer("test");

        foreach (var gg in stations1)
        {
            Debug.Log("stations1:" + gg.Name);

        }
        */
    }
    private void Update()
    {

    }
    public static void SaveToFile(String json, String fileName)
    {
        // Set the path to the persistent data path (works on most devices by default)
        string path = Application.persistentDataPath + "/saves/";
        Debug.Log("Path: " + path);
        // Create the directory IF it doesn't already exist
        Directory.CreateDirectory(path);
        File.WriteAllText(@path + fileName + ".json", json);
    }

    /// Load data using a string fileName
    public static T Load<T>(string fileName)
    {
        // Set the path to the persistent data path (works on most devices by default)
        string path = Application.persistentDataPath + "/saves/";

        JsonSerializer serializer = new JsonSerializer();

        // Open up a filestream, combining the path and fileName
        FileStream fileStream = new FileStream(path + fileName + ".json", FileMode.Open);

        /* 
         * Try/Catch/Finally block that will attempt to deserialize the data
         */
        try
        {
            var sr = new StreamReader(fileStream);
            string contents = sr.ReadToEnd();
            T obj = JsonConvert.DeserializeObject<T>(contents);
            return obj;
        }
        catch (SerializationException exception)
        {
            Debug.Log("Load failed. Error: " + exception.Message);
        }
        finally
        {
            fileStream.Close();
        }

        return default(T);
    }
}
