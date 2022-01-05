using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Player Attribute
    public float playerSpeed = 5f;                                              // Speed of player (number of cells per second)
    public int health = 3;                                                      // Number of player health
    public Text livesText;

    // Bullet Attribute
    public float bulletSpeed = 10f;                                             // Speed of Bullet (number of cells per second)
    public float fireRate = 0.5f;                                               // Fire rate of Bullet (second)
    private float nextFire;
    public GameObject bulletPrefab;
    
    // Score Attribute
    public int score;                                                           // Player Score
    public Text scoreText;

    // Map Attribute
    public GameObject gameOverUI;
    public GameObject pauseGameUI;
    private float nextMove = 0;
    private float mapLimit = 0.85f;                                             // Height limit that player can move up
    private Rigidbody2D rb;      
    private Vector3 moveDirection;                                              // Direction of player movement
                                                         
    private MapManager map;
    private Scene currentScene;         
        
    void Start(){
        health = PlayerPrefs.GetInt("PlayerHealth", health);
        currentScene = SceneManager.GetActiveScene();                                  
        rb = GetComponent<Rigidbody2D>();
        map = GameObject.Find("MapManager").GetComponent<MapManager>();
    }
    void Update(){    
        Movement();     
        PressFire();    
        livesText.text = "Lives : " + health.ToString();
        scoreText.text = "Score : " + score.ToString();     
        PauseGame();
        GameOver();                                                                 
    }    
                                                                                
    void Movement(){
        // Player move up
        if (CheckMovable(Vector3.up) && Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0){
            moveDirection = Vector3.up;
        }
        // Player move down
        else if (CheckMovable(Vector3.down) && Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0){
            moveDirection = Vector3.down;
        }
        // Player move to the left
        else if (CheckMovable(Vector3.left) && Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0){
            moveDirection = Vector3.left;
        }
        // Player move to the right
        else if (CheckMovable(Vector3.right) && Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0){
            moveDirection = Vector3.right;
        }
        else{
            moveDirection = Vector3.zero;
        }

        if (Time.time > nextMove){
            nextMove = Time.time + 1f / playerSpeed;
            rb.MovePosition(transform.position + moveDirection);
        }
    }

    bool CheckMovable(Vector3 moveDirection){
        // Check if the player faces with obstacle
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDirection, 1);
        foreach (RaycastHit2D h in hits){            
            if (!h.collider.gameObject.CompareTag("Player")){
                if (h.collider.gameObject.CompareTag("Centipede")){
                    health--;
                    PlayerPrefs.SetInt("PlayeLife", health);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene(currentScene.name);
                }
                return false;
            }
        }

        if(moveDirection == Vector3.up && transform.position.y + 1 > Math.Floor(mapLimit * map.gridSize)){
            return false;
        }
        return true;
    }

    // Fire Bullet from player
    void Fire(){         
        GameObject newBullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);    // Clone new bullet     
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = Vector2.up * bulletSpeed;                                                                           // Set speed of bullet
    }

    // Controller to fire bullet
    void PressFire(){
        if (fireRate == 0 && Input.GetButtonDown("Jump")){
            Fire();
        }        
        else{
            if (Input.GetButton("Jump") && Time.time > nextFire && fireRate > 0){
                nextFire = Time.time + fireRate;
                Fire();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision){        
        // When centipede collide with player then player dead
        if (collision.gameObject.CompareTag("Centipede")){
            Debug.Log("Player Health decreased by 1");
            health--;
            PlayerPrefs.SetInt("PlayerHealth", health);
            PlayerPrefs.Save();
            SceneManager.LoadScene(currentScene.name);                       
        }
    }

    void PauseGame(){
        // Pause game by press escape key
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Time.timeScale == 1){
                Time.timeScale = 0;
                pauseGameUI.SetActive(true);                
            }
            // Continue game
            else{
                Time.timeScale = 1;
                pauseGameUI.SetActive(false);
            }
        }        
    }

    void GameOver(){                 
        GameObject[] centipede = GameObject.FindGameObjectsWithTag("Centipede");
        // When player health decreased to 0 or player destroy all centipede body, game over
        if (health <= 0 || centipede.Length == 0){
           if(Time.timeScale == 1){
               Time.timeScale = 0;
               gameOverUI.SetActive(true);        
               // Press space bar to reload scene
               if (Input.GetKey(KeyCode.Space)){
                   SceneManager.LoadScene(currentScene.name);
                   health = 3;
                   PlayerPrefs.SetInt("PlayerHealth",health);
               }       
               // Press escape to back to menu
               if (Time.timeScale == 0 && Input.GetKey(KeyCode.Escape)){
                   SceneManager.LoadScene("Menu");
               }
           }           
        }
    }
}
