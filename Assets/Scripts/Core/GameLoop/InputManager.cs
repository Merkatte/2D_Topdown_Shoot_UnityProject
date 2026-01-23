 using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputReader _playerInputReader;
    private IPlayerInput _playerInput;

    #region Init
    public void Init()
    {
        _playerInputReader.Init();
        _playerInput = _playerInputReader.GetPlayerInput();
    }
    #endregion
    
    #region public
    public IPlayerInput GetPlayerInput() => _playerInput;
    #endregion
}
