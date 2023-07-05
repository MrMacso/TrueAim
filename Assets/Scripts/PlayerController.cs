using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    public UIBarScript ui;
    public Animator animator;
    private JoystickHandler bluetooth;
    public GameObject loseScene;

    public float buttonFire;
    public float buttonReload;
    public float xJoystick;
    public float yJoystick;
    private bool isControllerActive = false;

    readonly float speed = 12f;

    readonly int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        bluetooth = FindObjectOfType<JoystickHandler>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        ui.UpdateValue(currentHealth, maxHealth);
    }

    void Update()
    {
        buttonFire = bluetooth.shoot;
        buttonReload = bluetooth.reload;
        xJoystick = bluetooth.joystickX;
        yJoystick = bluetooth.joystickY;

        Vector3 move;
        if (isControllerActive)
        {
            //controller movement
            if (xJoystick < 0.3f && xJoystick > -0.3f && yJoystick < 0.3f && yJoystick > -0.3f)
            {
                //standing state
                move = new(0f, 0f, 0f);
                animator.SetBool("isWalking", false);
            }
            else
            {
                //moving state
                move =   transform.forward * yJoystick + transform.right * xJoystick;
                animator.SetBool("isWalking", true);
            }
            controller.Move(move * speed * Time.deltaTime);
            
        }
        else
        {
            //keyboard control testing purpose
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            move = transform.right * x + transform.up * 0 + transform.forward * z;
            controller.Move(speed * Time.deltaTime * move);
        }
    }
    public void SetIsControllerActive(bool isActive)
    {
        isControllerActive = isActive;
    }
    public void RecieveDamage(int damage)
    {
        currentHealth -= damage;
        ui.UpdateValue(currentHealth, maxHealth);

        if (currentHealth <= 0)
            LoseGame();
    }
    private void LoseGame()
    {
        loseScene.SetActive(true);
        Time.timeScale = 0;
    }
}
