using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerShoot3PF : MonoBehaviour
{
    public string fireButton;
    public Rigidbody projectile;
    public float weaponCooldown = .25f;
    public float projectileVelocity = 50f;
    public float projectileLifespan = 1f;
    public AudioClip shootClip;

    private Transform weapon;
    private bool readyToShoot = true;
    private AudioSource audioSource;

    void Start()
    {
        weapon = transform.Find("Weapon").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
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

        // TODO: Add

        yield return new WaitForSeconds(weaponCooldown);
        readyToShoot = true;
    }
}
