using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cave1Trigger : MonoBehaviour {

    public Vector2 SpotInNextRoom;
    GameObject persistancescene;
    public GameManager sp;
    public string levelToLoad;
    public bool levelLoaded;

    void Start()
    {
        persistancescene = GameObject.Find("persistancescene");
        sp = persistancescene.GetComponent<GameManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.gameObject.name == "Player")
        {
            
            SceneManager.LoadScene(levelToLoad);
            sp.spawnLocation = SpotInNextRoom;
        }
      
    }
    public void OnDoorEntered()
    {
        GameManager.Instance.spawnLocation = SpotInNextRoom;
    }
}
