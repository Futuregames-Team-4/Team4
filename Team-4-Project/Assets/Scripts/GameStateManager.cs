using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Start,     // Show Main Menu
        PlayerTurn,   // Player's turn
        EnemyTurn, // Enemy's turn
        Paused,    // Show the Pause Menu
        End         // Game Over or Victory
    }
    public static GameStateManager Instance; // Singleton reference

    public GameState CurrentState { get; private set; } = GameState.Start;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private NewEnemyPathfinding enemy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        enemy = FindObjectOfType<NewEnemyPathfinding>();
        StartPlayerTurn();
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case GameState.PlayerTurn:
                if (PlayerMovement.currentActionPoints <= 0)
                {
                    EndPlayerTurn();
                }
                break;

            case GameState.EnemyTurn:
                // Enemy logic is handled within the enemy's script
                break;

            // Additional cases can be added later
            case GameState.Start:
            case GameState.End:
            case GameState.Paused:
                break;
        }
    }

    public void StartPlayerTurn()
    {
        CurrentState = GameState.PlayerTurn;
        player.useActionPoints = true;  // Enable player input
        PlayerMovement.currentActionPoints = player.maxActionPoints;
    }

    public void EndPlayerTurn()
    {
        CurrentState = GameState.EnemyTurn;
        player.useActionPoints = false; // Disable player input
        StartEnemyTurn();
    }

    public void StartEnemyTurn()
    {
        enemy.shouldFollowPlayer = true; // This will make the enemy calculate the path and start following the player
    }

    public void EndEnemyTurn()
    {
        CurrentState = GameState.PlayerTurn;
        StartPlayerTurn(); // Start the player's turn again
    }
}
