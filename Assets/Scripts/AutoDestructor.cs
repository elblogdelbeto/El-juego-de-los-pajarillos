using UnityEngine;

public class AutoDestructor : MonoBehaviour
{

    private void Start()
    {
        //mostrar en log que objeto lo tiene adjunto
        //print(this.gameObject.name);
    }



    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0)
            Destroy(gameObject);
    }
}
