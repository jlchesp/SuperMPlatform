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

    //Función encargada de cambiar el estado del juego
    void SetGameState(GameState newGameState) {

        if(newGameState == GameState.menu){

        } else if(newGameState == GameState.inGame){

        } else if(newGameState == GameState.gameOver){

        }

        //Asignamos el estado del juego actual al que nos ha llegado por parámetro
        this.currentGameState = newGameState;

    }

}
