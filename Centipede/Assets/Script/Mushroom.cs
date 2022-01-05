using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int mushroomHealth = 3;                     // Number of mushroom health   
    public int scoreMushroom = 50;                     // Score when player destroy mushroom    
    private Player player;    

    void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();        
    }

    void Update(){
        if (this.mushroomHealth <= 0){                 // When mushroom health less than or equal to 0 then destroy mushroom object
            Destroy(gameObject);           
            player.score += scoreMushroom;             // After mushroom get destroy add score
        }        
    }

    // When bullet hit mushroom then mushroom health decreased by 1
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Bullet")){
            this.mushroomHealth--;
        }        
    }
}
