using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // �̱��� ���� ��ȭ��Ű��
    // �̰͵� ���� ��� -> �� ���� �� �˾Ƽ� ����
    protected static T _instance = null;
    public static bool IsInstance => _instance != null;
    public static T TryGetInstance() => IsInstance ? _instance : null;

    public static T Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ���
            if (_instance == null)
            {
                // �ش� �̱��� �ν��Ͻ��� �θ� ��ü�� �� @Managers ã��
                GameObject manager = GameObject.Find("@Managers");
                // ������ �������ֱ�
                if (manager == null)
                {
                    manager = new GameObject("@Managers");
                    DontDestroyOnLoad(manager);
                }
                // �ش� �̱��� ��ü Ÿ���� ������Ʈ ã��(�̱����̹Ƿ� �ϳ��� ã���� �װ� ���ΰ���)
                _instance = FindAnyObjectByType<T>();
                // ������ �������ֱ�
                if (_instance == null)
                {
                    // �̱��� ��ü Ÿ���� �̸����� ���� ������Ʈ ����
                    GameObject obj = new GameObject(typeof(T).Name);
                    // �ش� �̱��� Ÿ���� ��ũ��Ʈ�� �߰�
                    T component = obj.AddComponent<T>();
                    // ������ ��ü�� �θ� ����
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
        // �� ��ȯ �� �ϸ� ����
    }
}
