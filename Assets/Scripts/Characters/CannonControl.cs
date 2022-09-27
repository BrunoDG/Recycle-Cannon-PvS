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

    private float horizontalInput;

    [Header("Unity Setup Fields")]
    public Transform rotationPart;
    public Transform target;
    private Vector3 shotDirection;

    public GameObject bulletPrefab;
    
    [SerializeField] private float _cannonSpeed = 1f;

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
            horizontalInput = _cannonSpeed;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            horizontalInput = -_cannonSpeed;
        }
        else
        {
            horizontalInput = shotDirection.x;
        }

        Vector3 movementInput = new Vector3(horizontalInput, 0, 0);
        Vector3 movementDirection = movementInput.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        Vector3 rotation = Quaternion.Lerp(rotationPart.rotation, lookRotation, Time.deltaTime * _cannonSpeed).eulerAngles;
        rotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        if (fireCountdown <= 0f)
        {
            switch(PlayerStats.AmmoType)
            {
                case "Organic":
                    bulletPrefab.GetComponent<Renderer>().material = (Material)Instantiate(Organic);
                    break;
                case "Plastic":
                    bulletPrefab.GetComponent<Renderer>().material = (Material)Instantiate(Plastic);
                    break;
                case "Metallic":
                    bulletPrefab.GetComponent<Renderer>().material = (Material)Instantiate(Metal);
                    break;
            }
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.bulletType = PlayerStats.AmmoType;
            shotDirection = target.position;
            if (bullet != null)
            {
                bullet.Seek(shotDirection, target);
            }
            PlayerStats.TotalAmmo--;
            
            fireCountdown = 1f / fireRate;
        }
    }
}
