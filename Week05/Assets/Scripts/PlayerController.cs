using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform target;
    public string horizontalAxis;
    public string verticalAxis;
    public string jumpButton;
    public float moveSpeed = 12f;
    public float rotSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float jumpScaler = 5f;       // Higher number decreases the time hanging in the air
    public float fallRate = -1.5f;
    public float gravity = -9.8f;
    private CharacterController cc;
    private float ySpeed;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement is based on the current axis position times speed.
        float deltaX = Input.GetAxis(horizontalAxis) * moveSpeed;
        float deltaZ = Input.GetAxis(verticalAxis) * moveSpeed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, moveSpeed);

        // Jumping
        // Check if the CharacterController's is on top of something (isGrounded = true). CharacterController reference: https://docs.unity3d.com/ScriptReference/CharacterController.html
        if (cc.isGrounded)
        {
            // If the player is jumping the y speed is increased, otherwise it will be decreased at a slower rate
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
            // If the CharacterController is not grounded...
            // Gradually add to the y speed, scale it so that it happens faster, and make it framerate independednt. 
            ySpeed += gravity * jumpScaler * Time.deltaTime;

            // Limit the downward speed to gravity
            if (ySpeed < gravity)
            {
                ySpeed = gravity;
            }
        }

        Quaternion tmp = target.rotation;
        target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotSpeed * Time.deltaTime);
        movement.y = ySpeed;                               
        movement *= Time.deltaTime;
        movement = target.TransformDirection(movement);
        target.rotation = tmp;

        cc.Move(movement);


    }
}
