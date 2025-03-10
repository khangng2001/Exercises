using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatonicSolidsMeshGeneration : MonoBehaviour
{
    void Start()
    {
        //CreateCube();
        CreateTetrahedron();
    }

    private void CreateCube()
    {

        Vector3[] vertices = {
            new Vector3 (0, 0, 0),
            new Vector3 (1f, 0, 0),
            new Vector3 (1f, 1f, 0),
            new Vector3 (0, 1f, 0),
            new Vector3 (0, 1f, 1f),
            new Vector3 (1f, 1f, 1f),
            new Vector3 (1f, 0, 1f),
            new Vector3 (0, 0, 1f),
        };

        int[] triangles = {
            0, 2, 1, //face front
            0, 3, 2,
            2, 3, 4, //face top
            2, 4, 5,
            1, 2, 5, //face right
            1, 5, 6,
            0, 7, 4, //face left
            0, 4, 3,
            5, 4, 7, //face back
            5, 7, 6,
            0, 6, 7, //face bottom
            0, 1, 6
        };

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        this.GetComponent<Renderer>().material.color = Color.blue;
        //MeshUtility.Optimize(mesh);
        mesh.RecalculateNormals();
    }
    
    private void CreateTetrahedron()
    {
        Vector3[] vertices = {
            new Vector3 (0, 0, 0),
            new Vector3 (1f, 0, 0),
            new Vector3 (0.5f, 0, 0.866f),
            new Vector3 (0.5f, 0.866f, 0.288f),
        };

        int[] triangles = {
            0, 1, 2, //face front
            0, 2, 3, //face top
            0, 3, 1, //face right
            1, 3, 2, //face left
        };

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        this.GetComponent<Renderer>().material.color = Color.yellow;
        //MeshUtility.Optimize(mesh);
        mesh.RecalculateNormals();
    }
    

   
}
