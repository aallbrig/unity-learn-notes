using UnityEngine;

public class InvertNormals : MonoBehaviour
{
    private void Start()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        var normals = mesh.normals;
        for (var i = 0; i < normals.Length; i++)
            normals[i] = -1 * normals[i];

        mesh.normals = normals;

        for (var i = 0; i < mesh.subMeshCount; i++)
        {
            var tris = mesh.GetTriangles(i);
            for (var j = 0; j < tris.Length; j += 3)
            {
                var temp = tris[j];
                tris[j] = tris[j + 1];
                tris[j + 1] = temp;
            }
            mesh.SetTriangles(tris, i);
        }
    }
}
