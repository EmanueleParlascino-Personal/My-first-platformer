using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject coinSpawn;
    [SerializeField] int spawnNum = 10;
    [SerializeField] Sprite openedChestSprite;
    SpriteRenderer mySprite;

    bool hasSpawned = false;
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag =="Player" && !hasSpawned)
        {
            mySprite.sprite = openedChestSprite;
            for (int i = 0; i<= spawnNum; i++)
            {
                Vector3 position = transform.position + new Vector3(0,2,0);
                GameObject obj = (GameObject)Instantiate(coinSpawn, position, transform.rotation);
                obj.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(-10, 10),Random.Range(-20,20));
            }
            hasSpawned = true;
        }
    }
}
