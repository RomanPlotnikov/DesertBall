using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private CoinContainer _coinContainer;

    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    private PlayerInput _input;
    private Rigidbody _rigibody;

    private bool _isGrounded = false;

    private void Awake()
    {
        _input = new PlayerInput();
        _rigibody = GetComponent<Rigidbody>();
        _rigibody.maxAngularVelocity = 500f;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Jump.performed += context => TryJump();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        Vector2 direction = -_input.Player.Rotate.ReadValue<Vector2>();

        RotateTo(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
            _coinContainer.Collect(coin);
    }

    private void OnCollisionStay(Collision collision)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void RotateTo(Vector2 direction)
    {
        _rigibody.AddTorque(-_cameraHolder.right * direction.y * _speed * Time.deltaTime, ForceMode.Impulse);
        _rigibody.AddTorque(_cameraHolder.forward * direction.x * _speed * Time.deltaTime, ForceMode.Impulse);
    }

    private bool TryJump()
    {
        if (_isGrounded)
        {
            _rigibody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            return true;
        }
        else
            return false;
    }
}