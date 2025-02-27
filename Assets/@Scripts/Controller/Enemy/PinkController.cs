using UnityEngine;

public class PinkController : EnemyController
{
    protected override void Initialize()
    {
        base.Initialize();
    }

    private void OnEnable()
    {
        _currentHp = 3;
    }
}
