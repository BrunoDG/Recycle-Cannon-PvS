using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonControl : MonoBehaviour
{
    private CharacterController _cannonControl;
    public Joystick joystick;

    private float horizontalInput;

    [SerializeField] private float _cannonSpeed = 10f;

    [Header("Munição do Canhão")]
    [SerializeField] private float metalAmmo;
    [SerializeField] private float plasticAmmo;
    [SerializeField] private float organicAmmo;

    private void Start()
    {
        _cannonControl = GetComponent<CharacterController>();
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

        _cannonControl.Move(movementDirection * _cannonSpeed * Time.deltaTime);
    }


}
