using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
   public float speed = 3f;
   public float detectionSpeed = 1f;
   public float aggroTime = 5f;
}
