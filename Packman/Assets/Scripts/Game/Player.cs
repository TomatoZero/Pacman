using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Movement), typeof(Animator),  typeof(Transform))]
    public class Player : MonoBehaviour {
        [SerializeField] private GameManager gameManager;
        
        private Movement _movement;
        private Animator _animator;
        private Transform _transform;
 
        private int _score;
        private int _life;
        private static readonly int Dead = Animator.StringToHash("Dead");
        private bool _isDead = false;
        
        public int Life
        {
            get => _life;
            set
            {
                if(_life > value) {
                    _life = value;
                    gameManager.PacmanDie();
                }
                else
                    _life = value;
            }
        }
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                gameManager.SetScore(_score);
            }
        }

        private void Awake() {
            _transform = GetComponent<Transform>();
            _movement = GetComponent<Movement>();
            _animator = GetComponent<Animator>();
            
        }

        private void Start()
        {
            _life = 3;

            _animator = GetComponent<Animator>();
            _movement = GetComponent<Movement>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
                _movement.SetDirection(Vector2.up);
            else if (Input.GetKeyDown(KeyCode.S))
                _movement.SetDirection(Vector2.down);
            else if (Input.GetKeyDown(KeyCode.A))
                _movement.SetDirection(Vector2.left);
            else if (Input.GetKeyDown(KeyCode.D))
                _movement.SetDirection(Vector2.right);

            var angel = Mathf.Atan2(_movement.Direction.y, _movement.Direction.x);
            _transform.rotation = Quaternion.AngleAxis(angel * Mathf.Rad2Deg, Vector3.forward);
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if(col.gameObject.layer == 3) {
                Pallet pallet = col.gameObject.GetComponent<Pallet>();
                if(pallet.PalletType != PalletType.None) {
                    if (pallet.PalletType == PalletType.Big) 
                        gameManager.PowerPelletEaten();
      
                    pallet.Disable();
                    Score++;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D col) {
            if(col.gameObject.layer == 9) {
                var ghost = col.gameObject.GetComponent<Ghost>();
                if(ghost.Mod != GhostMod.Frighten) {
                    _isDead = true;
                    _animator.SetTrigger(Dead);
                }
                else {
                    ghost.Eaten();
                }
            }
        }

        public void GamePause(bool pause)
        {
            if(!_isDead)
                _animator.SetBool("IsPause", pause);
        }

        public void ResetState() {
            _movement.ResetState();
        }

        public void NewGameLoad() {
            _life = 0;
            _score = 0;
            ResetState();
        }
        
        public void IsDead() {
            Life--;
        }
        
    }
}