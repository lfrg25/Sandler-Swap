using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject backgroundPanel;
    public GameObject victoryPanel;
    public GameObject losePanel;

    public int goal;
    public int moves;
    public int points;
    private int lastTurnPoints; // Stores points from the previous turn

    public bool isGameEnded;

    public TMP_Text pointsTxt;
    public TMP_Text movesTxt;
    public TMP_Text goalTxt;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize(int _moves, int _goal)
    {
        moves = _moves;
        goal = _goal;
        lastTurnPoints = points; // Initialize lastTurnPoints
    }

    void Update()
    {
        pointsTxt.text = "Points: " + points.ToString();
        movesTxt.text = "Moves: " + Mathf.Max(0, moves).ToString();
        goalTxt.text = "Goal: " + goal.ToString();
    }


    public void subtractmove(bool _subtractMoves)
    {
        if (_subtractMoves) // Only decrease moves if _subtractMoves is true
        {
            moves--;
            Debug.Log("Moves decreased! New moves count: " + moves); // Debugging line
        }

        // Check if moves reach 0 and trigger the lose screen
        if (moves <= 0 && !isGameEnded)
        {
            isGameEnded = true;
            backgroundPanel.SetActive(true);
            losePanel.SetActive(true);
            PotionBoard.Instance.potionParent.SetActive(false);
            Debug.Log("Game Over! Moves reached 0."); // Debugging line
        }
    }

    public void ProcessTurn(int _pointsToGain, bool _subtractMoves)
    {
        if (isGameEnded) return;

        points += _pointsToGain;

        Debug.Log("ProcessTurn called! Points: " + points + ", Moves: " + moves); // Debugging line

        lastTurnPoints = points; // Update lastTurnPoints for next turn

        if (points >= goal)
        {
            isGameEnded = true;
            backgroundPanel.SetActive(true);
            victoryPanel.SetActive(true);
            PotionBoard.Instance.potionParent.SetActive(false);
            return;
        }

        if (moves <= 0)
        {
            isGameEnded = true;
            backgroundPanel.SetActive(true);
            losePanel.SetActive(true);
            PotionBoard.Instance.potionParent.SetActive(false);
            return;
        }
    }
} 