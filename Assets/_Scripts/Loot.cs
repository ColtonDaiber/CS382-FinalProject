using UnityEngine;

public class Loot : MonoBehaviour
{
    public float worth = 100;

    void OnTriggerEnter(Collider coll){
        GameObject otherGO = coll.gameObject;
        if(otherGO.transform.parent == null) return;
        
        Player p = otherGO.transform.parent.GetComponent<Player>();
        if(p != null){
         Destroy(this.gameObject);
         p.score += worth;    
        }
    }

}
