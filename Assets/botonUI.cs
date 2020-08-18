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
            Debug.Log(this.gameObject.name + " was selected");
            cursor.transform.position = new Vector2(cursor.transform.position.x, this.transform.position.y);
        }
    }

    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " entro en OnSelect");
    }
}
