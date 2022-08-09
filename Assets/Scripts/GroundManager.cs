using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundManager : MonoBehaviour
{
    [SerializeField] private GameObject[] grounds;

    [SerializeField] private GameObject player;
    const int nbGrounds = 9;
    private int[] sortedGround = new int[nbGrounds] {5,8,4,3,6,5,6,1,2};
    private int activeGround;

    [SerializeField] private Text timeText;

    [SerializeField] private StoreScore storeScore;

    [SerializeField] private DiffScore diffScore;

    private bool gameDone = false;
    private float gameTime = 0f;

    private Body body;

    // Start is called before the first frame update
    void Start()
    {
        activeGround = 0;
        Ground currentGround = grounds[sortedGround[activeGround]].GetComponent<Ground>();
        currentGround.activate();
        diffScore.Reset();
        PlayerPrefs.SetFloat("startTime", Time.time);
        body = player.GetComponent<Body>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameDone) {
            gameTime = roundFloat(Time.time - PlayerPrefs.GetFloat("startTime"), 3);
            timeText.text = gameTime.ToString() + "s";
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKeyDown("r")) {
            PlayerPrefs.SetFloat("startTime", Time.time);
            Ground currentGround = grounds[sortedGround[activeGround]].GetComponent<Ground>();
            currentGround.deactivate();
            activeGround = 0;
            currentGround = grounds[sortedGround[activeGround]].GetComponent<Ground>();
            currentGround.activate();
            diffScore.Reset();
            body.respawn();
        }
    }

    private float roundFloat(float value, int significantNumber) {
        if (significantNumber < 0) {
            return value;
        }

        float factor = Mathf.Pow(10, significantNumber);
        return Mathf.Round(value*factor)/factor;
    }

    public void changeActiveGround() {
        gameDone = activeGround >= nbGrounds-1;
        diffScore.CompareScore(roundFloat(Time.time - PlayerPrefs.GetFloat("startTime"), 3));
        if (gameDone) {
            finalizeGame();
            return;
        } else {
            activeGround++;
            grounds[sortedGround[activeGround]].GetComponent<Ground>().activate();
        }
    }

    private void finalizeGame() {
        Debug.Log("Setting final score : " + gameTime.ToString() + 's');
        storeScore.SetScore(gameTime);
        diffScore.PublishNewBestScore();
    }
}
