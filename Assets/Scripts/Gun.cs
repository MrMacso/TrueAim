using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 10000f;

    public int currentAmmo;
    readonly int maxAmmo = 30;
    readonly float reloadTime = 1f;

    public RaycastHit hit;
    public TextMeshProUGUI magazineText;
    public AudioManager audioManager;
    public GameObject impactFlash;
    public ParticleSystem muzzleFlash;
    readonly float fireRate = 15f;

    private Camera fpsCam;
    private float nextTimeToFire = 0.0f;
    private Animator animator;
    private PlayerController playerController;
    private float buttonFire;
    private float buttonReload;

    private void Start()
    {
        //sets up the starting value of the ammo and the string of the ammo
        currentAmmo = maxAmmo;
        magazineText.text = currentAmmo.ToString("0") + "/30";
        fpsCam = this.GetComponent<Camera>();
        animator = GetComponentInChildren<Animator>();
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        buttonFire = playerController.buttonFire;
        buttonReload = playerController.buttonReload;
        //updates the ammos text
        magazineText.text = currentAmmo.ToString("0") + "/30";
        if(Input.GetButtonDown("Fire1") || buttonFire > 0)
        {
            animator.SetTrigger("Shoot");
            Shoot();
        }
        if(Input.GetKey(KeyCode.R) || buttonReload > 0)
        {
            animator.SetTrigger("Reload");
            StartCoroutine(Reload());
        }
    }
    public void Shoot()
    {
        //checks the size of the ammo
        if (Time.time >= nextTimeToFire && currentAmmo != 0)
        {
            muzzleFlash.Play();
            //checks raycast
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Enemy target = hit.transform.GetComponent<Enemy>();

                //if the ray finds a target deals damage to it
                if (target != null)
                {
                    target.TakeDamage(10);
                }
                //shoot sound
                audioManager.Play("shot");
                //reduce from ammo
                currentAmmo--;
                //impact flash
                Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                currentAmmo--;
                audioManager.Play("shot");
            }
        }
        else if (Time.time >= nextTimeToFire && currentAmmo == 0)
        {
            //if ammo is 0 plays an empty magazine sound each time the player tries to shoot
            audioManager.Play("empty");
        }
    }
    IEnumerator Reload()
    {
        //realoads the magazine sets the value back to max and plays a sound
        audioManager.Play("reload");
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
    }
}
