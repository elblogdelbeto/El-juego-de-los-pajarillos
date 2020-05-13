using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionEnemiga : MonoBehaviour {

    // Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }



}
