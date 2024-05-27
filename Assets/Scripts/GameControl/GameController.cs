using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private SceneController sceneController;
    private PlayerController playerController;

    [SerializeField] private TMP_Text levelDisplay;
    [SerializeField] private TMP_Text livesDisplay;
    [SerializeField] private TMP_Text pointsDisplay;

    [Tooltip("The current level of the scene.")]
    [SerializeField] private int level = 0;

    [Tooltip("The amount of lives at the start of the game.")]
    [SerializeField] private int livesAtGameStart = 0;

    private void Start()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        WorldState.Level = level;
        InitializeLivesAtGameStart();
        SaveLevelLivesAndPointsAtLevelStart();

        UpdateLevelDisplay();
        UpdateLivesDisplay();
        UpdatePointsDisplay();

        Cursor.visible = false;
    }

    private void InitializeLivesAtGameStart()
    {
        if (level == 0)
        {
            WorldState.Lives = livesAtGameStart;
        }
    }

    private void SaveLevelLivesAndPointsAtLevelStart()
    {
        if (level != 0)
        {
            WorldState.LevelAtLevelStart = WorldState.Level;
            WorldState.LivesAtLevelStart = WorldState.Lives;
            WorldState.PointsAtLevelStart = WorldState.Points;
        }
    }

    public void UpdateLevelDisplay()
    {
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

    public void AddLives(int _livesToAdd)
    {
        WorldState.Lives += _livesToAdd;
        UpdateLivesDisplay();
    }

    public void HurtOrKillPlayer(float recoilDirection)
    {
        if (playerController.CanBeHit)
        {
            RemoveLife();

            if (WorldState.Lives > 0)
            {
                playerController.HurtPlayer(recoilDirection);
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

    public void AddPoints(int _pointsToAdd)
    {
        WorldState.Points += _pointsToAdd;
        UpdatePointsDisplay();
    }

    public void ResetLivesAndPoints()
    {
        WorldState.Lives = livesAtGameStart;
        WorldState.Points = 0;
    }

    public void ResetLevelLivesAndPointsToValuesAtLevelStart()
    {
        WorldState.Level = WorldState.LevelAtLevelStart;
        WorldState.Lives = WorldState.LivesAtLevelStart;
        WorldState.Points = WorldState.PointsAtLevelStart;
    }
}