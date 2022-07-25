using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreScore : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI scoreBoardText;

    private List<float> currentScores;

    private int numberOfScore = 5;



    // Start is called before the first frame update
    void Start() {
        currentScores = new List<float>{};
        for (int i = 0; i < numberOfScore; i++) {
            currentScores.Add(float.MaxValue);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetScore(float score){
        int index = getFirstIndexGreater(score);
        if (index >= 0) {
            currentScores.Insert(index,score);
            currentScores.RemoveAt(currentScores.Count - 1);
            scoreBoardText.text = toString();
        }
    }

    private int getFirstIndexGreater(float score) {
        for (int i = 0; i < currentScores.Count; i++) {
            if (score < currentScores[i]) {
                return i;
            }
        }
        return -1;
    }

    private string toString() {
        string text = "";
        foreach (float score in currentScores) {
            if (score == float.MaxValue) {
                return text;
            }
            text += score.ToString() + "s\n";
        }
        return text;
    }
}
