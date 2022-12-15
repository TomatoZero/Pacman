using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Pallet : MonoBehaviour
    {
        [SerializeField] private PalletType type = PalletType.Small;
        [SerializeField] private bool ghostWall = false;
        [Space(10)]
        [SerializeField] private Sprite bigPallet;
        [SerializeField] private Sprite smallPallet;
        [Space(10)] 
        [SerializeField] private LayerMask palletLayer;
        [SerializeField] private Pallets palletParent;

        private SpriteRenderer _renderer;
        private PalletType _defaultType;
        public bool GhostWall => ghostWall;
        public PalletType PalletType => type;


        private List<Vector2> _availableDirections;
        public List<Vector2> AvailableDirections { get => _availableDirections; }
        
        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _defaultType = type;
            palletParent = GetComponentInParent<Pallets>();
            
            if (type == PalletType.Small)
                _renderer.sprite = smallPallet;
            else if(type == PalletType.Big) 
                _renderer.sprite = bigPallet;

            _availableDirections = new List<Vector2>();
            CheckDirection(Vector2.left);
            CheckDirection(Vector2.right);
            CheckDirection(Vector2.up);
            CheckDirection(Vector2.down);
        }

        public void Disable() {
            type = PalletType.None;
            _renderer.sprite = null;

            palletParent.EatenPallets++;
        }

        public void Enable() {
            type = _defaultType;
 
            if (type == PalletType.Small)
                _renderer.sprite = smallPallet;
            else if(type == PalletType.Big) 
                _renderer.sprite = bigPallet;
        }
       
        #region Old Code
        
        public Collider2D[] AdjacentPallets()
        {
            var pallets = new Collider2D[4]
            {
                CheckDirection(Vector2.left),
                CheckDirection(Vector2.right),
                CheckDirection(Vector2.up),
                CheckDirection(Vector2.down)
            };

            return pallets;
        }
        
        private Collider2D CheckDirection(Vector2 direction)
        {
            var position = GetComponent<Transform>().position;
            
            var newPosition = new Vector2();
            if (direction == Vector2.up)
                newPosition = new Vector2(position.x, position.y + 0.3f);
            else if(direction == Vector2.down)
                newPosition = new Vector2(position.x, position.y - 0.3f);
            else if(direction == Vector2.right)
                newPosition = new Vector2(position.x + 0.3f, position.y);
            else if (direction == Vector2.left)
                newPosition = new Vector2(position.x - 0.3f, position.y);
                
            
            var hit = Physics2D.Raycast(newPosition, direction, 1f, palletLayer);

            if(hit.collider != null)
                _availableDirections.Add(direction);
            
            return hit.collider;
        }
        
        #endregion

    }

    public enum PalletType
    {
        Small,
        Big,
        None
    };
}