using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimiento
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;
    public CharacterController player;

    //Velocidad
    public float playerSpeed;

    //Almacenar valores de camara
    private Vector3 movePlayer;

    
    public bool isOnSlope = false; //si no esta en una rampa
    private Vector3 hitnormal;//direccion del vector donde esta apuntando
    public float slideVelocity;
    public float slopeForceDown;//fuerza hacia a bajo
    

    //Gravedad 
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    //Camara
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;


    // Start is called before the first frame update
    void Start()
    {
        //Movimiento
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        //Mover camara con el jugador
        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        //Hacer que el jugador vea hacia donde se esta moviendo
        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerSkills();

        //Se mueve el jugador
        player.Move(movePlayer * Time.deltaTime);

        Debug.Log(player.velocity.magnitude);
    
    }
    //Funcion para determinar la direccion a la que mira la camara.
    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    //Funcion para las habilidades de nuestro jugador
    public void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }

    //Funcion para la gravedad.
   void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        SlideDown();
    }
    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitnormal) >= player.slopeLimit;

        if(isOnSlope)
        {
            movePlayer.x += ((1f - hitnormal.y) * hitnormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitnormal.y) * hitnormal.z) * slideVelocity;

            movePlayer.y += slopeForceDown;
        } 
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) //OnControllerColliderHit detecta cuando tocamos algo como un objeto
    {
        hitnormal = hit.normal;
    }
}
