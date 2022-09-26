using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController _controller;
    public Joystick joystick;

    [SerializeField]
    private float _playerSpeed = 20f;

    [SerializeField]
    private float _rotationSpeed = 10f;

    private float horizontalInput;
    private float verticalInput;

    [Header("Trash Collected")]
    [SerializeField] private int organicAmmo;
    [SerializeField] private int metalAmmo;
    [SerializeField] private int plasticAmmo;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (joystick.Horizontal >= .2f)
        {
            horizontalInput = _playerSpeed;
        } else if (joystick.Horizontal <= -.2f)
        {
            horizontalInput = -_playerSpeed;
        } else
        {
            horizontalInput = 0f;
        }

        if (joystick.Vertical >= .2f)
        {
            verticalInput = _playerSpeed;
        }
        else if (joystick.Vertical <= -.2f)
        {
            verticalInput = -_playerSpeed;
        }
        else
        {
            verticalInput = 0f;
        }

        Vector3 movementInput = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }

        _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "OrganicTrash":
                Debug.Log("Organic Trash Collected");
                organicAmmo++;
                break;
            case "PlasticTrash":
                Debug.Log("Plastic Trash Collected");
                plasticAmmo++;
                break;
            case "MetallicTrash":
                Debug.Log("Metallic Trash Collected");
                metalAmmo++;
                break;
            default:
                break;
        }
        other.gameObject.SetActive(false);
        Destroy(other.gameObject);
    }
}
