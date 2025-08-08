using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float _smoothing = 0.1f;

    private Vector3 _direction;
    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        _direction = _target.transform.position + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, _direction, ref _velocity, _smoothing, _speed);
    }
}