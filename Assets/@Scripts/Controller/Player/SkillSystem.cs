using System.Collections;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    Coroutine _coSkill;
    WaitForSeconds _coolTime = new WaitForSeconds(1f);

    void Start()
    {
        if (_coSkill == null)
            _coSkill = StartCoroutine(CoStartSkill());
    }

    IEnumerator CoStartSkill()
    {
        while (true)
        {
            if (ObjectManager.Instance.Pinks.Count > 0)
            {
                Vector3 spawnPos = ObjectManager.Instance.Player.Center;
                GameManager.Instance.Target = ObjectManager.Instance.GetNearestTarget();
                if (GameManager.Instance.Target != null)
                {
                    PoolManager.Instance.GetObject<ProjectileController>(spawnPos);
                }
            }
            yield return _coolTime;
        }
    }
}
