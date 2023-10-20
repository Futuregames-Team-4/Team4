using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; } // Singleton instance
    
    public enum GameState { 
        GameStateStart,     // Show Main Menu
        GameStatePlaying,   // Player's turn
        GameStateEnemyTurn, // Enemy's turn
        GameStatePaused,    // Show the Pause Menu
        GameStateEnd }      // Game Over / Victory!

    public static GameState CurrentState { get; private set; } = GameState.GameStateStart;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        // Singleton logic
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return; // This is important to stop further execution for the duplicate instance.
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        playerMovement = FindObjectOfType<PlayerMovement>();
        OnGameStateChanged += HandleGameStateChange;
    }

    private void OnDestroy()
    {
        OnGameStateChanged -= HandleGameStateChange;
    }

    public delegate void GameStateChanged(GameState newState);
    public event GameStateChanged OnGameStateChanged;

    public void SetGameState(GameState newState)
    {
        if (newState != CurrentState)
        {
            CurrentState = newState;
            OnGameStateChanged?.Invoke(newState);
        }
    }

    private void HandleGameStateChange(GameState newState)
    {
        if (newState == GameState.GameStatePlaying)
        {
            playerMovement.enabled = true;
        }
        else 
        {
            playerMovement.enabled = false;
        }
    }
}
