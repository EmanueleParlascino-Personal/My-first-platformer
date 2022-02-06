using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField] int timeDelay = 3;
    BoxCollider2D myCollider;


    private void Start() {
        myCollider = GetComponent<BoxCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            StartCoroutine(ExitLevel());
        }
        
    }
    IEnumerator ExitLevel()
    {

       yield return new WaitForSecondsRealtime(timeDelay);

       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    

}
