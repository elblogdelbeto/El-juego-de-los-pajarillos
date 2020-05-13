using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NivelEstructura  {
    

    [Serializable]
    public class EnemigoNivel
    {
        public string identificadorEnemigo;
        public float tiempoAparecer;
        public bool reaparecer = false;
        public bool inicioAleatorio = true;
        public Vector2 posicionAparecer;
        public LugarAparecer lugarAparecer = LugarAparecer.derecha;
        public DificultadEnemigo dificultadEnemigo;
        public float velocidad;
        public float saludEnemigo;
        public int puntosEnemigo;
    }

    public string fondo;
    public float velocidadFondo;
    public float duracionTiempo;
    public float porcentajePuntosMediaEstrella;
    public float porcentajePuntosEstrella;
    public float porcentajePunteriaMediaEstrella;
    public float porcentajePunteriaEstrella;
    public float porcentajeSaludMediaEstrella;
    public float porcentajeSaludEstrella;
    public string musica;
    public List<EnemigoNivel> enemigosNivel;


}


public enum LugarAparecer
{
    arriba,
    derecha,
    abajo,
    izquierda,
}

