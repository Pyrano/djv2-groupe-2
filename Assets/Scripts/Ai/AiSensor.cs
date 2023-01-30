using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSensor : MonoBehaviour
{
   public float distance = 10f;
   public float angle = 30;
   public float height = 1f;
   public Color meshColor = Color.red;

   private Mesh _mesh;

   private Mesh CreatewedgeMesh()
   {
      Mesh mesh = new Mesh();

      int numTraiangles = 8;
      int numVertices = numTraiangles * 3;

      Vector3[] vertices = new Vector3[numVertices];
      int[] triangles = new int[numVertices];

      Vector3 bottomCenter = Vector3.zero;
      Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
      Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

      Vector3 topCenter = bottomCenter + Vector3.up * height;
      Vector3 topRight = bottomRight + Vector3.up * height;
      Vector3 topLeft = bottomLeft + Vector3.up * height;
      
      return mesh;
   }
}
