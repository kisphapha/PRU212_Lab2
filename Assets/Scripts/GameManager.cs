using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject player;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject hiScoreUITextGO;
    public GameObject TimeCounterGO;
    public enum GameManagerState
    {
        GamePlay,
        GameOver,
        Opening,
    }
    GameManagerState state;
    // Start is called before the first frame update
    void Start()
    {
        //state = GameManagerState.Opening;
        StartGameplay();
    }

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch (state)
        {
            case GameManagerState.GamePlay:
                //hide the play button
                playButton.SetActive(false);

                //hide the quit button
                quitButton.SetActive(false);

                //hide gameover
                GameOverGO.SetActive(false);
             
                //Set player firerate
                //player.GetComponent<PlayerControl>().FireRate = 2f;

                //Reset the score 
                scoreUITextGO.GetComponent<GameScore>().Score = 0;


                //Get hight score
                scoreUITextGO.GetComponent<GameScore>().HiScore =  PlayerPrefs.GetInt("High Score");


                //set the player visible (active) and init the player lives
                //player.GetComponent<ShipMovement>().Init();

                //start the time counter 
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                break;
            case GameManagerState.GameOver:

                if(scoreUITextGO.GetComponent<GameScore>().HiScore < scoreUITextGO.GetComponent<GameScore>().Score)
                {
                    scoreUITextGO.GetComponent<GameScore>().HiScore = scoreUITextGO.GetComponent<GameScore>().Score;
                }

                PlayerPrefs.SetInt("High Score", scoreUITextGO.GetComponent<GameScore>().HiScore);
                //Stop the time counter
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                //Display game over 
                GameOverGO.SetActive(true);

                //Change game manager state 
                Invoke("ChangeToOpeningState", 4f);



                break;
            case GameManagerState.Opening:


                scoreUITextGO.GetComponent<GameScore>().HiScore =  PlayerPrefs.GetInt("High Score");

                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                //Hide game over 
                GameOverGO.SetActive(false);

                //set play button visible (active)
                playButton.SetActive(true);

                //set exit button
                quitButton.SetActive(true);
                break;
        }
    }
    //Function to set the game manager state 
    public void SetGameManagerState(GameManagerState states)
    {
        state = states;
        UpdateGameManagerState();
    }
    public void StartGameplay()
    {
        state = GameManagerState.GamePlay;
        UpdateGameManagerState();
    }
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        SceneManager.LoadSceneAsync(0);
        UnityEngine.Debug.Log("Quit success");
    }
}
