using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System;
using System.Runtime.Serialization.Json;

using System.Linq;
using Valve.Newtonsoft.Json;

public class SaveLoadManager : MonoBehaviour
{
    /// Handle saving data to File
    /// 
    private void Start()
    {
        
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
        Stations.Add(station);
        station.PreviousStations.Add(station);
        List<string[]> stationJson = new List<string[]>();
        foreach (Station station_ in Stations)
        {

            stationJson.Add(station_.stationSerializer());
        }
        string json = SaveLoadManager.stationArraysJson(stationJson);
        SaveLoadManager.SaveToFile(json, "test");
        
        */
        


        /*
        Station tetStation = new Station(parts);
        
    
     

        string[] output = station.stationSerializer();
        List<string[]> stationsString = new List<string[]>();
        stationsString.Add(output);
        stationsString.Add(output);
        SaveToFile(stationArraysJson(stationsString), "test");


        Debug.Log(output);
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
    /*
    public static void SaveToFile<T>(T data, string fileName)
    {
        // Set the path to the persistent data path (works on most devices by default)
        string path = Application.persistentDataPath + "/saves/";
        // Create the directory IF it doesn't already exist
        Directory.CreateDirectory(path);
        Debug.Log("Path: " + path);

        // Grab an instance of the BinaryFormatter that will handle serializing our data
        //  BinaryFormatter formatter = new BinaryFormatter();

        // Using xml serializer
        var serializer = new XmlSerializer(typeof(T));

        // Open up a filestream, combining the path and object key
        FileStream fileStream = new FileStream(path + fileName + ".xml", FileMode.Create);

        // Try/Catch/Finally block that will attempt to serialize/write-to-stream, closing stream when complete
        try
        {
            //  formatter.Serialize(fileStream, objectToSave);
            serializer.Serialize(fileStream, data);
        }
        catch (SerializationException exception)
        {
            Debug.Log("Save failed. Error: " + exception.Message);
        }
        finally
        {
            fileStream.Close();
        }
    }
    */
    /// Load data using a string fileName
    public static T Load<T>(string fileName)
    {
        // Set the path to the persistent data path (works on most devices by default)
        string path = Application.persistentDataPath + "/saves/";

        JsonSerializer serializer = new JsonSerializer();

        // Grab an instance of the BinaryFormatter that will handle serializing our data
        // BinaryFormatter formatter = new BinaryFormatter();

        // Using xml serializer
        // var serializer = new XmlSerializer(typeof(T));

        // Open up a filestream, combining the path and fileName
        FileStream fileStream = new FileStream(path + fileName + ".json", FileMode.Open);
        // Initialize a variable with the default value of whatever type we're using
        //T returnData = default(T);

        /* 
         * Try/Catch/Finally block that will attempt to deserialize the data
         * If we fail to successfully deserialize the data, we'll just return the default value for Type
         */
        // T obj = null;
        try
        {
            // returnValue = (T)formatter.Deserialize(fileStream);

            //returnData = (T)serializer.Deserialize(fileStream);
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


        /*
        foreach (GameObject g in returnData as GameObject[])
        {
            Instantiate(g);
        }*/

        return default(T);
    }


    public static String stationArraysJson(List<string[]> stationArray)
    {
        String json = "[";
        foreach (var stationJson in stationArray)
        {
            json += "\n" + "\""+ stationJson[0]+ "\"" +": "+ stationJson[1] + ",";

        }
        json += "\n]";
        return json;
    }




}
