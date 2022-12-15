using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float speedMultiplier = 1f;
        [SerializeField] private Vector2 initialDirection;
        [SerializeField] private GameManager gameManager;
    
        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private Vector2 _nextDirection;
        private Vector2 _startPosition;
    
        public Vector2 Direction { get => _direction; }
        public Vector2 StartPosition { get => _startPosition; }
        public float SpeedMultiper {
            get => speedMultiplier;
            set => speedMultiplier = value;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _startPosition = this.transform.position;
            _nextDirection = Vector2.zero;
        }

        private void Start()
        {
            _rigidbody.isKinematic = false;
            _direction = initialDirection;
        }

        private void Update()
        {
            if(_nextDirection != Vector2.zero)
                SetDirection(_nextDirection);
        }

        private void FixedUpdate()
        {
            if(gameManager.IsPause) {
                return;
            }
            
            var translation = _direction * (speed * speedMultiplier * Time.fixedDeltaTime);
            _rigidbody.MovePosition(_rigidbody.position + translation);
        }

        public void SetDirection(Vector2 direction)
        {
            if (!CheckDirection(direction))
            {
                this._direction = direction;
                _nextDirection = Vector2.zero;
            }
            else
            {
                _nextDirection = direction;
            }
        }

        private bool CheckDirection(Vector2 direction)
        {
            var hit = Physics2D.BoxCast(_rigidbody.position, Vector2.one * 0.75f, 0f, direction, 1.5f,obstacleLayer);
            return hit.collider != null;
        }

        public void ResetState() {
            this.transform.position = _startPosition;
            _direction = initialDirection;
        }
    }
}
