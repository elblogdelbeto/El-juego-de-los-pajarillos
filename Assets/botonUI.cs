using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class botonUI : MonoBehaviour
{

    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Compare selected gameObject with referenced Button gameObject
        if (EventSystem.current.currentSelectedGameObject == this.gameObject && (int)cursor.transform.position.y != (int)this.transform.position.y)
        {
            RectTransform rec = GetComponent<RectTransform>();
            Vector3[] v = new Vector3[4];
            rec.GetWorldCorners(v);
            float CursorPosicionX = v[1].x;
            cursor.transform.position = new Vector2(CursorPosicionX, this.transform.position.y);
        }
    }


}
