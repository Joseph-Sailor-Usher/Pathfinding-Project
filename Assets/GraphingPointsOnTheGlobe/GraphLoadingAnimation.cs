using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphLoadingAnimation : MonoBehaviour
{
    public Graph graph;
    public PolyphonicSinewave sines;
    public Material defaultMaterial, indicatorMaterial;
    public float offsetFreq = 100, freqScalar = 10.0f;

    private void Start()
    {
        if (graph == null)
            gameObject.SetActive(false);
        StartCoroutine(WaitForGraphLoad());
    }

    private IEnumerator WaitForGraphLoad()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayLoadingAnimation());
    }

    private IEnumerator PlayLoadingAnimation()
    {
        foreach(string location in graph.vertices.Keys)
        {
            yield return new WaitForSeconds(0.5f);
            foreach(string adjacentLocation in graph.adjacentVertices[location])
            {
                MeshRenderer currentEdgeMeshRenderer = graph.GetEdge(location, adjacentLocation).GetComponent<MeshRenderer>();
                currentEdgeMeshRenderer.material = indicatorMaterial;
                float newFreq = Vector3.Distance(graph.vertices[location].transform.position, graph.vertices[adjacentLocation].transform.position);
                sines.frequency1 = (newFreq * freqScalar) + offsetFreq;
                sines.PlayTone(sines.frequency1, 0);
                yield return new WaitForSeconds(0.2f);
                currentEdgeMeshRenderer.material = defaultMaterial;
            }
        }
        StartCoroutine(PlayLoadingAnimation());
    }
}
