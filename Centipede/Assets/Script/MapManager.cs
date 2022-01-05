using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Create grid map
    public int gridSize = 30;                                                            // Size of the grid
    public GameObject cam;                                                               // Main camera

    // Create centipede in map
    public int centipedeLength = 15;                                                     // Length of the centipede body 
    public GameObject centipedePrefab;

    // Create mushroom in map
    public int mushroomNum = 30;                                                         // Number of mushroom in map
    public GameObject  mushroomPrefab;


    void Start(){
        // Set camera position of grid map
        float newPos = (gridSize / 2f) - 0.5f;
        Vector2 verticalOffset = new Vector2(newPos + 1, 0);
        Vector2 verticalSize = new Vector2(1, gridSize + 2);
        Vector2 horizontalOffset = new Vector2(0, newPos + 1);
        Vector2 horizontalSize = new Vector2(gridSize + 2, 1);

        CameraManager(newPos);
        WallManager(newPos, "North", horizontalOffset, horizontalSize);                  // Setup north wall
        WallManager(newPos, "South", -horizontalOffset, horizontalSize);                 // Setup south wall
        WallManager(newPos, "West", -verticalOffset, verticalSize);                      // Setup west wall
        WallManager(newPos, "East", verticalOffset, verticalSize);                       // Setup east wall
        CreateMushroom();
        CreateCentipede();        
    }

    // Setup Camera 
    void CameraManager(float newPos){
        transform.position = new Vector3(newPos, newPos, 1);
        cam.transform.position = new Vector3(newPos, newPos, -10);                       // Transform camera position
        transform.localScale = new Vector3(gridSize/10f, gridSize/10f, gridSize/10f);    // Resize the background         
        Camera camComponent = cam.GetComponent<Camera>();
        camComponent.orthographicSize = gridSize/2f;
    }

    // Create the walls that use for being the edge of map
    void WallManager(float newPos, string name, Vector2 offset, Vector2 size){
        GameObject wall = GameObject.Find(name);                                         // Find name Gameobject(North, South, East, West)
        wall.transform.position = new Vector3(newPos, newPos, 0);
        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();                     // Add Collider when Gameobject collide with wall
        collider.offset = offset;                                                        // Collider offset position
        collider.size = size;                                                            // Size of Collider
    }
    
    // Create centipede in map
    void CreateCentipede(){
        for (int i = 0; i < centipedeLength; i++){
            Vector3 nextPos = new Vector3(i, gridSize - 1, 0);
            GameObject newCentipede = Instantiate(centipedePrefab, nextPos, Quaternion.identity);
            newCentipede.GetComponent<Centipede>().order = i;
            newCentipede.name = "Centipede " + i;
        }
    } 
    void CreateMushroom(){
        int count = 0;
        while (count < mushroomNum){
            // Random position of mushroom in map
            int x = Random.Range(0, gridSize- 1);            
            int y = Random.Range(2, gridSize - 1);                                       // Leave 2 rows for player    
            Instantiate(mushroomPrefab, new Vector3(x, y, 0), Quaternion.identity);
            count++;
        }
    }
    

}
