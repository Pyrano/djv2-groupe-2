using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
   public float speed = 3f;
   public float detectionSpeed = 1f;
   public float aggroTime = 5f;
   public float attackRange = 2f;
   public float attackSpeed = 3f;
   public float attackDamage = 10f;
}
