using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerShoot : MonoBehaviour
{
    public enum AttackType
    {
        HalfLife = 0,
        Splatoon = 1
    }

    public AttackType type = AttackType.Splatoon;
    public string fireButton; 
    public Camera playerCamera;            
    private float canSeeDistance = 10f;    
    public LayerMask canSeeLayer;          
    public Rigidbody projectile;           
    public float weaponCooldown = .25f;    
    public float projectileVelocity = 50f; 
    public float projectileLifespan = 1f;  
    public AudioClip shootClip;

    private Transform weapon;              
    private bool readyToShoot = true;      
    private RaycastHit hit;                
    private bool targetActive;                                                                                                                                                                                                                                                                                                                                                                                                                                              
    private AudioSource audioSource;

    void Start()
    {
        weapon = transform.Find("Weapon").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 centerPoint = new Vector3(playerCamera.pixelWidth/2, playerCamera.pixelHeight/2, 0);
        Debug.Log(centerPoint);
        Ray ray = playerCamera.ScreenPointToRay(centerPoint);
        Debug.DrawRay(ray.origin, ray.direction * canSeeDistance, Color.red);
        targetActive = Physics.Raycast(ray, out hit, canSeeDistance, canSeeLayer);

        if(targetActive)
        {
            // DO SOMETHING WHEN HIT
        }

        if (Input.GetButtonDown(fireButton))
        {
            if (readyToShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        audioSource.PlayOneShot(shootClip);
        readyToShoot = false;

        if (type == AttackType.HalfLife)
        {
            if (targetActive)
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
        else if(type == AttackType.Splatoon)
        {
            Rigidbody p = Instantiate(projectile, weapon.position, weapon.rotation) as Rigidbody;
            p.transform.LookAt(playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, canSeeDistance)));
            p.velocity = p.transform.forward * projectileVelocity;
            Destroy(p.gameObject, projectileLifespan);
        }

        yield return new WaitForSeconds(weaponCooldown);
        readyToShoot = true;
    }
}
