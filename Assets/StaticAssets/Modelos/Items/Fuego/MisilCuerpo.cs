using UnityEngine;

public class MisilCuerpo : MonoBehaviour
{


    private Animator animator;


    // Use this for initialization
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("MisilExplota");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

}
