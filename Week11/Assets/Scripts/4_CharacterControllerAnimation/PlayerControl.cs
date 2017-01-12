using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float rotSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float jumpScaler = 5f;
    public float fallRate = -1.5f;
    public float gravity = -9.8f;
    private CharacterController cc;
    private float ySpeed;
    private Animator anim;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // Pass the grounded boolean to the animator
        anim.SetBool("isGrounded", cc.isGrounded);

        anim.SetFloat("vert", Input.GetAxis("Vertical"), 0.1f, Time.deltaTime);
        anim.SetFloat("hori", Input.GetAxis("Horizontal"), 0.1f, Time.deltaTime);

        float deltaX = Input.GetAxis("Horizontal") * moveSpeed;
        float deltaZ = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 movement = new Vector3(0, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, moveSpeed);

        transform.Rotate(new Vector3(0, deltaX * rotSpeed * Time.deltaTime, 0));
        movement.y = ySpeed;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        cc.Move(movement);

    }
}
