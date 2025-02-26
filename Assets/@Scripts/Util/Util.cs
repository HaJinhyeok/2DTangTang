using UnityEngine;

public class Util
{
    // 게임에 필요한 메서드들 저장
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }

    public static bool RandomBool()
    {
        return UnityEngine.Random.Range(0, 2) == 0;
    }
    public static bool RandomPercent(int percent)
    {
        return percent > UnityEngine.Random.Range(0, 100);
    }
}
