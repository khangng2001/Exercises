using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HalfCircleMeshGenerator : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private int segments = 32;
    [SerializeField] private bool fillCenter = true;
    [SerializeField] private Material material;

    private void Start()
    {
        GenerateHalfCircleMesh();
    }

    public void GenerateHalfCircleMesh()
    {
        // Get required components
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        
        // Create new mesh
        Mesh mesh = new Mesh();
        
        // Calculate vertices count
        int verticesCount = segments + 1; // +1 for the center point
        
        // Create arrays for mesh data
        Vector3[] vertices = new Vector3[verticesCount];
        Vector2[] uv = new Vector2[verticesCount];
        int[] triangles;
        
        // Set center vertex (at local coordinates origin)
        vertices[0] = Vector3.zero;
        uv[0] = new Vector2(0.5f, 0.5f); // Center of the UV space
        
        // Calculate angle step
        float angleStep = Mathf.PI / (segments - 1);
        
        // Generate vertices along the half circle
        for (int i = 0; i < segments; i++)
        {
            // Calculate the angle for this vertex (from 0 to π)
            float angle = i * angleStep;
            
            // Calculate position using parametric equation
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            
            // Set vertex position
            vertices[i + 1] = new Vector3(x, y, 0);
            
            // Calculate UV coordinates (map from world position to 0-1 space)
            // Map x from [-radius, radius] to [0, 1]
            // Map y from [0, radius] to [0, 1]
            uv[i + 1] = new Vector2(x / (2 * radius) + 0.5f, y / radius);
        }
        
        // Create triangles (face indices)
        if (fillCenter)
        {
            // We need segments triangles, each with 3 vertices
            triangles = new int[segments * 3];
            
            // Each triangle connects the center point with two adjacent vertices on the arc
            for (int i = 0; i < segments - 1; i++)
            {
                int baseIndex = i * 3;
                triangles[baseIndex] = 0;  // Center vertex
                triangles[baseIndex + 1] = i + 1;
                triangles[baseIndex + 2] = i + 2;
            }
            
            // Complete the last triangle
            int lastTriangleBase = (segments - 1) * 3;
            triangles[lastTriangleBase] = 0;  // Center vertex
            triangles[lastTriangleBase + 1] = segments;
            triangles[lastTriangleBase + 2] = 1;
        }
        else
        {
            // If not filling the center, create a line strip
            triangles = new int[(segments - 1) * 2];
            for (int i = 0; i < segments - 1; i++)
            {
                triangles[i * 2] = i + 1;
                triangles[i * 2 + 1] = i + 2;
            }
        }
        
        // Assign data to mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        
        // Recalculate mesh properties
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        // Assign mesh to mesh filter
        meshFilter.mesh = mesh;
        
        // Assign material
    }
}