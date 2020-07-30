using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PajaroLateral : Enemigo
{    
    private Animator animator;
    
    // Awake is called when the script instance is being loaded
    private new void Awake()
    {
        base.Awake();       
        animator = transform.gameObject.GetComponentInChildren<Animator>();  
    }

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        AsignarVelocidadMov();
    }

    // METODOS ----------------------------------------------------------------------------------------------------------
    public void AsignarVelocidadMov()
    {
        if (animator)
        {
            if(velocidad > 1)
              animator.SetFloat("VelocidadMov", 1.0f + (velocidad*0.2f));            
        }
    }





}
