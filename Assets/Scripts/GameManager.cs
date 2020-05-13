using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Posibles estados del videojuego
public enum GameState {
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour {

    //Variable que referencia al propio GameManager
    public static GameManager sharedInstance;

    //Variable que nos indica en que estado del juego nos encontramos
    public GameState currentGameState = GameState.menu;
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;
    public int collectedObjects = 0;

    private void Awake() {
        sharedInstance = this;
    }

    void Start() {
        BackToMenu();
    }

    void Update() {
        
        if(Input.GetButtonDown("Start") && currentGameState != GameState.inGame) {
            StartGame();
        }

        if(Input.GetButtonDown("Menu")) {
            BackToMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            ExitGame();
        }
    }

    //Función encargada de iniciar el juego
    public void StartGame() {
        SetGameState(GameState.inGame);
        PlayerController.sharedInstance.StartGame();
    }

    //Función que se llama al morir
    public void GameOver() {
        SetGameState(GameState.gameOver);
    }

    //Función para volver al menú principal cuando el usuario quiera
    public void BackToMenu() {
        SetGameState(GameState.menu);
    }

     public void ExitGame()
    {
        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }

    //Función encargada de cambiar el estado del juego
    void SetGameState(GameState newGameState) {

        if(newGameState == GameState.menu){

            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;

        } else if(newGameState == GameState.inGame){

            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;


        } else if(newGameState == GameState.gameOver){

            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;

        }

        //Asignamos el estado del juego actual al que nos ha llegado por parámetro
        this.currentGameState = newGameState;

    }

    public void CollectObject(int objectValue){
        this.collectedObjects += objectValue;
    }

}
