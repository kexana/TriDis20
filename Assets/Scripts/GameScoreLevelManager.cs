using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScoreLevelManager : MonoBehaviour
{
    private static int currentScore=0;
    public static int currentLevel=1;
    public static Dictionary<string, int> scoringTable = new Dictionary<string, int>() {
        { "1line", 100 },
        { "2lines", 300},
        { "3lines", 500},
        { "tetris", 800},
        { "spin", 1600},
        { "clear", 5000}
    };
    public TextCounterUtility scoreCounterUtility;
    public TextCounterUtility lineCounterUtility;
    public TextCounterUtility finalScoreCounterUtility;
    public Text Level;
    public Text LinesCleared;
    public int CurrentScore
    {
        get { return currentScore; }
        set
        {
            scoreCounterUtility.Value = value;
            currentScore = value;
        }
    }
    private void Start()
    {
        Level.text = currentLevel.ToString();
        currentLevel = 1;
        currentScore = 0;
    }
    bool trig = false;
    public void ManageLevel()
    {
        if (Matrixgridcontroller.LineToClearHeights.Count>0)
        {
            if (Matrixgridcontroller.totalClearLines>= currentLevel * 10)
            {
                if (currentLevel % 2 == 0)
                {
                    Matrixgridcontroller.movementSpeed -= 0.1f;
                }
                currentLevel += 1;
                Level.text = currentLevel.ToString();
            }
            lineCounterUtility.Value += Matrixgridcontroller.LineToClearHeights.Count;
        }
        if (Matrixgridcontroller.gameEnded && !trig) {
            finalScoreCounterUtility.Value += CurrentScore;
            trig = true;
        }
    }
}
