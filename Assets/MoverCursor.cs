using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoverCursor : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Button botonArriba;
    [SerializeField] Button botonAbajo;
    Button ultimoBotonSeleccionado;
    [SerializeField] Button volumenSonidoBoton;
    [SerializeField] Slider volumenSonidoSlider;


    private float tiempo = 0.0F;
    private float tiempoSiguiente = 0.3F;


    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;

        if (EventSystem.current.currentSelectedGameObject)
            if(EventSystem.current.currentSelectedGameObject.GetComponent<Button>())
                ultimoBotonSeleccionado = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        if (Input.GetMouseButtonDown(0))
        {
            if (ultimoBotonSeleccionado)
                ultimoBotonSeleccionado.Select();
        }
        
        if(EventSystem.current.currentSelectedGameObject == volumenSonidoBoton.gameObject)
        {
            int subirVolumen;

            if ((Input.GetButton("Horizontal") && tiempo > tiempoSiguiente))
            {
                tiempoSiguiente = tiempo + 0.3f;

                subirVolumen = (int)Input.GetAxisRaw("Horizontal");

                if (subirVolumen != 0)
                {
                    volumenSonidoSlider.value += subirVolumen*2;
                }
            }
            

        }
        
        
    }
}
