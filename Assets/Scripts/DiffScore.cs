using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiffScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diffScoreText;

    private List<float> bestScores;

    private List<float> currentScores;

    private int currentScoreIndex;

    // Start is called before the first frame update
    void Start()
    {
        bestScores = new List<float>{};
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompareScore(float score) {
        currentScores.Add(score);
        currentScoreIndex = currentScores.Count - 1;
        if (bestScores.Count - 1 < currentScoreIndex) {
            return;
        }

        float diff = currentScores[currentScoreIndex] - bestScores[currentScoreIndex];
        if (diff < 0) {
            diffScoreText.text = "- " + (-diff).ToString();
            diffScoreText.color = Color.green;
        } else {
            diffScoreText.text = "+ " + diff.ToString();
            diffScoreText.color = Color.red;
        }
    }

    public void Reset() {
        currentScores = new List<float>{};
        diffScoreText.text = "";
        currentScoreIndex = -1;
    }

    public void PublishNewBestScore() {
        if (bestScores.Count == 0 || bestScores[bestScores.Count - 1] > currentScores[currentScores.Count - 1]) {
            bestScores = currentScores;
        }
    }
}
