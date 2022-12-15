using System;
using System.Collections.Generic;
using Game;
using JetBrains.Annotations;
using UnityEngine;

namespace Old
{
    public class Ghost : MonoBehaviour
    {
        [SerializeField] private Pallet startPallet;
        [SerializeField] private Pallet targetPallet;
        [SerializeField] private Pallets manager;
        [SerializeField] private LayerMask palletLayer;
        
        private bool _isLive;
        private Vector2 _prevDirection;
        private Transform _transform;
        private Movement _movement;
        
        private Pallet _target;
        private Pallet _currentPallet;
        private Pallet _prevPallet;
        private Pallet _nextPallet;
        private int _currentWayId = 0;


        private bool _reachedNextPallet = false;
        private bool _reachedTarget = false;
        private Pallet[] _way;
        
        
        private void Start() {
            _currentPallet = startPallet;
            _isLive = true;
            _target = targetPallet;
            _transform = GetComponent<Transform>();
            _movement = GetComponent<Movement>();
            
            _way = manager.BuildWay(startPallet, _target, null);
            // var prevDrawPallet = _currentPallet;
            // Debug.Log("Way lenght" + _way.Length);
            // for (int i = 0; i < _way.Length; i++) {
            //     var drawDirection = (prevDrawPallet.transform.position - _way[i].transform.position).normalized;
            //     Debug.DrawRay(prevDrawPallet.transform.position, drawDirection, Color.blue, 10f);
            //     prevDrawPallet = _way[i];
            //     // Debug.Log($"Way[{i}] {_way[i].name}");
            // }
        }

        private void Update() {
            if (_reachedTarget) {
                
                var prevDrawPallet = _currentPallet;
                foreach (var pallet in _way) {
                    var drawDirection = (prevDrawPallet.transform.position - pallet.transform.position).normalized;
                    Debug.DrawRay(prevDrawPallet.transform.position, drawDirection, Color.blue, 10f);
                    // Debug.Log($"pallet name {pallet.name}");
                    prevDrawPallet = pallet;
                }

                _reachedTarget = false;
            }

            if (_reachedNextPallet) {
                var direction = -((Vector2)_currentPallet.transform.position - (Vector2)_nextPallet.transform.position).normalized;
                
                Debug.Log($"Set direction in reached next pallet {direction}");
                _movement.SetDirection(direction);
                _reachedNextPallet = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var o = col.gameObject;
            Debug.Log("reach " + o.name + " " + o.layer + " " + (int)palletLayer);
            Debug.Log("name: " + o.name);
            
            if (col.gameObject.layer == 3)
            {
                Debug.Log("1");
                if (o.GetComponent<Pallet>() == _target) {
                    Debug.Log("2");
                    _target = DefinedNextTarget();
                    _way = manager.BuildWay(_currentPallet, _target, null);
                    _currentWayId = 0;
                }
                Debug.Log("3");

                _reachedNextPallet = true;
                _nextPallet = _way[++_currentWayId];
                _prevPallet = _currentPallet;
                _currentPallet = o.GetComponent<Pallet>();
            }
        }

        public void ResetState() {
            _movement.ResetState();
        }

        private Pallet DefinedNextTarget() {
            //TODO:
            
            return _target;
        }
    }
}
