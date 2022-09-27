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

    private List<GameObject> containers = new List<GameObject>();

    [Header("Trash Collected")]
    [SerializeField] private string fuelType = "";
    [SerializeField] private int totalFuel;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        pickOrDeposit.onClick.AddListener(PickOrDepositTapped);
        totalFuel = 0;
    }

    private void Update()
    {
        Movement();
    }

    public void TakeDamage()
    {
        LevelUI life = GetComponent<LevelUI>();

        PlayerStats.Lives--;
        life.DamagePlayer(1);
        if (PlayerStats.Lives <= 0)
        {
            Die();
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(5f, 0, 5f), _playerSpeed * Time.deltaTime);
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
        if (other.gameObject.tag == "Organic" ||
            other.gameObject.tag == "Plastic" ||
            other.gameObject.tag == "Metallic" )
        {
            Debug.Log("Lixo pode ser coletado!");
            pickItem = true;
        }
        else if (other.gameObject.name == "OrganicContainer" ||
                 other.gameObject.name == "PlasticContainer" ||
                 other.gameObject.name == "MetallicContainer" )
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


        if(pickItem)
        {
            if (fuelType == "")
            {
                fuelType = type;
            }

            if (type.Equals(fuelType))
            {
                Debug.Log($"{fuelType} Trash Collected");
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

            Debug.Log($"{fuelType} Trash Fueled on the Right Place");
            PlayerStats.AmmoType = "Organic";
            PlayerStats.TotalAmmo = totalFuel;
            totalFuel = 0;
            fuelType = "";
        }

        actionPressed = false;

        if (totalFuel == 5)
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
