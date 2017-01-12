using UnityEngine;
using System.Collections;

public class LayerControl : MonoBehaviour {

    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {

        }
        else if (Input.GetButtonUp("Fire1"))
        {

        }

        if (Input.GetButtonDown("Fire2"))
        {

        }
    }
}
