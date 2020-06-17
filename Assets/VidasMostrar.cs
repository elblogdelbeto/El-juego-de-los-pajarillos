using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VidasMostrar : MonoBehaviour
{

	TextMeshProUGUI textoVidas;
	Jugador jugador;

	// Use this for initialization
	void Start()
	{
		textoVidas = GetComponent<TextMeshProUGUI>();
		jugador = FindObjectOfType<Jugador>();
	}

	// Update is called once per frame
	void Update()
	{
		if (textoVidas.text != jugador.ConsultarVidas().ToString())
		{
			textoVidas.text = jugador.ConsultarVidas().ToString();
		}
	}
}
