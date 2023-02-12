using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int totalAmmo = 200;
    public int maxAmmo = 30;
    public int ammo = 30;
    public int damage = 1;
    public float reloadTime = 2f;
    private float lastReloadTime;
    public bool isReloading = false;

    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float spread = 2f;
    private Camera camera;
    public float shootingDelay = 0.5f;
    public ParticleSystem muzzleFlash;
    public Animator animator;
    public float cameraShakeIntensity = 0.5f;
    public float cameraShakeDuration = 0.1f;
    private CameraShake cameraShake;
    private GunLoader gunLoader;

    private float lastShotTime;

    private void Start()
    {
        gunLoader = transform.parent.GetComponent<GunLoader>();
        totalAmmo = gunLoader.ammo;
        if (totalAmmo < maxAmmo)
            ammo = totalAmmo;
        camera = FindObjectsOfType<Camera>()[0];
        cameraShake = camera.GetComponent<CameraShake>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammo < maxAmmo && totalAmmo > 0)
        {
            animator.SetTrigger("reload");
            isReloading = true;
            lastReloadTime = Time.time;
        }
        if (isReloading && Time.time - lastReloadTime >= reloadTime)
        {
            if(totalAmmo >= maxAmmo)
            {
                ammo = maxAmmo;
            }
            else
            {
                ammo = totalAmmo;
            }
            gunLoader.ammo = totalAmmo; 
            isReloading = false;
        }

        Vector3 direction = camera.transform.forward.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;

        if (Input.GetMouseButton(0) && Time.time - lastShotTime >= shootingDelay)
        {


            if (totalAmmo > 0 && ammo > 0)
            {
                Shoot();
                totalAmmo--;
                ammo--;
            }
        }
    }

    void Shoot()
    {
        cameraShake.shakeDuration = 0.1f;
        animator.SetTrigger("shoot");
        Vector3 spreadDirection = Random.onUnitSphere * spread;
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0) + spreadDirection);
            RaycastHit hit;
            Vector3 bulletDirection = Vector3.zero;
            Physics.Raycast(ray, out hit);
            if (Physics.Raycast(ray, out hit))
                bulletDirection = (hit.point - bulletSpawnPoint.position).normalized;
            else
                bulletDirection = camera.transform.forward.normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(bulletDirection));
            bullet.GetComponent<BulletScript>().damage = damage;
            bullet.GetComponent<Rigidbody>().velocity = bulletDirection * bulletSpeed; //
            lastShotTime = Time.time;
            Destroy(bullet, 3f);
            muzzleFlash.Play();
    }
}
