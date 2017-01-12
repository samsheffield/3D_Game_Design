using UnityEngine;
using System.Collections;

public class PlayerController3PF : MonoBehaviour
{
    public string horizontalAxis;
    public string verticalAxis;
    public string jumpButton;
    public float moveSpeed = 12f;
    public float rotSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float jumpScaler = 5f;     
    public float fallRate = -1.5f;
    public float gravity = -9.8f;
    private CharacterController cc;
    private float ySpeed;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis(horizontalAxis) * moveSpeed;
        float deltaZ = Input.GetAxis(verticalAxis) * moveSpeed;

        Vector3 movement = new Vector3(0, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, moveSpeed);

        if (cc.isGrounded)
        {
            if (Input.GetButtonDown(jumpButton))
            {
                ySpeed = jumpSpeed;
            }
            else
            {
                ySpeed = fallRate;
            }
        }
        else
        {
            ySpeed += gravity * jumpScaler * Time.deltaTime;

            if (ySpeed < gravity)
            {
                ySpeed = gravity;
            }
        }

        // TODO : Rotate when moving
        transform.Rotate(new Vector3(0, deltaX * rotSpeed * Time.deltaTime, 0));
        movement.y = ySpeed;                              
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        cc.Move(movement);

    }
}
