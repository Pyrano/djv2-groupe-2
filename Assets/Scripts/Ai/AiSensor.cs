using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AiSensor : MonoBehaviour
{
   public float distance = 10f;
   public float angle = 30;
   public float height = 1f;
   public Color meshColor = Color.red;
   public int scanFrequency = 30;
   public LayerMask layers;
   public LayerMask occlusionLayers;
   public MeshFilter viewMeshFilter;
   public Mesh viewMesh;
   public Vector3 viewOffset;
   
   public struct ViewCastInfo
   {
      public bool hit;
      public Vector3 point;
      public float distance;
      public float angle;
      public Vector3 offset;  

      public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle,  Vector3 _offset)
      {
         hit = _hit;
         point = _point;
         distance = _distance;
         angle = _angle;
         offset = _offset;
      }
   }

   public List<GameObject> Objects
   {
      get
      {
         objects.RemoveAll(obj => !obj);
         return objects;
      }
   }
   public List<GameObject> objects = new();

   private Collider[] _colliders = new Collider[50];

   private Mesh _mesh;
   private int count;
   private float scanInterval;
   private float scanTimer;

   private void Start()
   {
      scanInterval = 1f / scanFrequency;
      viewMesh = new Mesh();
      viewMesh.name = "View Mesh";
      viewMeshFilter.mesh = viewMesh;
   }

   private void Update()
   {
      scanTimer -= Time.deltaTime;
      if (scanTimer < 0)
      {
         scanTimer += scanInterval;
         Scan();
      }
   }
   private void LateUpdate()
   {
      drawFieldOfView(viewOffset);
   }

   private void Scan()
   {
      count = Physics.OverlapSphereNonAlloc(transform.position, distance, _colliders,
         layers, QueryTriggerInteraction.Collide);
      objects.Clear();
      for (int i = 0; i < count; ++i)
      {
         GameObject obj = _colliders[i].gameObject;
         if (IsInSight(obj))
         {
            objects.Add(obj);
         }
      }
   }

   private bool IsInSight(GameObject obj)
   {
      var origin = transform.position;
      var dest = obj.transform.position;
      var direction = dest - origin;
      if (direction.y < 0 || direction.y > height)
      {
         return false;
      }

      direction.y = 0;
      var deltaAngle = Vector3.Angle(direction, transform.forward);
      if (deltaAngle > angle)
      {
         return false;
      }

      origin.y += height / 2;
      dest.y = origin.y;
      if (Physics.Linecast(origin, dest, occlusionLayers))
      {
         return false;
      }
      return true;
   }

   private void OnValidate()
   {
      _mesh = CreatewedgeMesh();
      scanInterval = 1f / scanFrequency;
   }

   private void OnDrawGizmos()
   {
      if (_mesh)
      {
         Gizmos.color = meshColor;
         Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
      }

      Gizmos.DrawWireSphere(transform.position, distance);
      for (int i = 0; i < count; i++)
      {
         Gizmos.DrawSphere(_colliders[i].transform.position, 0.2f);
      }
      
      Gizmos.color = Color.green;
      foreach (var obj in objects)
      {
         Gizmos.DrawSphere(obj.transform.position, 0.2f);
      }
   }
   
   private Mesh CreatewedgeMesh()
   {
      Mesh mesh = new Mesh();

      int segments = 10;
      
      int numTraiangles = (segments * 4) + 2 + 2;
      int numVertices = numTraiangles * 3;

      Vector3[] vertices = new Vector3[numVertices];
      int[] triangles = new int[numVertices];

      Vector3 bottomCenter = Vector3.zero;
      Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
      Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

      Vector3 topCenter = bottomCenter + Vector3.up * height;
      Vector3 topRight = bottomRight + Vector3.up * height;
      Vector3 topLeft = bottomLeft + Vector3.up * height;

      int vert = 0;
      
      // left side
      vertices[vert++] = bottomCenter;
      vertices[vert++] = bottomLeft;
      vertices[vert++] = topLeft;

      vertices[vert++] = topLeft;
      vertices[vert++] = topCenter;
      vertices[vert++] = bottomCenter;
      
      // right side
      vertices[vert++] = bottomCenter;
      vertices[vert++] = topCenter;
      vertices[vert++] = topRight;

      vertices[vert++] = topRight;
      vertices[vert++] = bottomRight;
      vertices[vert++] = bottomCenter;

      float currentAngle = -angle;
      float deltaAngle = (angle * 2) / segments;
      for (int i = 0; i < segments; ++i)
      {
         bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
         bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

         topRight = bottomRight + Vector3.up * height;
         topLeft = bottomLeft + Vector3.up * height;
         
         // far side
         vertices[vert++] = bottomRight;
         vertices[vert++] = bottomRight;
         vertices[vert++] = topRight;

         vertices[vert++] = topRight;
         vertices[vert++] = topLeft;
         vertices[vert++] = bottomLeft;
         // top 
         vertices[vert++] = topCenter;
         vertices[vert++] = topLeft;
         vertices[vert++] = topRight;
         // bottom
         vertices[vert++] = bottomCenter;
         vertices[vert++] = bottomRight;
         vertices[vert++] = bottomLeft;
         
         currentAngle += deltaAngle;
      }

      for (int i = 0; i < numVertices; ++i)
      {
         triangles[i] = i;
      }

      mesh.vertices = vertices;
      mesh.triangles = triangles;
      mesh.RecalculateNormals();
      
      return mesh;
   }

   public int Filter(GameObject[] buffer, string layerName)
   {
      int layer = LayerMask.NameToLayer(layerName);
      int count = 0;
      foreach (var obj in objects)
      {
         if (obj.layer == layer)
         {
            buffer[count++] = obj;
         }

         if (buffer.Length == count)
         {
            break;// buffer is full
         }
      }

      return count;
   }

   private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
   {
      if (!angleIsGlobal)
      {
         angleInDegrees += transform.eulerAngles.y;
      }
      return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
   }

   private ViewCastInfo ViewCast(float globalAngle)
   {
      Vector3 dir = DirFromAngle(globalAngle, true);
      RaycastHit hit;

      if (Physics.Raycast(transform.position + viewOffset, dir, out hit, distance, occlusionLayers))
      {
         return new ViewCastInfo(true, hit.point, hit.distance, globalAngle, viewOffset);
      }
      else
      {
         return new ViewCastInfo(false, transform.position + dir * distance, distance, globalAngle, viewOffset);
      }
   }

   private void drawFieldOfView(Vector3 offset){
      int stepCount = Mathf.RoundToInt(angle * 2);
      float stepAngleSize = angle * 2 / stepCount;
      List<Vector3> viewPoints = new List<Vector3>();

      for (int i = 0; i <= stepCount; i++)
      {
         float eAngle = transform.eulerAngles.y - angle + stepAngleSize * i;
         Vector3 dir = DirFromAngle(eAngle, true);
         ViewCastInfo newViewCast = ViewCast(eAngle);
         Vector3 newpoint = newViewCast.point;
         viewPoints.Add(newpoint);
         }

         int vertexCount = viewPoints.Count + 1;
         Vector3[] vertices = new Vector3[vertexCount];
         int[] triangles = new int[(vertexCount - 2) * 3];
         
         vertices[0] = offset;
         for (int i = 0; i < vertexCount - 1; i++)
         {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);            
            
            if (i < vertexCount - 2)
            {
               triangles[i * 3] = 0;
               triangles[i * 3 + 1] = i + 1;
               triangles[i * 3 + 2] = i + 2;
            }
         }

         for (int i = 0; i < vertexCount; i++)
         {
            vertices[i].y = 0;
         }

         viewMesh.Clear();
         viewMesh.vertices = vertices;
         viewMesh.triangles = triangles;
         viewMesh.RecalculateNormals();
   }

}
