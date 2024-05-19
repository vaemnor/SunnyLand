using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private SceneController sceneController;
    private PlayerController playerController;

    [SerializeField] private int level = 0;

    [SerializeField] private TMP_Text levelDisplay;
    [SerializeField] private TMP_Text livesDisplay;
    [SerializeField] private TMP_Text pointsDisplay;

    private void Start()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
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

    public void HurtOrKillPlayer(float reboundDirection)
    {
        if (!(playerController.IsHurt || playerController.IsDying))
        {
            if (WorldState.Lives > 1)
            {
                RemoveLife();
                playerController.HurtPlayer(reboundDirection);
            }
            else
            {
                playerController.KillPlayer();
                StartCoroutine(sceneController.LoadGameOverScreen());
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