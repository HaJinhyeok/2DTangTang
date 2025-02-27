using System.Collections;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    Coroutine _coSpawningPool;
    WaitForSeconds _spawnInterval = new WaitForSeconds(2f);

    void Start()
    {
        ObjectManager.Instance.ResourceAllLoad();
        ObjectManager.Instance.Spawn<PlayerController>(Vector3.zero);
        if(_coSpawningPool==null)
        {
            _coSpawningPool = StartCoroutine(CoSpawningPool());
        }
    }

    IEnumerator CoSpawningPool()
    {
        while(true)
        {
            yield return _spawnInterval;

            Vector3 spawnPos = GetRandomPositionAround(ObjectManager.Instance.Player.transform.position);

            PoolManager.Instance.GetObject<PinkController>(spawnPos);
        }
    }

    public Vector2 GetRandomPositionAround(Vector2 origin, float minDistance = 5f, float maxDistance = 10f)
    {
        float angle = Random.Range(0, 360) * Mathf.Rad2Deg;
        float distance = Random.Range(minDistance, maxDistance);

        float offsetX = Mathf.Cos(angle) * distance;
        float offsetY = Mathf.Sin(angle) * distance;

        Vector2 pos = origin + new Vector2(offsetX, offsetY);

        return pos;
    }
}
