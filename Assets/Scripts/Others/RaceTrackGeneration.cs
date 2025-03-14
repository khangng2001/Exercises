using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RaceTrackGeneration : MonoBehaviour
{
    [SerializeField] private float straightLength = 20f;   // Length of straight sections
    [SerializeField] private float turnRadius = 5f;        // Radius of semicircular ends
    [SerializeField] private float trackWidth = 2f;        // Width of the track
    [SerializeField] private int segmentsStraight = 30;    // Segments per straight section
    [SerializeField] private int segmentsTurn = 30;        // Segments per semicircle

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshFilter == null || _meshRenderer == null)
        {
            Debug.LogError("MeshFilter or MeshRenderer is missing!");
            return;
        }
        GenerateTrackMesh();
    }

    // Moved helper function outside as a class method
    private void AddVertexPair(Vector3[] vertices, Vector2[] uvs, ref int vertexIndex, int totalSegments, 
                             float xOuter, float zOuter, float xInner, float zInner, float uvX)
    {
        if (vertexIndex >= totalSegments)
        {
            Debug.LogError("Vertex index out of bounds!");
            return;
        }
        vertices[vertexIndex] = new Vector3(xOuter, 0f, zOuter);                // Outer
        vertices[vertexIndex + totalSegments] = new Vector3(xInner, 0f, zInner); // Inner
        uvs[vertexIndex] = new Vector2(uvX, 1f);                                // Outer UV
        uvs[vertexIndex + totalSegments] = new Vector2(uvX, 0f);                // Inner UV
        vertexIndex++;
    }

    private void GenerateTrackMesh()
    {
        int totalSegments = 2 * segmentsStraight + 2 * segmentsTurn;
        Vector3[] vertices = new Vector3[2 * totalSegments];
        Vector2[] uvs = new Vector2[2 * totalSegments];
        int[] triangles = new int[6 * (totalSegments - 1) + 6]; // Adjusted for closing loop

        int vertexIndex = 0;

        // 1. Bottom straight (left to right)
        for (int i = 0; i < segmentsStraight; i++)
        {
            float t = (float)i / (segmentsStraight - 1);
            float x = Mathf.Lerp(-straightLength / 2, straightLength / 2, t);
            float zOuter = -turnRadius - trackWidth / 2;
            float zInner = -turnRadius + trackWidth / 2;
            float uvX = (float)i / (totalSegments - 1);
            AddVertexPair(vertices, uvs, ref vertexIndex, totalSegments, x, zOuter, x, zInner, uvX);
        }

        // 2. Right semicircle (bottom to top)
        for (int i = 0; i < segmentsTurn; i++)
        {
            float t = (float)i / (segmentsTurn - 1);
            float angle = Mathf.Lerp(-Mathf.PI / 2, Mathf.PI / 2, t);
            float cosA = Mathf.Cos(angle);
            float sinA = Mathf.Sin(angle);
            float outerRadius = turnRadius + trackWidth / 2;
            float innerRadius = turnRadius - trackWidth / 2;
            float xOuter = straightLength / 2 + outerRadius * cosA;
            float zOuter = outerRadius * sinA;
            float xInner = straightLength / 2 + innerRadius * cosA;
            float zInner = innerRadius * sinA;
            float uvX = (float)(segmentsStraight + i) / (totalSegments - 1);
            AddVertexPair(vertices, uvs, ref vertexIndex, totalSegments, xOuter, zOuter, xInner, zInner, uvX);
        }

        // 3. Top straight (right to left)
        for (int i = 0; i < segmentsStraight; i++)
        {
            float t = (float)i / (segmentsStraight - 1);
            float x = Mathf.Lerp(straightLength / 2, -straightLength / 2, t);
            float zOuter = turnRadius + trackWidth / 2;
            float zInner = turnRadius - trackWidth / 2;
            float uvX = (float)(segmentsStraight + segmentsTurn + i) / (totalSegments - 1);
            AddVertexPair(vertices, uvs, ref vertexIndex, totalSegments, x, zOuter, x, zInner, uvX);
        }

        // 4. Left semicircle (top to bottom)
        for (int i = 0; i < segmentsTurn; i++)
        {
            float t = (float)i / (segmentsTurn - 1);
            float angle = Mathf.Lerp(Mathf.PI / 2, 3 * Mathf.PI / 2, t);
            float cosA = Mathf.Cos(angle);
            float sinA = Mathf.Sin(angle);
            float outerRadius = turnRadius + trackWidth / 2;
            float innerRadius = turnRadius - trackWidth / 2;
            float xOuter = -straightLength / 2 + outerRadius * cosA;
            float zOuter = outerRadius * sinA;
            float xInner = -straightLength / 2 + innerRadius * cosA;
            float zInner = innerRadius * sinA;
            float uvX = (float)(segmentsStraight + segmentsTurn + segmentsStraight + i) / (totalSegments - 1);
            AddVertexPair(vertices, uvs, ref vertexIndex, totalSegments, xOuter, zOuter, xInner, zInner, uvX);
        }

        // Generate triangles
        int triangleIndex = 0;
        for (int i = 0; i < totalSegments - 1; i++)
        {
            int v0 = i;                        // Outer current
            int v1 = i + totalSegments;        // Inner current
            int v2 = (i + 1) % totalSegments;  // Outer next (wrap around)
            int v3 = (i + 1) % totalSegments + totalSegments; // Inner next (wrap around)

            // Ensure correct winding order (counterclockwise)
            triangles[triangleIndex++] = v0;
            triangles[triangleIndex++] = v1;
            triangles[triangleIndex++] = v2;

            triangles[triangleIndex++] = v1;
            triangles[triangleIndex++] = v3;
            triangles[triangleIndex++] = v2;
        }

        // Create and assign the mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Debug to verify vertex and triangle counts
//        Debug.Log($"Vertices: {vertices.Length}, Triangles: {triangles.Length / 3}");

        if (mesh.vertexCount > 0 && mesh.triangles.Length > 0)
        {
            _meshFilter.mesh = mesh;
            _meshRenderer.material.color = Color.black;
        }
        else
        {
            Debug.LogError("Mesh is invalid! Check vertex or triangle data.");
        }
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            if (_meshFilter == null) _meshFilter = GetComponent<MeshFilter>();
            if (_meshRenderer == null) _meshRenderer = GetComponent<MeshRenderer>();
            GenerateTrackMesh();
        }
    }
}