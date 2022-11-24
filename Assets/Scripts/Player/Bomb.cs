using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
  
    public float Countdown = 3;
   
    public float Radius = 1;
    public float Dammage = 1;
    

    // Update is called once per frame
    void Update()
    {
        Countdown -= Time.deltaTime;
        if (Countdown <=0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        
        
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, Radius);
            foreach (var hit in hitColliders)
        {
                if (hit.GetComponent<EnemyController>())
                {
                    var ennemy = hit.GetComponent<EnemyController>();
                        if (ennemy)
                        {
                            Destroy(ennemy.gameObject);
                        }
                }
                if (hit.gameObject.tag == "DestroyWall")
                {
                    var Wall = hit.gameObject;
                    if (Wall)
                    {
                        Destroy(Wall);
                    }
                }
                
                
            }
            Destroy(gameObject);
        
        
    }


}
