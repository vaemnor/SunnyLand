using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private SceneController sceneController;
    private PlayerMovement playerMovement;

    [SerializeField] private int level;

    [SerializeField] private TMP_Text levelDisplay;
    [SerializeField] private TMP_Text livesDisplay;
    [SerializeField] private TMP_Text pointsDisplay;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        UpdateLevelDisplay();
        UpdateLivesDisplay();
        UpdatePointsDisplay();
    }

    public void UpdateLevelDisplay()
    {
        WorldState.Level = level;
        levelDisplay.text = $"Level: {WorldState.Level}";
    }

    public void UpdateLivesDisplay()
    {
        livesDisplay.text = $"Lives: {WorldState.Lives}";
    }

    public void UpdatePointsDisplay()
    {
        pointsDisplay.text = $"Points: {WorldState.Points}";
    }

    public void AddLives(int livesToAdd)
    {
        WorldState.Lives += livesToAdd;
        UpdateLivesDisplay();
    }

    public void CheckIfLivesAreGreaterThanOne()
    {
        if (WorldState.Lives > 1)
        {
            RemoveLife();
        }
        else
        {
            sceneController.LoadGameOverScreen();
        }
    }

    private void RemoveLife()
    {
        WorldState.Lives--;

        UpdateLivesDisplay();
        playerMovement.GoToStartPosition();
    }

    public void AddPoints(int pointsToAdd)
    {
        WorldState.Points += pointsToAdd;
        UpdatePointsDisplay();
    }

    public void RemovePoints(int pointsToRemove)
    {
        WorldState.Points -= pointsToRemove;
        UpdatePointsDisplay();
    }

    public void ResetLivesAndPoints()
    {
        WorldState.Lives = 3;
        WorldState.Points = 0;
    }
}