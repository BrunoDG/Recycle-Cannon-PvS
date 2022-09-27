using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    public Button pickOrDeposit;
    private bool pickItem = false;
    private bool actionPressed = false;

    [Header("Life counter")]
    public int lives;

    private List<GameObject> lifePanels = new List<GameObject>();

    [Header("Trash Collected")]
    [SerializeField] private int organicFuel;
    [SerializeField] private int metalFuel;
    [SerializeField] private int plasticFuel;
    [SerializeField] private int totalFuel;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        lifePanels.AddRange(GameObject.FindGameObjectsWithTag("LifeScavenger"));
        PlayerStats.Lives = lifePanels.Count();
        pickOrDeposit.onClick.AddListener(PickOrDepositTapped);
        totalFuel = 0;
    }

    private void Update()
    {
        Movement();
    }

    public void TakeDamage()
    {
        PlayerStats.Lives--;
        Destroy(lifePanels[PlayerStats.Lives-1]);
        if (PlayerStats.Lives <= 0)
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

    void PickOrDepositTapped()
    {
        actionPressed = true;
    }

    void OnTriggerStay(Collider other)
    {    
        if(actionPressed)
        {
            PickAndDeposit(other.gameObject.tag, "Pick", other);
            return;
        }
    }

    void PickAndDeposit(string type, string action, Collider other)
    {
        if (action == "Pick" && totalFuel == 5)
        {
            Debug.Log("Você está cheio, coloque o lixo no container e volte depois!");
            return;
        }

        switch (type)
        {
            case "OrganicTrash":
                if (pickItem)
                {
                    if (plasticFuel==0 && metalFuel==0)
                    {
                        Debug.Log("Organic Trash Collected");
                        organicFuel++;
                        totalFuel++;
                        other.gameObject.SetActive(false);
                        Destroy(other.gameObject);
                    } else
                    {
                        Debug.Log("Não pode coletar tipos de lixo diferentes, só um por vez!");
                        return;
                    }
                } else
                {
                    Debug.Log("Organic Trash Fueled on the Right Place");
                    PlayerStats.AmmoType = "Organic";
                    PlayerStats.TotalAmmo = organicFuel;
                    totalFuel -= organicFuel;
                    organicFuel = 0;
                }
                break;
            case "PlasticTrash":
                if (pickItem)
                {
                    if (organicFuel == 0 && metalFuel == 0)
                    {
                        Debug.Log("Plastic Trash Collected");
                        plasticFuel++;
                        totalFuel++;
                        other.gameObject.SetActive(false);
                        Destroy(other.gameObject);
                    } else
                    {
                        Debug.Log("Não pode coletar tipos de lixo diferentes, só um por vez!");
                        return;
                    }
                } else
                {
                    Debug.Log("Plastic Trash Fueled on the Right Place");
                    PlayerStats.AmmoType = "Plastic";
                    PlayerStats.TotalAmmo = plasticFuel;
                    totalFuel -= plasticFuel;
                    plasticFuel = 0;
                }
                break;
            case "MetallicTrash":
                if (pickItem)
                {
                    if (plasticFuel == 0 && organicFuel == 0)
                    {
                        Debug.Log("Metallic Trash Collected");
                        metalFuel++;
                        totalFuel++;
                        other.gameObject.SetActive(false);
                        Destroy(other.gameObject);
                    }else
                    {
                        Debug.Log("Não pode coletar tipos de lixo diferentes, só um por vez!");
                        return;
                    }
                } else
                {
                    Debug.Log("Metallic Trash Fueled on the Right Place");
                    PlayerStats.AmmoType = "Metallic";
                    PlayerStats.TotalAmmo = metalFuel;
                    totalFuel -= metalFuel;
                    metalFuel = 0;
                }
                break;
        }

        actionPressed = false;

        if (totalFuel == 6)
        {
            Debug.Log("You're full! Unload all trash on a container to pick more fuel!");
            return;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
