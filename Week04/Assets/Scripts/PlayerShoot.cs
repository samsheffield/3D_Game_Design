// Basic shooting controls. 2 modes: Half-Life style (instant shot), Splatoon (projectile-based)
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerShoot : MonoBehaviour
{
    public enum AttackType
    {
        HalfLife = 0,
        Splatoon = 1
    }

    public AttackType type = AttackType.HalfLife;   // Default attack type. Set in the Inspector.

    private Camera playerCamera;            // Camera GameObject attached to the player
    private float canSeeDistance = 10f;     // How far can the player see? Affects Half-Life style shooting by requiring the player to be in closer proximity to the target
    public LayerMask canSeeLayer;           // Which layers contain GameObjects that will work with Raycasting
    public Rigidbody projectile;            // Projectile prefab with Rigidbody. Drag into the Inspector.
    public float weaponCooldown = .25f;     // Rate to limit firing
    public float projectileVelocity = 50f;  // Velocity to apply to cloned projectile prefabs
    public float projectileLifespan = 1f;   // How long to keep the cloned prefabs alive if they don't collide with anything
    public AudioClip shootClip;

    private Transform weapon;               // Transform of the child GameObject attached to the player. A projectile will be fired from this in Splatoon mode
    private bool readyToShoot = true;        // Has the weapon cooled down yet?
    private RaycastHit hit;                 // RaycastHit will store information on anything the ray collides with.
    private bool targetActive; 
    private AudioSource audioSource;

    void Start()
    {
        // There is only one Camera attached to the player, so GetComponentChildren<T>() can be used to quickly find it.
        playerCamera = GetComponentInChildren<Camera>();
        
        // The weapon is a child GameObject called "Weapon". Name must match exactly. (Another option would be to set it up as public and drag it in via the Inspector)
        // Only the transform is needed
        weapon = transform.Find("Weapon").transform;

        // Get the attached AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // The Camera's pixelWidth and pixelHeight variables are used to get find the center of the window. 
        // The weapon will shoot towards this point. Reticle UI can be centered in the window for easier targeting.
        Vector3 centerPoint = new Vector3(playerCamera.pixelWidth/2, playerCamera.pixelHeight/2, 0);

        // Contert the 2D coordinates of the screen to world space and draw a ray from the camera toward it. This is used for aiming and seeing.
        Ray ray = playerCamera.ScreenPointToRay(centerPoint);

        // Debug.DrawRay and Debug.DrawLine can both be used to draw the ray in the scene view at runtime. Good for debugging.
        Debug.DrawRay(ray.origin, ray.direction * canSeeDistance, Color.red);


        // Test if the cast ray collides with anything on the canSeeLayer. Rays can be cast out at infinite lengths, so canSeeDistance limits the range of sight. 
        // Limiting range and specifying layers are useful for performance.
        targetActive = Physics.Raycast(ray, out hit, canSeeDistance, canSeeLayer);
        if(targetActive)
        {
            //Debug.Log("Name: " + hit.collider.gameObject.name);     // To access hit's GameObject, you need to use .collider.gameObject. hit is not a GameObject.
            //Debug.Log("Collision point: " + hit.point);             // hit.point stores a Vector3 of the point where the ray made contact

            // Try to access a ChangeColor component (custom script) attached to something hit.
            ChangeColor changeColor = hit.collider.gameObject.GetComponent<ChangeColor>();

            // If the component exists, set it's public variable, change, to true. ColorChange will handle the rest.
            if(changeColor != null)
            {
                changeColor.change = true;
            }
        }

        // Firing weapon
        if (Input.GetButtonDown("Fire1"))
        {
            // Has cooldown period expired?
            if (readyToShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        audioSource.PlayOneShot(shootClip);

        // Can't shoot until this becomes true again
        readyToShoot = false;

        if (type == AttackType.HalfLife)
        {
            // If the raycast hit something
            if (targetActive)
            {
                // Deactivate it
                hit.collider.gameObject.SetActive(false);
            }
        }
        else if(type == AttackType.Splatoon)
        {
            // Instantiating as a Rigidbody will allow us to skip GetComponent<Rigidbody>() step
            Rigidbody p = Instantiate(projectile, weapon.position, weapon.rotation) as Rigidbody;

            // Rotate the projectile to face the middle of the window at a set distance
            p.transform.LookAt(playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, canSeeDistance)));

            // Move the projectile forwards at a set velocity
            p.velocity = p.transform.forward * projectileVelocity;

            // Destroy the projectile's GameObject after a brief period
            Destroy(p.gameObject, projectileLifespan);
        }

        // Cooldown in effect
        yield return new WaitForSeconds(weaponCooldown);

        // After cooldown, shooting is reset
        readyToShoot = true;
    }
}
