using UnityEngine;
using UnityEngine.SceneManagement;
using DebugTools;


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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update references when a new scene is loaded
        player = FindObjectOfType<PlayerMovement>();
        enemy = FindObjectOfType<NewEnemyPathfinding>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keeps this object alive across scene changes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);            // Destroy any other instances that get created in new scenes
            return;
        }

        if (CurrentState != GameState.Start) // Assuming Start state corresponds to the menu
        {
            player = FindObjectOfType<PlayerMovement>();
            enemy = FindObjectOfType<NewEnemyPathfinding>();
            StartPlayerTurn(); //
        }
    }

    private void Start()
    {
        CurrentState = GameState.Start;
        player = FindObjectOfType<PlayerMovement>();
        enemy = FindObjectOfType<NewEnemyPathfinding>();
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
        if (player && !player.enabled)
        {
            CurrentState = GameState.PlayerTurn;
            player.enabled = true;
            player.useActionPoints = true; // Enable player input
            PlayerMovement.currentActionPoints = player.maxActionPoints;
        }
        
    }

    public void EndPlayerTurn()
    {
        if (player && player.enabled)
        {
            player.useActionPoints = false; // Disable player input
            player.enabled = false;
            StartEnemyTurn();
        }
    }

    public void StartEnemyTurn()
    {
        if (enemy)
        {
            int playerCurrentActionPoints = PlayerMovement.currentActionPoints;
            CurrentState = GameState.EnemyTurn;
            if (!enemy.enabled)
            {
                EndEnemyTurn();
                return;
            }
            else
            {
                enemy.shouldFollowPlayer = true; // This will make the enemy calculate the path and start following the player
            }
        }
    }

    public void EndEnemyTurn()
    {
        if (enemy)
        {
            CurrentState = GameState.PlayerTurn;
            StartPlayerTurn(); // Start the player's turn again
        }
    }
}
