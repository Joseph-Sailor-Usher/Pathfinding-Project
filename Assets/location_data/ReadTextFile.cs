using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class ReadTextFile : MonoBehaviour
{
    public List<string> vertices = new List<string>();
    public List<Vector2> coordinates = new List<Vector2>();
    public List<string> edges = new List<string>();
    public Graph graph;
    public PinPlacer pinPlacer;

    void Start()
    {
        string filePath = Application.dataPath + "/location_data.txt"; // Adjust the path as needed

        if (File.Exists(filePath))
        {
            string input = File.ReadAllText(filePath);
            ParseInput(input);
            if(graph != null)
            {
                for(int i = 0; i < vertices.Count; i++)
                {
                    //make a pin
                    GameObject newPin = pinPlacer.PlacePin(coordinates[i].x, coordinates[i].y);
                    //Add it to the graph
                    graph.AddVertex(vertices[i], newPin.GetComponent<EasyGameObjectReference>().reference);
                }
                for(int i = 0; i < edges.Count; i += 2)
                {
                    graph.AddEdge(edges[i], edges[i + 1]);
                }
            }
        } 
        else
        {
            Debug.LogError("File 'location_data.txt' not found.");
        }
    }

    void ParseInput(string input)
    {
        string[] lines = input.Split('\n');

        string currentSection = "";
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (line == "Vertices")
            {
                currentSection = "Vertices";
            }
            else if (line == "Edges")
            {
                currentSection = "Edges";
            }
            else if (currentSection == "Vertices")
            {
                vertices.Add(line);
                float x = float.Parse(lines[++i].Trim());
                float y = float.Parse(lines[++i].Trim());
                coordinates.Add(new Vector2(x, y));
            }
            else if (currentSection == "Edges")
            {
                edges.Add(line);
            }
        }

        // Print the parsed data to the console (for debugging)
        PrintParsedData();
    }

    void PrintParsedData()
    {
        Debug.Log("Vertices:");
        for (int i = 0; i < vertices.Count; i++)
        {
            Debug.Log(vertices[i] + " (" + coordinates[i].x + ", " + coordinates[i].y + ")");
        }

        Debug.Log("Edges:");
        foreach (string edge in edges)
        {
            Debug.Log(edge);
        }
    }
}
