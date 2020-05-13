using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niveles : MonoBehaviour {

  
    public Sprite nivelDesbloquedo;                
    public Sprite nivelBloqueado;              
    public Sprite unaEstrella;
    public Sprite dosEstrellas;
    public Sprite tresEstrellas;

    public List<NivelGUI> listaNiveles = new List<NivelGUI>();

    void Awake () {

        GameManager.dificultad = Dificultad.normal;

        GameManager.SalvarPartida();
        GameManager.CargarPartida();


        foreach (NivelGUI niv in listaNiveles)
        {
            Nivel nivel = GameManager.progresoNiveles.Find(n => n.idNivel == niv.idNivel);
            if(nivel != null)
            {
                niv.desbloqueado = nivel.desbloqueado;
                switch (GameManager.dificultad)
                {
                    case Dificultad.superfacil:
                        niv.estrellas = nivel.estrellasSuperFacil;
                        break;
                    case Dificultad.facil:
                        niv.estrellas = nivel.estrellasFacil;
                        break;
                    case Dificultad.normal:
                        niv.estrellas = nivel.estrellasNormal;
                        break;
                    case Dificultad.dificil:
                        niv.estrellas = nivel.estrellasDificil;
                        break;
                    default:
                        niv.estrellas = nivel.estrellasDificil;
                        break;                        
                }
                
            }            
        }


        for (int i = 0; i < listaNiveles.Count; i++)
        {
            //draw stats depends on how much stars we gained;
            if (listaNiveles[i].desbloqueado)
            {
                listaNiveles[i].objetoNivel.sprite = nivelDesbloquedo;
                //listaNiveles[i].objetoNivel.gameObject.SetActive(true);

                if (listaNiveles[i].estrellas == 1) listaNiveles[i].objetoEstrellas.sprite = unaEstrella;
                if (listaNiveles[i].estrellas == 2) listaNiveles[i].objetoEstrellas.sprite = dosEstrellas;
                if (listaNiveles[i].estrellas == 3) listaNiveles[i].objetoEstrellas.sprite = tresEstrellas;
            }
            else
            {
                listaNiveles[i].objetoNivel.sprite = nivelBloqueado;
                //listaNiveles[i].objetoNivel.gameObject.SetActive(false);
            }
    
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}



//Level class;
[System.Serializable]
public class NivelGUI
{
    public SpriteRenderer objetoNivel;          
    public SpriteRenderer objetoEstrellas;
    public int idNivel;      
    public bool desbloqueado;
    public int estrellas;
  
}
