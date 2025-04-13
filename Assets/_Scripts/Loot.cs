using UnityEngine;

public class Loot : MonoBehaviour
{
    public float worth = 100;

    void OnTriggerEnter(Collision coll){
        GameObject otherGO = coll.gameObject;
        Player p = otherGO.GetComponent<Player>();
        if(p != null){
         Destroy(this.gameObject);
         p.score += worth;    
        }
    }

}
