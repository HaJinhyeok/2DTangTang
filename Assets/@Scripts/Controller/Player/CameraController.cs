using UnityEngine;

public class CameraController : BaseController
{
    private Transform _target;
    private Vector3 _offset = new Vector3(0, 0, -10);
    private Vector3 _movePos;
    private float _speed;

    protected override void Initialize()
    {
        _target = ObjectManager.Instance.Player.transform;
    }

    private void FixedUpdate()
    {
        _movePos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _movePos, _speed * Time.deltaTime);
    }
}
