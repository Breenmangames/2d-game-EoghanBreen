using UnityEngine;
using UnityEngine.Rendering;

public class SpikeScript : MonoBehaviour
{

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    GameObject player;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(player);
        }
    }

    
}
