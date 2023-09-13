using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPlacer : MonoBehaviour
{
    public GameObject pinPrefab;
    public List<GameObject> pins;

    private void Start()
    {
        pins = new();
    }

    public void PlacePin(float latitude, float longitude)
    {
        
    }
}
