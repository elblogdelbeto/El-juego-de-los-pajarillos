using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorParticulas : MonoBehaviour {

    private ParticleSystem particulas;

	// Use this for initialization
	void Start () {

        particulas = GetComponent<ParticleSystem>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!particulas.IsAlive())
        {
            Destroy(gameObject);
        }
	}
}
