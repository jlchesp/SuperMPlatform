using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController sharedInstance;
    public float jumpForce = 5f;
    public LayerMask groundLayer; //Esta variable sirve para detectar la capa del suelo
    public Animator animator; //Esta variable sirve para llamar al componente de animaciones
    public float runningSpeed = 1.5f;
    private Rigidbody2D rigidBody; //Esta variable llama al componente Rigidbody2D del personaje
    private Vector3 startPosition;

    //Funcioón para configurar todas las variables antes de empezar el juego
    void Awake() {
        sharedInstance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position; //Toma el valor de donde empieza nuestro personaje la primera vez
    }

    // Start is called before the first frame update
    public void StartGame(){
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        this.transform.position = startPosition; //Cada vez que reiniciamos, ponemos al personaje en la start position
    }

    // Update is called once per frame
    void Update(){

        //Solo saltamos si estamos en el modo inGame
        if(GameManager.sharedInstance.currentGameState == GameState.inGame){

            //Condicional para comprobar si la tecla espacio está pulsada
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
                Jump(); //Llamada a la función de salto
            }

            //Asignamos a la animación el mismo valor que el método IsTouchingTheGround que devuelve parámetros booleanos
            animator.SetBool("isGrounded", IsTouchingTheGround());
        }
    }

    //Función para aplicar fuerzas constantes en el juego para evitar que nos afecten las caídas de FPS
    void FixedUpdate() {

        //Solo nos movemos si estamos en el modo inGame
        if(GameManager.sharedInstance.currentGameState == GameState.inGame){

            //Con estos condicionales manejamos las teclas para el movimieno del personaje
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){

                if(rigidBody.velocity.x < runningSpeed) {
                    rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
                }
            }

            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){

                if(rigidBody.velocity.x > -runningSpeed) {
                    rigidBody.velocity = new Vector2(-runningSpeed, rigidBody.velocity.y);
                }
        
            }
        }
    }

    //Función que devuelve true si estamos tocando el suelo y false en otro caso
    bool IsTouchingTheGround(){
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer)){
            return true;
        } else {
            return false;
        }
    }

    //Función que se encarga de aplicarle una fuerza de salto al personaje
    void Jump(){
        //F = m * a -------> a = F/m
        if(IsTouchingTheGround()){
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    //Funcioón que se encarga de llamar al estado gameOver y cambiar la animación por la de muerte
    public void Kill() {
        GameManager.sharedInstance.GameOver();
        this.animator.SetBool("isAlive", false);
    }

}
