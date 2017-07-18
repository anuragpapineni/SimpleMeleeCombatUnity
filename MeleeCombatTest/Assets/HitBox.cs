using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : Box {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        HurtBox hurtbox;
        if (other.GetComponent<HurtBox>())
        {
            hurtbox = other.GetComponent<HurtBox>();
            if (hurtbox.owner != owner)
            {
                Debug.Log(other.name);
            }
        }
    }
}
