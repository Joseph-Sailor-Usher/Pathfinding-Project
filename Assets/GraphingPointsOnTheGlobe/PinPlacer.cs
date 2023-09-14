using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPlacer : MonoBehaviour
{
    public Graph graph;
    public GameObject pinPrefab;
    public Vector3 globeCenter;
    public List<GameObject> activePins;
    public List<Color> colors;

    private void Start()
    {
        activePins = new List<GameObject>();
    }

    public GameObject PlacePin(float latitude, float longitude)
    {
        GameObject newPin = Instantiate(pinPrefab, globeCenter, Quaternion.identity, this.transform);
        Vector3 tempRotation = new Vector3(0, longitude, latitude);
        newPin.transform.rotation = Quaternion.Euler(tempRotation);
        return newPin;
    }
}
