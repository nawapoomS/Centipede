using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{     
    public bool isCollide;                                                              // Check centipede collide with gameobject or not?                 
    /* Check when centipede collide with wall and centipede will move 
    such as centipede collide with west wall and centipede will move to east*/
    public bool collideEast;
    /* Check when centipede collide with wall and centipede will move 
    such as centipede collide with south wall and centipede will move up to north*/
    public bool collideSouth; 

    public int order;                                                                   // Order of the centipede body       
    public float centipedeSpeed = 5f;                                                   // Speed of centipede      
    public int scoreCentipede = 150;
    
    public Vector3 moveDirection;                                                      // Direction of centipede movement
    private float nextMove = 0;
    private Rigidbody2D rb;      
    private MapManager map;
    private Player player;
    
    void Start(){
        isCollide = false;
        collideSouth = true;
        collideEast = true;
        moveDirection = Vector3.right; 

        rb = GetComponent<Rigidbody2D>();
        map = GameObject.Find("MapManager").GetComponent<MapManager>();
        player = GameObject.Find("Player").GetComponent<Player>();         
    }

    void Update(){
        CheckMovable(moveDirection);
        CentipedeMovement();
    }

    void CentipedeMovement(){
        if(collideEast){                                                                // Move from West to East
            moveDirection = Vector3.right;
        }
        else{
            moveDirection = Vector3.left;                                               // Move from East to West
        }

        if(isCollide){
            if(collideSouth){                                                           // Move from North to south
                moveDirection = Vector3.down;
            }
            else{                                                                       // Move from South to North
                moveDirection = Vector3.up;  
            }
        }      

        if (Time.time > nextMove){                                                      // Set speed of centipede
            isCollide = false;
            nextMove = Time.time + 1f / centipedeSpeed;
            rb.MovePosition(transform.position + moveDirection);
            // When change rows centipede still moving        
            if (collideEast){                                                   // Move from West to East            
                moveDirection = Vector3.right;
            }            
            else{                                                                       // Move from East to West            
                moveDirection = Vector3.left;
            }
        }
    }   
 
    void CheckMovable(Vector3 moveDirection){
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDirection, 1);
        foreach (RaycastHit2D h in hits){            
            if (!h.collider.gameObject.CompareTag("Centipede"))
            {
                // Change the horizontal direction between West and East
                if (h.collider.gameObject.CompareTag("Wall") || h.collider.gameObject.CompareTag("Mushroom")
                 || h.collider.gameObject.CompareTag("Player")){
                    isCollide = true;
                    collideEast = collideEast == true ? false : true;
                }
                // Change the Vertical direction between North and South
                else if (h.collider.gameObject.name == "North" || h.collider.gameObject.name == "South"){
                    collideSouth = collideSouth == true ? false : true;
                }
            }
        }
    }

    // Check collision when bullet hit the centipede
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Bullet")){
            GameObject[] centipede = GameObject.FindGameObjectsWithTag("Centipede");
            // After centipede body get destroyed to more than one part check collide each part
            for (int i = 0; i < centipede.Length; i++){                
                if (i < order){
                    centipede[i].GetComponent<Centipede>().collideEast = collideEast == true ? false : true;
                }
            }
            Destroy(gameObject);
            player.score += scoreCentipede;                                            // After body of centipede get destroyed then add score
        }        
    }
}


