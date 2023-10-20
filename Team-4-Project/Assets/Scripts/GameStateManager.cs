using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState { 
        GameStateStart,     // Show Main Menu
        GameStatePlaying,   // Player's turn
        GameStateEnemyTurn, // Enemy's turn
        GameStatePaused,    // Show the Pause Menu
        GameStateEnd }      // Game Over / Victory!

    public GameState CurrentState { get; private set; } = GameState.GameStateStart;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        // Only 1 instance of GameStateManager exists between scenes, and is not destroyable
        if (FindObjectsOfType<GameStateManager>().Length > 1) 
            Destroy(gameObject);
        else
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
