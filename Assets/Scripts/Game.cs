using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : Singleton<Game>
{
   public List<Transform> alarms;
   public List<AiAgent> enemies;
   public GameObject player;
   public Vector3 triggeredAlarm;

   private void Start()
   {
      enemies = FindObjectsOfType<AiAgent>().ToList();
   }

   public Vector3 GetClosestAlarm(Vector3 targetPoint)
   {
      Vector3 closestPoint = Vector3.zero;
      float closestDistance = Mathf.Infinity;

      foreach (Transform point in alarms)
      {
         float distance = Vector3.Distance(targetPoint, point.position);

         if (distance < closestDistance)
         {
            closestDistance = distance;
            closestPoint = point.position;
         }
      }

      return closestPoint;
   }

}
