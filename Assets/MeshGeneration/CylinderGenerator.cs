using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderGenerator : MonoBehaviour
{
    public static GameObject CreateCylinder(Vector3 startPoint, Vector3 endPoint, Material material, float radius, int segments, Transform parent)
    {
        GameObject result = new GameObject();
        result.transform.parent = parent;
        MeshFilter meshFilter = result.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = result.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[(segments + 1) * 2];
        int[] triangles = new int[segments * 6];

        Vector3 direction = endPoint - startPoint;
        Vector3 circleNormal = direction.normalized;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, circleNormal);

        for (int i = 0; i <= segments; i++)
        {
            float angle = 2.0f * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            vertices[i] = startPoint + rotation * new Vector3(x, 0, z);
            vertices[i + segments + 1] = endPoint + rotation * new Vector3(x, 0, z);

            if (i < segments)
            {
                int baseIndex = i * 6;
                triangles[baseIndex] = i;
                triangles[baseIndex + 1] = (i + 1) % segments;
                triangles[baseIndex + 2] = i + segments + 1;

                triangles[baseIndex + 3] = (i + 1) % segments;
                triangles[baseIndex + 4] = (i + 1) % segments + segments + 1;
                triangles[baseIndex + 5] = i + segments + 1;
            }
        }

        // Assign vertices and triangles to the mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        mesh.RecalculateNormals();

        for (int i = 0; i < vertices.Length; i++)
        {
            // Set normals for both circles to be the same as circleNormal
            mesh.normals[i] = circleNormal;
        }

        return result;
    }
}
