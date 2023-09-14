using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    //vertices have names because they're locations on a globe
    public Dictionary<string, GameObject> vertices;
    //Edges are between two locations, so we need a way to get them.
    public Dictionary<string, GameObject> edges;
    //Materials to indicate the state of each edge
    public List<Material> materials;
    //To set the radius of edges
    public float edgeRadius = 0.01f;
    //To set the number of segments in the cylinder representations of edges
    public int edgeSegments = 9;

    private void Start()
    {
        vertices = new();
        edges = new();
    }

    //Add a vertex
    public void AddVertex(string name, GameObject vertextGameObject)
    {
        vertices.Add(name, vertextGameObject);
    }

    //Remove a vertex
    public void RemoveVertex(string name)
    {
        //Remove all edges connected to this vertex
        foreach(string vertexName in vertices.Keys)
        {
            if (vertexName == name)
                continue;
            RemoveEdge(name, vertexName);
        }
    }

    //Add an edge
    public void AddEdge(string vertexNameA, string vertexNameB)
    {
        if(vertices.ContainsKey(vertexNameA) == false)
        {
            Debug.Log("Failed to add edge because there is no vertex of the name: " + vertexNameA);
            return;
        }
        if (vertices.ContainsKey(vertexNameB) == false)
        {
            Debug.Log("Failed to add edge because there is no vertex of the name: " + vertexNameB);
            return;
        }

        if(edges.ContainsKey(vertexNameA + vertexNameB) || edges.ContainsKey(vertexNameB + vertexNameA))
        {
            Debug.Log("Attempted to add a preexisting edge: " + vertexNameA + " " + vertexNameB);
            return;
        }

        GameObject newEdge = CylinderGenerator.CreateCylinder(vertices[vertexNameA].transform.position, vertices[vertexNameB].transform.position, materials[0], edgeRadius, edgeSegments, this.transform);
        edges.Add(vertexNameA + vertexNameB, newEdge);
    }

    //Remove and edge
    public void RemoveEdge(string vertexNameA, string vertexNameB)
    {
        if (edges.ContainsKey(vertexNameA + vertexNameB))
        {
            Destroy(edges[vertexNameA + vertexNameB]);
            edges.Remove(vertexNameA + vertexNameB);
        }
        if (edges.ContainsKey(vertexNameB + vertexNameA))
        {
            Destroy(edges[vertexNameB + vertexNameA]);
            edges.Remove(vertexNameB + vertexNameA);
        }
    }
}
