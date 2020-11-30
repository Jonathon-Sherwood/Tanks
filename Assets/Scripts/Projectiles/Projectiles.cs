using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [HideInInspector]public GameObject spawnOrigin; //Given a value by the ship that instantiates this
    [HideInInspector]public float damage; //Pulls from the ship that instantiates this
}
