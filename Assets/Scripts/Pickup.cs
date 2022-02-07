using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    [SerializeField] AudioClip pickSound;

    public int coinValue = 100;

    bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(pickSound, new Vector3(0,0,0));
            FindObjectOfType<GameSession>().PickupCoin(coinValue);
        }        
    }
}
