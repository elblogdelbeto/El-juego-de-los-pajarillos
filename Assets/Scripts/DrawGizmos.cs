using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour {

    enum DibujarGizmo
    {
        siempre,
        seleccionado
    }

    [SerializeField]
    private DibujarGizmo dibujarGizmos;

    private void Start()
    {
        dibujarGizmos = new DibujarGizmo();
    }

    private void OnDrawGizmos()
    {
        if (dibujarGizmos == DibujarGizmo.siempre)
            Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z));
    }

    private void OnDrawGizmosSelected()
    {
        if (dibujarGizmos == DibujarGizmo.seleccionado)
            Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z));
    }

}
