using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private readonly Vector2 _lowerBounds = new(-7.5F, -4.5F);
    private readonly Vector2 _upperBounds = new(7.5F, 4.5F);
    private readonly Vector2 _position = new(-5, -3.5F);
    private Vector2 _movement;
    private float _fireDelay = 0.5F;
    private float _lastFired;
    private bool _fireLaser;

    [SerializeField] private float _speed;

    [SerializeField] private GameObject _laser;

    public void Start()
    {
        transform.position = _position;
    }

    public void Update()
    {
        FireLaser();

        transform.Translate(_movement * Time.deltaTime * _speed);

        if (transform.position.x > _upperBounds.x)
        {
            transform.position = new(_lowerBounds.x, transform.position.y);
        }
        else if (transform.position.x < _lowerBounds.x)
        {
            transform.position = new(_upperBounds.x, transform.position.y);
        }

        if (transform.position.y > _upperBounds.y)
        {
            transform.position = new(transform.position.x, _upperBounds.y);
        }
        else if (transform.position.y < _lowerBounds.y)
        {
            transform.position = new(transform.position.x, _lowerBounds.y);
        }
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    public void FireLaser(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _fireLaser = true;
        }
        else if (context.canceled)
        {
            _fireLaser = false;
        }
    }

    public void FireLaser()
    {
        if (!_fireLaser || Time.time - _lastFired < _fireDelay)
        {
            return;
        }

        Instantiate(_laser, transform.position, Quaternion.identity);
        _lastFired = Time.time;
    }
}
