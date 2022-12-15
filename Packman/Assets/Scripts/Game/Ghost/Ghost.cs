using System.Collections;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Movement))]
    public class Ghost : MonoBehaviour {

        [SerializeField] private Transform target;
        [SerializeField] private Animator bodyAnimator;
        [SerializeField] private float deadDuration;

        [SerializeField] private GameObject body;
        [SerializeField] private GameObject eyes;
        [SerializeField] private Color defaultColor;
        
        private GhostScatter _scatter;
        private GhostChase _chase;
        private GhostFrightened _frightened;
        private GhostHome _home;
        private GhostMod _mod;
        
        private Vector2 _prevDirection;
        private Transform _transform;
        private Movement _movement;

        public Movement Movement { get => _movement; }
        public Transform Target { get => target; }
        public GhostMod Mod { get => _mod; }
        public Color DefaultColor {
            get => defaultColor;
        }
        
        private void Start() {
            _transform = GetComponent<Transform>();
            _movement = GetComponent<Movement>();

            _scatter = GetComponent<GhostScatter>();
            _chase = GetComponent<GhostChase>();
            _frightened = GetComponent<GhostFrightened>();
            _home = GetComponent<GhostHome>();
            
            Spawn(deadDuration);
        }

        private void Spawn(float duration) {
            body.SetActive(false);
            _mod = GhostMod.Null;
            _movement.SetDirection(Vector2.zero);

            StartCoroutine(Respawn(duration));
        }

        private IEnumerator Respawn(float duration) {
            while (duration > 0) {
                yield return new WaitForSeconds(1f);
                duration--;
            }
            
            _home.Enable();
            _mod = GhostMod.Home;
            _movement.ResetState();
            body.SetActive(true);
        }
        
        public void ResetState() {
            SetGhostMod(GhostMod.Frighten, false);
            _movement.ResetState();
            SetGhostMod(GhostMod.Home, true);
            Spawn(deadDuration);
        }

        public void SetGhostMod(GhostMod mod, bool active) {
            if(_mod != GhostMod.Null)
                if(active) {
                    _mod = mod;
                    (GetMod(mod)).Enable();
                }
                else
                    (GetMod(mod)).Disable();
        }
        
        public void SetAnimatorTrigger(string trigger) {
            bodyAnimator.SetTrigger(trigger);
        }

        public void SetRenderer(bool active, Color color) {
            eyes.SetActive(active);
            body.GetComponent<SpriteRenderer>().color = color;
        }

        public void Eaten() {
            ResetState();
            Spawn(deadDuration);
            
            GameManager.Instance.GhostEaten();
        }

        private void OnTriggerEnter2D(Collider2D col) {
            var pallet = col.GetComponent<Pallet>();

            if (pallet != null) {
                var mod = GetMod(_mod);
                if(mod != null)
                    _movement.SetDirection(mod.FindDirection(pallet));
            }
        }

        private GhostBehavior GetMod(GhostMod mod) {
            switch (_mod) {
                case GhostMod.Scatter:
                    return _scatter;
                case GhostMod.Chase:
                    return _chase;
                case GhostMod.Frighten:
                    return _frightened;
                case GhostMod.Home:
                    return _home;
                default:
                    return null;
            }
        }
        
    }

    public enum GhostMod {
        Scatter,
        Chase,
        Frighten,
        Home,
        Null
    }
}
