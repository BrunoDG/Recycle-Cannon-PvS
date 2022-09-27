using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CannonControl : MonoBehaviour
{
    private CharacterController _cannonControl;
    
    [Header("Controllers")]
    public Joystick joystick;
    public Button cannonShoot;

    [Header("Attributes")]
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public Material Organic;
    public Material Plastic;
    public Material Metal;

    [Header("Unity Setup Fields")]
    public Transform rotationPart;
    public Transform target;
    private Vector3 shotDirection;

    public GameObject bulletPrefab;

    public Transform firePoint;

    private void Start()
    {
        _cannonControl = GetComponent<CharacterController>();
        cannonShoot.onClick.AddListener(Shoot);
    }

    private void Update()
    {
        CannonMove();
    }

    void CannonMove()
    {
        if (joystick.Horizontal >= .2f)
        {
            Quaternion rot = Quaternion.AngleAxis(2, Vector3.up);
            transform.Rotate(rot.eulerAngles);
        }
        else if (joystick.Horizontal <= -.2f)
        {
            Quaternion rot = Quaternion.AngleAxis(-2, Vector3.up);
            transform.Rotate(rot.eulerAngles);
        }

        if (fireCountdown > 0f)
        {
            fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (PlayerStats.TotalAmmo < 3)
        {
            Debug.LogError("Sem munição! Colete mais lixo para poder atirar!");
        }
        if (fireCountdown <= 0f && PlayerStats.TotalAmmo <= 5 && PlayerStats.TotalAmmo >= 3)
        {
            for (int i = 0; i < PlayerStats.TotalAmmo; i++)
            {
                StartCoroutine(SpawnBullet());
            }
            PlayerStats.TotalAmmo = 0;
            
            fireCountdown = 1f / fireRate;
        }
    }

    IEnumerator SpawnBullet()
    {
        switch (PlayerStats.AmmoType)
        {
            case "Organic":
                bulletPrefab.GetComponent<Renderer>().material = (Material)Instantiate(Organic);
                break;
            case "Plastic":
                bulletPrefab.GetComponent<Renderer>().material = (Material)Instantiate(Plastic);
                break;
            case "Metal":
                bulletPrefab.GetComponent<Renderer>().material = (Material)Instantiate(Metal);
                break;
        }
        bulletPrefab.tag = PlayerStats.AmmoType;
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        shotDirection = target.position;
        if (bullet != null)
        {
            bullet.Seek(shotDirection, target);
        }
        yield return new WaitForSeconds(0.2f);
    }
}
