using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphLoadingAnimation : MonoBehaviour
{
    public Graph graph;
    public Material defaultMaterial, indicatorMaterial;

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
                yield return new WaitForSeconds(0.2f);
                currentEdgeMeshRenderer.material = defaultMaterial;
            }
        }
        StartCoroutine(PlayLoadingAnimation());
    }
}
