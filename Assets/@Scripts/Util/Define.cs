using UnityEngine;

public class Define
{
    #region Path
    public const string PlayerPath = "Prefabs/Player";
    public const string PinkPath = "Prefabs/Pink";
    public const string ProjectilePath = "Prefabs/Projectile";
    #endregion

    #region Tag
    public const string EnemyTag = "Enemy";
    #endregion

    #region Animator
    public readonly static int isMoveHash = Animator.StringToHash("isMove");
    #endregion
}