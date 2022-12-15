using UnityEngine;

namespace Game
{
    public class TeleportScript : MonoBehaviour
    {
        [SerializeField] private GameObject otherTeleport;
        [SerializeField] private TpNum numTeleport; 
    
        public void OnTriggerEnter2D(Collider2D col)
        {
            if(numTeleport == TpNum.First)
                col.transform.position = otherTeleport.transform.position + Vector3.left;
            else
                col.transform.position = otherTeleport.transform.position + Vector3.right;
        }
        
        enum TpNum
        {
            First,
            Second
        }
    }
}
