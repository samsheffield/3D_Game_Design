using UnityEngine;
using System.Collections;

public class RootMotionControl : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("vert", Input.GetAxis("Vertical"), 0.1f, Time.deltaTime);
        anim.SetFloat("hori", Input.GetAxis("Horizontal"), 0.1f, Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("isJumping", true);
        }

    }

    void StopJumping()
    {
        anim.SetBool("isJumping", false);
    }
}
