using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // 싱글톤 패턴 진화시키기
    // 이것도 옛날 방식 -> 더 좋은 건 알아서 공부
    protected static T _instance = null;
    public static bool IsInstance => _instance != null;
    public static T TryGetInstance() => IsInstance ? _instance : null;

    public static T Instance
    {
        get
        {
            // 인스턴스가 없을 경우
            if (_instance == null)
            {
                // 해당 싱글턴 인스턴스의 부모 객체가 될 @Managers 찾기
                GameObject manager = GameObject.Find("@Managers");
                // 없으면 생성해주기
                if (manager == null)
                {
                    manager = new GameObject("@Managers");
                    DontDestroyOnLoad(manager);
                }
                // 해당 싱글턴 객체 타입의 오브젝트 찾기(싱글턴이므로 하나만 찾으면 그게 주인공임)
                _instance = FindAnyObjectByType<T>();
                // 없으면 생성해주기
                if (_instance == null)
                {
                    // 싱글턴 객체 타입을 이름으로 갖는 오브젝트 생성
                    GameObject obj = new GameObject(typeof(T).Name);
                    // 해당 싱글턴 타입을 스크립트로 추가
                    T component = obj.AddComponent<T>();
                    // 생성된 객체의 부모 설정
                    obj.transform.parent = manager.transform;

                    _instance = component;
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    protected void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        Clear();
    }

    protected virtual void Clear()
    {
        // 씬 전환 시 하면 좋음
    }
}
