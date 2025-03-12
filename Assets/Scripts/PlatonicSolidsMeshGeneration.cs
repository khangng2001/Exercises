using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Shape
{
    Cube,
    Tetrahedron,
    Octahedron
}

public class PlatonicSolidsMeshGeneration : MonoBehaviour
{
    [SerializeField] private Shape _shape;
    private void Start()
    {
        switch (_shape)
        {
            case Shape.Cube: 
                GenerateCube();
                break;
            case Shape.Tetrahedron:
                GenerateTetrahedron();
                break;
            case Shape.Octahedron:
                GenerateOctahedron();
                break;
        }
    }
    

    private void GenerateCube()
    {
        Vector3[] vertices = new Vector3[24];
        {
            //front-face
            vertices[0] = new Vector3(0, 0, 0); 
            vertices[1] = new Vector3(0, 1, 0);
            vertices[2] = new Vector3(1, 1, 0);
            vertices[3] = new Vector3(1, 0 ,0);
            
            //back-face
            vertices[4] = new Vector3(1, 0, 1);
            vertices[5] = new Vector3(1,1,1);
            vertices[6] = new Vector3(0,1,1);
            vertices[7] = new Vector3(0, 0, 1);
            
            //top-face
            vertices[8] = new Vector3(0, 1, 0);
            vertices[9] = new Vector3(0, 1, 1);
            vertices[10] = new Vector3(1, 1, 1);
            vertices[11] = new Vector3(1, 1, 0);
            
            //bottom-face
            vertices[12] = new Vector3(0,0,1);
            vertices[13] = new Vector3(0, 0, 0);
            vertices[14] = new Vector3(1, 0, 0);
            vertices[15] = new Vector3(1,0 , 1);
            
            //left-face
            vertices[16] = new Vector3(0, 0, 1);
            vertices[17] = new Vector3(0, 1, 1);
            vertices[18] = new Vector3(0, 1, 0);
            vertices[19] = new Vector3(0, 0, 0);
            
            //right-face
            vertices[20] = new Vector3(1, 0, 0);
            vertices[21] = new Vector3(1, 1, 0);
            vertices[22] = new Vector3(1, 1, 1);
            vertices[23] = new Vector3(1, 0, 1);
        }
        
        // Each face has 6 triangles --> 6 faces --> 36 triangles in total
        int[] triangles = new int[36];
        {
            // Front face
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;
        
            // Back face
            triangles[6] = 4;
            triangles[7] = 5;
            triangles[8] = 6;
            triangles[9] = 4;
            triangles[10] = 6;
            triangles[11] = 7;
        
            // Top face
            triangles[12] = 8;
            triangles[13] = 9;
            triangles[14] = 10;
            triangles[15] = 8;
            triangles[16] = 10;
            triangles[17] = 11;
        
            // Bottom face
            triangles[18] = 12;
            triangles[19] = 13;
            triangles[20] = 14;
            triangles[21] = 12;
            triangles[22] = 14;
            triangles[23] = 15;
        
            // Left face
            triangles[24] = 16;
            triangles[25] = 17;
            triangles[26] = 18;
            triangles[27] = 16;
            triangles[28] = 18;
            triangles[29] = 19;
        
            // Right face
            triangles[30] = 20;
            triangles[31] = 21;
            triangles[32] = 22;
            triangles[33] = 20;
            triangles[34] = 22;
            triangles[35] = 23;
        }
        
        Vector2[] uvs = new Vector2[24];
        {
            // Front face
            uvs[0] = new Vector2(0, 0); // Bottom-left
            uvs[1] = new Vector2(0, 1); // Top-left
            uvs[2] = new Vector2(1, 1); // Top-right
            uvs[3] = new Vector2(1, 0); // Bottom-right
        
            // Back face
            uvs[4] = new Vector2(0, 0);
            uvs[5] = new Vector2(0, 1);
            uvs[6] = new Vector2(1, 1);
            uvs[7] = new Vector2(1, 0);
        
            // Top face
            uvs[8] = new Vector2(0, 0);
            uvs[9] = new Vector2(0, 1);
            uvs[10] = new Vector2(1, 1);
            uvs[11] = new Vector2(1, 0);
        
            // Bottom face
            uvs[12] = new Vector2(0, 0);
            uvs[13] = new Vector2(0, 1);
            uvs[14] = new Vector2(1, 1);
            uvs[15] = new Vector2(1, 0);
        
            // Left face
            uvs[16] = new Vector2(0, 0);
            uvs[17] = new Vector2(0, 1);
            uvs[18] = new Vector2(1, 1);
            uvs[19] = new Vector2(1, 0);
        
            // Right face
            uvs[20] = new Vector2(0, 0);
            uvs[21] = new Vector2(0, 1);
            uvs[22] = new Vector2(1, 1);
            uvs[23] = new Vector2(1, 0);
        }
        
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        GetComponent<MeshRenderer>().material.color = Color.cyan;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    private void GenerateTetrahedron()
    {
        Vector3[] vertices = new Vector3[4];
        {
            vertices[0] = new Vector3(0, 0, 0);
            vertices[1] = new Vector3(1, 0, 0);
            vertices[2] = new Vector3(0.5f, 0f, 0.866f);

            vertices[3] = new Vector3(0.5f, 0.866f, 0.288f);
        }

        //4 faces --> each face has 3 triangles --> 12 triangles in total
        int[] triangles = new int[12];
        {
            triangles[0] = 0;
            triangles[1] = 2;
            triangles[2] = 1;
        
            // Front face
            triangles[3] = 0;
            triangles[4] = 1;
            triangles[5] = 3;
        
            // Right face
            triangles[6] = 1;
            triangles[7] = 2;
            triangles[8] = 3;
        
            // Left face
            triangles[9] = 2;
            triangles[10] = 0;
            triangles[11] = 3;
        }
        Vector2[] uvs = new Vector2[4];
        {
            uvs[0] = new Vector2(0, 0);  // Bottom-left
            uvs[1] = new Vector2(1, 0);  // Bottom-right
            uvs[2] = new Vector2(0.5f, 1); // Back
            uvs[3] = new Vector2(0.5f, 0.5f); // Top
        }
        
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear(); 
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        // Recalculate the normals for proper lighting
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

    }

    private void GenerateOctahedron()
    {
    // Define the 6 vertices of the octahedron
    Vector3[] vertices = new Vector3[6];
    {
        // The octahedron has 6 vertices at the ends of the 3 axes
        vertices[0] = new Vector3(0, 1, 0);    // Top vertex
        vertices[1] = new Vector3(1, 0, 0);    // Right vertex
        vertices[2] = new Vector3(0, 0, 1);    // Forward vertex
        
        vertices[3] = new Vector3(-1, 0, 0);   // Left vertex
        vertices[4] = new Vector3(0, 0, -1);   // Back vertex
        vertices[5] = new Vector3(0, -1, 0);   // Bottom vertex
    }

    // Define the 8 triangular faces (24 indices total)
    int[] triangles = new int[24];
    {
        // Top half (4 triangles connecting the top vertex with the middle vertices)
        // Top-Right-Forward triangle
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        
        // Top-Forward-Left triangle
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;
        
        // Top-Left-Back triangle
        triangles[6] = 0;
        triangles[7] = 3;
        triangles[8] = 4;
        
        // Top-Back-Right triangle
        triangles[9] = 0;
        triangles[10] = 4;
        triangles[11] = 1;
        
        // Bottom half (4 triangles connecting the bottom vertex with the middle vertices)
        // Bottom-Right-Forward triangle (note the winding order is reversed compared to top)
        triangles[12] = 5;
        triangles[13] = 2;
        triangles[14] = 1;
        
        // Bottom-Forward-Left triangle
        triangles[15] = 5;
        triangles[16] = 3;
        triangles[17] = 2;
        
        // Bottom-Left-Back triangle
        triangles[18] = 5;
        triangles[19] = 4;
        triangles[20] = 3;
        
        // Bottom-Back-Right triangle
        triangles[21] = 5;
        triangles[22] = 1;
        triangles[23] = 4;
    }

    // Define UVs for texturing
    Vector2[] uvs = new Vector2[6];
    {
        // Simple UV mapping for each vertex
        uvs[0] = new Vector2(0.5f, 1.0f);   // Top
        uvs[1] = new Vector2(1.0f, 0.5f);   // Right
        uvs[2] = new Vector2(0.5f, 0.75f);  // Forward
        uvs[3] = new Vector2(0.0f, 0.5f);   // Left
        uvs[4] = new Vector2(0.5f, 0.25f);  // Back
        uvs[5] = new Vector2(0.5f, 0.0f);   // Bottom
    }

    // Get or create the mesh
    Mesh mesh = GetComponent<MeshFilter>().mesh;
    mesh.Clear(); // Clear any existing mesh data
    
    // Set the mesh data
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.uv = uvs;
    
    GetComponent<MeshRenderer>().material.color = Color.green;
    // Recalculate the normals for proper lighting
    mesh.RecalculateNormals();
    mesh.RecalculateBounds();
}
  
    
   
}
