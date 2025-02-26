using System;
using UnityEngine;

public struct PlayerInfo
{
    public float Atk;
    public float CurrentHp;
    public float MaxHp;
    public float Speed;
}

public class GameManager : Singleton<GameManager>
{
    #region Joystick
    public event Action<Vector2> OnMoveDirChanged;

    Vector2 _moveDir;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set
        {
            _moveDir = value;
            OnMoveDirChanged?.Invoke(value);
        }
    }
    #endregion
    #region PlayerInfo
    public PlayerInfo PlayerInfo = new PlayerInfo()
    {
        Atk = 1,
        CurrentHp = 100,
        MaxHp = 100,
        Speed = 2,
    };

    public GameObject Target { get; set; }
    #endregion
}
