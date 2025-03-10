using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatonicSolidsMeshGeneration : MonoBehaviour
{
    private void Start()
    {
        //GenerateCube();
        //GenerateCubeWithProperUVs();
        //GenerateTetrahedron();
        GenerateOctahedron();
    }

    private void GenerateCube()
    {
        Vector3[] vertices =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1,0, 0),
            
            new Vector3(0, 0, 1),
            new Vector3(0, 1, 1),
            new Vector3(1, 1, 1),
            new Vector3(1, 0, 1)
        };
        
        int [] triangles =
        {
            // Front face
            0, 1, 2,
            0, 2, 3,
        
            // Back face
            7, 6, 5,
            7, 5, 4,
        
            // Top face
            1, 5, 6,
            1, 6, 2,
        
            // Bottom face
            0, 3, 7,
            0, 7, 4,
        
            // Left face
            4, 5, 1,
            4, 1, 0,
        
            // Right face
            3, 2, 6,
            3, 6, 7
        };
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        GetComponent<MeshRenderer>().material.color = Color.cyan;
        mesh.RecalculateBounds();
    }
    
    private void GenerateCubeWithProperUVs()
        {
        Vector3[] vertices = new Vector3[24];
        Vector2[] uvs = new Vector2[24];

        // Front face
        vertices[0] = new Vector3(0, 0, 0); // bottom-left
        vertices[1] = new Vector3(0, 1, 0); // top-left
        vertices[2] = new Vector3(1, 1, 0); // top-right
        vertices[3] = new Vector3(1, 0, 0); // bottom-right

        // Back face
        vertices[4] = new Vector3(1, 0, 1); // bottom-left
        vertices[5] = new Vector3(1, 1, 1); // top-left
        vertices[6] = new Vector3(0, 1, 1); // top-right
        vertices[7] = new Vector3(0, 0, 1); // bottom-right

        // Top face
        vertices[8] = new Vector3(0, 1, 0); // bottom-left
        vertices[9] = new Vector3(0, 1, 1); // top-left
        vertices[10] = new Vector3(1, 1, 1); // top-right
        vertices[11] = new Vector3(1, 1, 0); // bottom-right

        // Bottom face
        vertices[12] = new Vector3(0, 0, 1); // bottom-left
        vertices[13] = new Vector3(0, 0, 0); // top-left
        vertices[14] = new Vector3(1, 0, 0); // top-right
        vertices[15] = new Vector3(1, 0, 1); // bottom-right

        // Left face
        vertices[16] = new Vector3(0, 0, 1); // bottom-left
        vertices[17] = new Vector3(0, 1, 1); // top-left
        vertices[18] = new Vector3(0, 1, 0); // top-right
        vertices[19] = new Vector3(0, 0, 0); // bottom-right

        // Right face
        vertices[20] = new Vector3(1, 0, 0); // bottom-left
        vertices[21] = new Vector3(1, 1, 0); // top-left
        vertices[22] = new Vector3(1, 1, 1); // top-right
        vertices[23] = new Vector3(1, 0, 1); // bottom-right

        // Apply the same UV coordinates for each face
        // This gives each face the full texture
        for (int i = 0; i < 6; i++)
        {
            uvs[i * 4 + 0] = new Vector2(0, 0); // bottom-left
            uvs[i * 4 + 1] = new Vector2(0, 1); // top-left
            uvs[i * 4 + 2] = new Vector2(1, 1); // top-right
            uvs[i * 4 + 3] = new Vector2(1, 0); // bottom-right
        }

        // Each face consists of 2 triangles, so 12 triangles total
        int[] triangles = new int[36];

        // Set up triangles for all 6 faces
        for (int i = 0; i < 6; i++)
        {
            int baseIndex = i * 4;
            int triangleIndex = i * 6;
            
            // First triangle of face (counter-clockwise)
            triangles[triangleIndex + 0] = baseIndex + 0;
            triangles[triangleIndex + 1] = baseIndex + 1;
            triangles[triangleIndex + 2] = baseIndex + 2;
            
            // Second triangle of face (counter-clockwise)
            triangles[triangleIndex + 3] = baseIndex + 0;
            triangles[triangleIndex + 4] = baseIndex + 2;
            triangles[triangleIndex + 5] = baseIndex + 3;
        }

        // Get and clear the mesh
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        // Assign vertices, triangles, and UVs
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        GetComponent<MeshRenderer>().material.color = Color.cyan;

        // Recalculate normals for proper lighting
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        }
    
    private void GenerateTetrahedron()
    {
        // Define the 4 vertices of the tetrahedron
        Vector3[] vertices = 
        {
            new Vector3(0, 0, 0),       // 0: Base - front right
            new Vector3(-1, 0, -1),     // 1: Base - back left
            new Vector3(1, 0, -1),      // 2: Base - back right
            new Vector3(0, 1.5f, -0.5f) // 3: Top point
        };
    
        // Define the triangles for all 4 faces
        int[] triangles = 
        {
            // Base (counter-clockwise when viewed from below)
            0, 2, 1,
        
            // Front face (counter-clockwise)
            0, 1, 3,
        
            // Right face (counter-clockwise)
            0, 3, 2,
        
            // Back face (counter-clockwise)
            1, 2, 3
        };
    
        // Define UV coordinates for each vertex
        Vector2[] uvs = new Vector2[4];
    
        // Simple UV mapping (triangular layout)
        uvs[0] = new Vector2(0.5f, 0);    // Bottom middle
        uvs[1] = new Vector2(0, 0);       // Bottom left
        uvs[2] = new Vector2(1, 0);       // Bottom right
        uvs[3] = new Vector2(0.5f, 1);    // Top middle
    
        // Get and clear the mesh
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
    
        // Assign vertices, triangles, and UVs
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
    
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        // Recalculate normals for proper lighting
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
    
    private void GenerateOctahedron()
{
    // For proper UV mapping, we'll define each triangular face separately
    // 8 faces with 3 vertices each = 24 total vertices
    Vector3[] vertices = new Vector3[24];
    Vector2[] uvs = new Vector2[24];
    
    // Define the 6 corner points of the octahedron
    Vector3 top = new Vector3(0, 1, 0);        // Top vertex
    Vector3 bottom = new Vector3(0, -1, 0);    // Bottom vertex
    Vector3 front = new Vector3(0, 0, 1);      // Front vertex
    Vector3 back = new Vector3(0, 0, -1);      // Back vertex
    Vector3 right = new Vector3(1, 0, 0);      // Right vertex
    Vector3 left = new Vector3(-1, 0, 0);      // Left vertex
    
    // Top faces (4 triangles)
    // Top-Front-Right face
    vertices[0] = top;
    vertices[1] = front;
    vertices[2] = right;
    
    // Top-Right-Back face
    vertices[3] = top;
    vertices[4] = right;
    vertices[5] = back;
    
    // Top-Back-Left face
    vertices[6] = top;
    vertices[7] = back;
    vertices[8] = left;
    
    // Top-Left-Front face
    vertices[9] = top;
    vertices[10] = left;
    vertices[11] = front;
    
    // Bottom faces (4 triangles)
    // Bottom-Front-Left face
    vertices[12] = bottom;
    vertices[13] = front;
    vertices[14] = left;
    
    // Bottom-Left-Back face
    vertices[15] = bottom;
    vertices[16] = left;
    vertices[17] = back;
    
    // Bottom-Back-Right face
    vertices[18] = bottom;
    vertices[19] = back;
    vertices[20] = right;
    
    // Bottom-Right-Front face
    vertices[21] = bottom;
    vertices[22] = right;
    vertices[23] = front;
    
    // Apply UV coordinates for each triangular face
    for (int i = 0; i < 8; i++)
    {
        // Triangle UVs - simple equilateral triangle mapping
        uvs[i * 3 + 0] = new Vector2(0.5f, 0);   // Top/Bottom vertex
        uvs[i * 3 + 1] = new Vector2(0, 1);      // Left edge vertex
        uvs[i * 3 + 2] = new Vector2(1, 1);      // Right edge vertex
    }
    
    // Define the triangles - since we've defined vertices face by face,
    // the triangles array is simply sequential indices
    int[] triangles = new int[24];
    for (int i = 0; i < 24; i++)
    {
        triangles[i] = i;
    }
    
    // Get and clear the mesh
    Mesh mesh = GetComponent<MeshFilter>().mesh;
    mesh.Clear();
    
    // Assign vertices, triangles, and UVs
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.uv = uvs;
    GetComponent<MeshRenderer>().material.color = Color.green;
    // Recalculate normals for proper lighting
    mesh.RecalculateNormals();
    mesh.RecalculateBounds();
}
}
