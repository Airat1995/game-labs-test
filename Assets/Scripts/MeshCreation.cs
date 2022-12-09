using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshCreation : MonoBehaviour
{
    [SerializeField]
    private int _widthSize = 0;

    [SerializeField]
    private int _heightSize = 0;

    [SerializeField]
    private float _pieceSize;

    [SerializeField]
    private MeshFilter _meshFilter;


    void Start()
    {
        Mesh createdMesh = new Mesh();

        Vector3[] vertices = new Vector3[4 * _widthSize * _heightSize];
        Vector2[] uvs = new Vector2[4 * _widthSize * _heightSize];
        int[] triangles = new int[6 * _widthSize * _heightSize];

        _meshFilter.mesh = createdMesh;

        for (int meshWidthIndex = 0; meshWidthIndex < _widthSize; meshWidthIndex++)
        {
            for (int meshHeightIndex = 0; meshHeightIndex < _heightSize; meshHeightIndex++)
            {
                int index = meshWidthIndex * _heightSize + meshHeightIndex;
                
                vertices[index * 4]     = new Vector3(_pieceSize * meshWidthIndex, _pieceSize * meshHeightIndex);
                vertices[index * 4 + 1] = new Vector3(_pieceSize * meshWidthIndex, (meshHeightIndex + 1) * _pieceSize);
                vertices[index * 4 + 2] = new Vector3((1 + meshWidthIndex) * _pieceSize, (meshHeightIndex + 1) * _pieceSize);
                vertices[index * 4 + 3] = new Vector3((1 + meshWidthIndex) * _pieceSize, _pieceSize * meshHeightIndex);

                uvs[index * 4] = new Vector2(vertices[index * 4].x/(_widthSize * _pieceSize), vertices[index * 4].y/(_heightSize * _pieceSize));
                uvs[index * 4 + 1] = new Vector2(vertices[index * 4 + 1].x/(_widthSize * _pieceSize), vertices[index * 4 + 1].y/(_heightSize * _pieceSize));
                uvs[index * 4 + 2] = new Vector2(vertices[index * 4 + 2].x/(_widthSize * _pieceSize), vertices[index * 4 + 2].y/(_heightSize * _pieceSize));
                uvs[index * 4 + 3] = new Vector2(vertices[index * 4 + 3].x/(_widthSize * _pieceSize), vertices[index * 4 + 3].y/(_heightSize * _pieceSize));

                // uvs[index * 4 + 1] = new Vector2(_pieceSize * meshWidthIndex, (meshHeightIndex + 1) * _pieceSize);
                // uvs[index * 4 + 2] = new Vector2((1 + meshWidthIndex) * _pieceSize, (meshHeightIndex + 1) * _pieceSize);
                // uvs[index * 4 + 3] = new Vector2((1 + meshWidthIndex) * _pieceSize, _pieceSize * meshHeightIndex);

                triangles[index * 6] = index * 4;
                triangles[index * 6 + 1] = index * 4 + 1;
                triangles[index * 6 + 2] = index * 4 + 2;
                triangles[index * 6 + 3] = index * 4;
                triangles[index * 6 + 4] = index * 4 + 2;
                triangles[index * 6 + 5] = index * 4 + 3;
            }
        }

        createdMesh.vertices = vertices;
        createdMesh.uv = uvs;
        createdMesh.triangles = triangles;

        Debug.Log(createdMesh.subMeshCount);
    }
}
