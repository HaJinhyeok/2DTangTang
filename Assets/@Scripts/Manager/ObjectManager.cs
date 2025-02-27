using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    private PlayerController _player;
    public PlayerController Player { get => _player; }

    // * HashSet
    // - 중복되지 않는 원소들을 저장하는 집합
    // - 순서가 없음
    // - 빠른 검색, 삽입, 삭제
    public HashSet<PinkController> Pinks { get; set; } = new HashSet<PinkController>();
    public HashSet<EnemyController> Enemys { get; set; } = new HashSet<EnemyController>();
    public HashSet<ProjectileController> Projectiles { get; set; } = new HashSet<ProjectileController>();

    private GameObject _playerResource;
    private GameObject _pinkResource;
    private GameObject _projectileResource;

    protected override void Initialize()
    {
        base.Initialize();

        _playerResource = Resources.Load<GameObject>(Define.PlayerPath);

    }

    public void ResourceAllLoad()
    {
        _playerResource = Resources.Load<GameObject>(Define.PlayerPath);
        _pinkResource = Resources.Load<GameObject>(Define.PinkPath);
        _projectileResource = Resources.Load<GameObject>(Define.ProjectilePath);
    }

    public T Spawn<T>(Vector3 spawnPos) where T : BaseController
    {
        Type type = typeof(T);
        if (type == typeof(PlayerController))
        {
            GameObject obj = Instantiate(_playerResource, spawnPos, Quaternion.identity);
            PlayerController playerController = obj.GetOrAddComponent<PlayerController>();
            _player = playerController;
            // 여기서 CameraController compoenent가 Main Camera에 추가되므로, 따로 스크립트를 붙여줄 필요는 없다
            Camera.main.GetOrAddComponent<CameraController>();
            return playerController as T;
        }
        else if (type == typeof(PinkController))
        {
            GameObject obj = Instantiate(_pinkResource, spawnPos, Quaternion.identity);
            PinkController pinkController = obj.GetOrAddComponent<PinkController>();
            Pinks.Add(pinkController);
            return pinkController as T;
        }
        else if (type == typeof(ProjectileController))
        {
            GameObject obj = Instantiate(_projectileResource, spawnPos, Quaternion.identity);
            ProjectileController projectileController = obj.GetOrAddComponent<ProjectileController>();
            Projectiles.Add(projectileController);
            return projectileController as T;
        }
        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController
    {
        obj.gameObject.SetActive(false);
    }

    protected override void Clear()
    {
        base.Clear();
        Pinks.Clear();
        Projectiles.Clear();
        _player = null;
        // 리소스 파일의 내용 중 사용하지 않은 것들 정리
        Resources.UnloadUnusedAssets();
    }

    public GameObject GetNearestTarget(float distance = 20f)
    {
        var targetList = Pinks.Where(enemy => enemy.gameObject.activeSelf).ToList();
        // 거리 순으로 정렬하여 가장 가까운 적
        var target = targetList.OrderBy(enemy => (Player.Center - enemy.transform.position).sqrMagnitude).FirstOrDefault();

        if ((target.transform.position - Player.Center).sqrMagnitude > distance)
        {
            return null;
        }
        return target.gameObject;
    }
}
