using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerController playerController;

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
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

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

    public void HurtOrKillPlayer()
    {
        if (!(playerController.IsHurt || playerController.IsDying))
        {
            if (WorldState.Lives > 1)
            {
                RemoveLife();
                playerController.HurtPlayer();
            }
            else
            {
                playerController.KillPlayer();
            }
        }
    }

    private void RemoveLife()
    {
        WorldState.Lives--;
        UpdateLivesDisplay();
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