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
    public GameObject pinPrefab;
    public Vector3 globeCenter;
    public LocationList locationList; // Use the custom wrapper class
    public List<GameObject> activePins;

    private void Start()
    {
        activePins = new List<GameObject>();
        foreach (Location location in locationList.locations)
        {
            PlacePin(location.longitude, location.latitude);
        }
    }

    public void PlacePin(float latitude, float longitude)
    {
        GameObject newPin = Instantiate(pinPrefab, globeCenter, Quaternion.identity, this.transform);
        Vector3 tempRotation = new Vector3(0, longitude, latitude);
        newPin.transform.rotation = Quaternion.Euler(tempRotation);
    }
}
