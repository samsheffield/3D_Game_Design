using UnityEngine;
using System.Collections;

public class BlendTreeControl : MonoBehaviour {

    private Animator anim;
    public float speed = .5f;
    public float rotSpeed = 30;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        float deltaZ = Input.GetAxis("Vertical");
        float deltaX = Input.GetAxis("Horizontal");

        anim.SetFloat("vert", deltaZ, 0.2f, Time.deltaTime);
        transform.position += transform.forward * speed * deltaZ * Time.deltaTime;
        transform.Rotate(new Vector3(0, deltaX * rotSpeed * Time.deltaTime, 0));
    }
}
