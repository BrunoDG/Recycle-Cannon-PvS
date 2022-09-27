using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonControl : MonoBehaviour
{
    private CharacterController _cannonControl;
    public Joystick joystick;
    public Transform rotationPart;
    public Button cannonShoot;

    private float horizontalInput;

    [SerializeField] private float _cannonSpeed = 1f;

    [Header("Munição do Canhão")]
    [SerializeField] private int metalAmmo;
    [SerializeField] private int plasticAmmo;
    [SerializeField] private int organicAmmo;

    private void Start()
    {
        _cannonControl = GetComponent<CharacterController>();
        cannonShoot.onClick.AddListener(Shoot);
        metalAmmo = 0;
        plasticAmmo = 0;
        organicAmmo = 0;
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
            horizontalInput = 0f;
        }

        Vector3 movementInput = new Vector3(horizontalInput, 0, 0);
        Vector3 movementDirection = movementInput.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        Vector3 rotation = Quaternion.Lerp(rotationPart.rotation, lookRotation, Time.deltaTime * _cannonSpeed).eulerAngles;
        rotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        Debug.Log("SHOT IN THE DARK");
    }

    public void LoadAmmo(int ammo, string type)
    {

    }

}
