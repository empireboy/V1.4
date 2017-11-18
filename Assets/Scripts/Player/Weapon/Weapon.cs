using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private InputManager inputManager;
    private AudioSource _AudioSource;
    public Transform shootPoint;
    public ParticleSystem muzzleFlash;
    public AudioClip shootSound;
    public GameObject hitParticles;
    public GameObject bulletImpact;
    private Vector3 originalPosition;
    public Vector3 aimPosition;
    private PlayerMovement _pm;
    private PlayerRunning _pr;
    private PlayerCrouching _pc;
    private bool isReloading = false;

    [SerializeField]
    private float reloadTime;

    private float reloadTimer;

    public bool aiming = false;

    public float range = 100f;
    public int bulletsPerMag = 30;
    public int bulletsLeft = 200;
    public int currentBullets;

    public float fireRate = 0.1f;
    public bool semi = false;
    public float shakeMin;
    public float shakeMax;
    public float adsSpeed = 8f;

    float fireTimer;

	void Start () {
        if (!(inputManager = this.GetComponent<InputManager>()))
        {
            inputManager = this.gameObject.AddComponent<InputManager>();
        }
        _AudioSource = GetComponent<AudioSource>();
        originalPosition = transform.localPosition;
        _pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _pr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRunning>();
        _pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCrouching>();
        reloadTimer = reloadTime * 60;
    }
	
	void Update () {
        if (!semi)
        {
            if (inputManager.Shoot() && !inputManager.Run() || (inputManager.Run() && aiming && inputManager.Shoot()))
            {
                if (currentBullets > 0 && !isReloading)
                    Fire();
                else if (bulletsLeft > 0)
                {
                    // Automatic reloading
                    //Reload();
                }
            }
        }
        else
        {
            if (inputManager.ShootSemi() && !inputManager.Run() || (inputManager.Run() && aiming && inputManager.ShootSemi()))
            {
                if (currentBullets > 0 && !isReloading)
                    Fire();
                else if (bulletsLeft > 0)
                {
                    // Automatic reloading
                    //Reload();
                }
            }
        }
        
        if (inputManager.Reload())
        {
            if (currentBullets < bulletsPerMag && bulletsLeft > 0 && !isReloading)
            {
                if (_pm.movementSpeed != _pr.fastMovementSpeed)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);
                    isReloading = true;
                }
            }
        }

        if (fireTimer < fireRate) fireTimer += Time.deltaTime;

        AimDownSights();

        if (isReloading)
        {
            reloadTimer -= 1;
            if (reloadTimer <= 0)
            {
                Reload();
                isReloading = false;
                reloadTimer = reloadTime * 60;
            }
        }
    }

    private void AimDownSights()
    {
        if (inputManager.Aim())
        {
            if (!isReloading)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * adsSpeed);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * adsSpeed / 3);
            }
        aiming = true;
            _pm.movementSpeed = _pc.slowMovementSpeed;
        }
        else
        {
            if (!isReloading)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * adsSpeed);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * adsSpeed/3);
            }
            aiming = false;
        }
    }

    private void Fire()
    {
        if (fireTimer < fireRate || currentBullets <= 0) return;

        RaycastHit hit;

        // Shake
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.Rotate(0, Random.Range(Mathf.Lerp(-shakeMin/2, 0, 0.01f), Mathf.Lerp(shakeMax/2, 0, 0.01f)), 0);
        Camera.main.gameObject.transform.Rotate(Random.Range(Mathf.Lerp(-shakeMin, 0, 0.01f), Mathf.Lerp(shakeMax, 0, 0.01f)), 0, 0);

        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            /*if (hit.transform.tag == "Head")
            {
                hit.transform.GetComponent<EnemyHealth>().hp -= 5;
                hit.transform.GetComponent<EnemyHealth>().hit = true;
            }
            else */if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyHealth>().hp--;
                hit.transform.GetComponent<EnemyHealth>().hit = true;
            }
            else
            {
                GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

                Destroy(hitParticleEffect, 1f);
                Destroy(bulletHole, 2f);
            }
        }

        StartCoroutine(DoQuery());
        muzzleFlash.Play();
        PlayShootSound();
        currentBullets--;
        fireTimer = 0.0f;
    }

    private void Reload()
    {
        if (bulletsLeft <= 0) return;

        int bulletsToLoad = bulletsPerMag - currentBullets;
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;
        isReloading = false;
    }

    private void PlayShootSound()
    {
        _AudioSource.PlayOneShot(shootSound);
        //_AudioSource.clip = shootSound;
        //_AudioSource.Play();
    }

    IEnumerator DoQuery()
    {
        float t_x = transform.position.x;
        float t_y = transform.position.y;
        float t_z = transform.position.z;
        WWW request = new WWW("http://22198.hosts.ma-cloud.nl/bewijzenmap/p2.1/gpr/database/database.php?t_x="+t_x+"&t_y="+t_y+"&t_z="+t_z+"&p_id=1");
        yield return request;
    }
}
