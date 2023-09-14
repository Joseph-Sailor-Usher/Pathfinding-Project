using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Location
{
    public float longitude, latitude;
    public string name;
}

[Serializable]
public class LocationList
{
    public List<Location> locations;
}

public class PinPlacer : MonoBehaviour
{
    public Graph graph;
    public GameObject pinPrefab;
    public Vector3 globeCenter;
    public LocationList locationList;
    public List<GameObject> activePins;
    public List<Color> colors;

    private void Start()
    {
        activePins = new List<GameObject>();
        foreach (Location location in locationList.locations)
        {
            GameObject newPin = PlacePin(location.longitude, location.latitude);
            if (graph != null)
            {
                graph.AddVertex(location.name, newPin.GetComponent<EasyGameObjectReference>().reference);
            }
        }

        graph.AddEdge("Kansas City", "New York");
        graph.AddEdge("Gomez Pelacio", "Kansas City");
    }

    public GameObject PlacePin(float latitude, float longitude)
    {
        GameObject newPin = Instantiate(pinPrefab, globeCenter, Quaternion.identity, this.transform);
        Vector3 tempRotation = new Vector3(0, longitude, latitude);
        newPin.transform.rotation = Quaternion.Euler(tempRotation);
        return newPin;
    }
}
