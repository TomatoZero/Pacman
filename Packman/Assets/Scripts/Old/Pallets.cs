#nullable enable
using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Old {
    public class Pallets : MonoBehaviour
    {
        private int _countPallets;

        // private Pallet start;
        // private Pallet target;

        [SerializeField] private Pallet[] pallets;
    
        private void Start() {
        
            _countPallets = transform.childCount;
            // Debug.Log($"Pallets Start. {_countPallets}");
            // for (int i = 0; i < _countPallets; i++) {
            //     transform.GetChild(i).name = i.ToString();
            // }


        }

        // public void SetStartTarget(Pallet sttart, Pallet ttarget, Pallet block) {
        //     this.start = sttart;
        //     this.target = ttarget;
        //
        //     var prev = Bfs(sttart, ttarget, block);
        //     Debug.Log($"Prev length {prev.Length}");
        //
        //     var currentPallet = ttarget;
        //     Pallet prevPellet;
        //     while (true)
        //     {
        //         var id = int.Parse(currentPallet.name);
        //
        //         prevPellet = currentPallet;
        //         currentPallet = prev[id];
        //
        //         var direction = (prevPellet.transform.position - currentPallet.transform.position).normalized;
        //         Debug.DrawRay(prevPellet.transform.position, direction, Color.blue, 10f);
        //         Debug.Log($"pallet name {currentPallet.name}");
        //         if(currentPallet.Equals(sttart))
        //             break;
        //     }
        // }

        public Pallet[] BuildWay(Pallet start, Pallet target, Pallet blockWay)
        {
            pallets = Bfs(start, blockWay);
            var way = new List<Pallet>(){target};

            // palletss = pallets;
            
            // Debug.Log($"Pallets.Length {pallets.Length}");
            // Debug.Log($"Start {start}");
            // Debug.Log($"Target {target}");
            // Debug.Log($"BlockWay {blockWay}");
            
            var currentPallet = target;
            do
            {
                var id = int.Parse(currentPallet.name);
                
                // Debug.Log($"id {id}");

                currentPallet = pallets[id];
                way.Add(currentPallet);
            
                // Debug.Log($"CurrentPallet {currentPallet}");

            
                // Debug.Log($"Layer pallet {(int)pallets[id].PalletMask}");
                // Debug.Log($"pallets[id] {pallets[id]}");
                // Debug.Log($"current {currentPallet} \nstart {start} \ntarget {target}");
            
            } while (currentPallet.name != start.name);

            way.Reverse();
            return way.ToArray();
        }
    
        private Pallet[] Bfs(Pallet start, Pallet? blockWay)
        {
            var qe = new Queue<Pallet>();
            var st = new int?[_countPallets];
            var dist = new int?[_countPallets];
            var previous = new Pallet?[_countPallets];

            var startId = int.Parse(start.name); 
        
            qe.Enqueue(start);
            st[startId] = 1;
            dist[startId] = 0;
            previous[startId] = null;
        
            Pallet current;
            int myI = 0;
            while (qe.Count > 0)
            {
                current = qe.Dequeue();
                var currentId = int.Parse(current.name);
                var adjacent = current.AdjacentPallets();
                
                Pallet next;
                int nextId;

                for (var i = 0; i < 4; i++)
                {
                    if(adjacent[i] == null) 
                        continue;
                    
                    next = adjacent[i].GetComponent<Pallet>();
                    nextId = int.Parse(next.name);
                
                    // Debug.Log($"Next: {next.name}");
                    // Debug.Log($"st[nextId] {st[nextId]}");
                
                    if(next.name == blockWay?.name || i == 2 && next.GhostWall)
                    {
                        st[nextId] = -1;
                        continue;
                    }
                    else if(st[nextId] == null)
                    {
                        // Debug.Log("Queue add" + next.name);
                        qe.Enqueue(next);
                        dist[nextId] = dist[currentId] + 1;
                        previous[nextId] = current;
                        st[nextId] = 1;
                    }
                }
                // Debug.Log("I " + myI++);
            }

            return previous;
        }

    }
}
