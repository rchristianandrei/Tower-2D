using System;
using UnityEngine;

public class GameInputActions : MonoBehaviour
{
    public static GameInputActions Instance { get; private set; }

    public event EventHandler OnPlayerJump;
    public event EventHandler OnPlayerAttack;

    private GameInput gameInput;

    private Vector2 playerMovement;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Game Input Actions should be singleton");
            return;
        }

        gameInput = new GameInput();
        gameInput.Player.Enable();
        gameInput.Player.Jump.performed += (obj) =>
        {
            OnPlayerJump?.Invoke(this, EventArgs.Empty);
        };
        gameInput.Player.PrimaryAttack.performed += (obj) =>
        {
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
        };

        Instance = this;
    }

    private void OnDestroy()
    {
        gameInput.Dispose();
        Instance = null;
    }

    private void Update()
    {
        playerMovement = gameInput.Player.Movement.ReadValue<Vector2>().normalized;
    }

    public Vector2 getPlayerMovement()
    {
        return playerMovement;
    }
}
