using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour {

    // Use this for initialization
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Hit");
    }
}
