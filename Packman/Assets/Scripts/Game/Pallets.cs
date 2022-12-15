using Game;
using UnityEngine;

public class Pallets : MonoBehaviour {

    private Transform[] _allPallets;
    public int EatenPallets = 0;
    
    void Start() {
        _allPallets = new Transform[transform.childCount];

        for (var i = 0; i < transform.childCount; i++)
            _allPallets[i] = transform.GetChild(i);
    }

    public void ResetState() {
        EatenPallets = 0;
        foreach (var pallet in _allPallets) {
            if(pallet.gameObject.activeSelf) {
                Debug.Log($"Pallet {pallet.name}");
                pallet.GetComponent<Pallet>().Enable();
            }
        }
    }

}
