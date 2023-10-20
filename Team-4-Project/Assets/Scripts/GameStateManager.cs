using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState { GameStateStart, GameStatePlaying, GameStatePaused, GameStateEnd }
    public GameState CurrentState { get; private set; } = GameState.GameStateStart;

    private PlayerMovement playerMovement;

    private void Awake()
    {
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
