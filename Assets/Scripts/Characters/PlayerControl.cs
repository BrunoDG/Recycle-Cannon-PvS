using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
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
    private bool pickItem = false;

    [Header("Life counter")]
    public int lives;

    private List<GameObject> lifePanels = new List<GameObject>();

    [Header("Trash Collected")]
    [SerializeField] private int organicAmmo;
    [SerializeField] private int metalAmmo;
    [SerializeField] private int plasticAmmo;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        lifePanels.AddRange(GameObject.FindGameObjectsWithTag("LifeScavenger"));
        lives = lifePanels.Count();
    }

    private void Update()
    {
        Movement();
    }

    public void TakeDamage()
    {
        lives--;
        Destroy(lifePanels[lives-1]);
        if (lives == 0)
        {
            Die();
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OrganicTrash" ||
            other.gameObject.tag == "PlasticTrash" ||
            other.gameObject.tag == "MetallicTrash" )
        {
            Debug.Log("Lixo pode ser coletado!");
            pickItem = true;
        }
        else if (other.gameObject.tag == "OrganicContainer" ||
                 other.gameObject.tag == "PlasticContainer" ||
                 other.gameObject.tag == "MetallicContainer" )
        {
            Debug.Log("Pode depositar para munição!");
        }
    }

    void OnTriggerStay(Collider other)
    {
        
        PickAndDeposit(other.gameObject.tag, "Pick");
        other.gameObject.SetActive(false);
        Destroy(other.gameObject);
    }

    void PickAndDeposit(string type, string action)
    {
        switch (type)
        {
            case "OrganicTrash":
                if (pickItem)
                {
                    Debug.Log("Organic Trash Collected");
                    organicAmmo++;
                } else
                {
                    Debug.Log("Organic Trash Fueled on the Right Place");
                    
                    organicAmmo = 0;
                }
                break;
            case "PlasticTrash":
                if (pickItem)
                {
                    Debug.Log("Plastic Trash Collected");
                    plasticAmmo++;
                }
                break;
            case "MetallicTrash":
                Debug.Log("Metallic Trash Collected");
                metalAmmo++;
                break;
        }
    }

    void Die()
    {
        Destroy(gameObject);
        GameManager.GameOver(0);
    }
}
