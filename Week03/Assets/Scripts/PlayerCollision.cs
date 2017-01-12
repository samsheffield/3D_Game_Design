using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    public GameManager gameManager;                 // Drag the Game Manager GameObject from the Heirarchy to the Inspector
    public AudioSource itemSource, hitSource;       // Two AudioSource components used to play different types of audio independently
    public AudioClip gotItem, hitWall;              // Audioclips related to collision

    // When this collides with a collider marked set as a trigger...
    void OnTriggerEnter(Collider other)
    {
        // Check if it is an item. Be sure to add "Items" as a tag and apply it to your items. Watch out for spelling!
        if (other.CompareTag("Items"))
        {
            
            // Check if the other GameObject has an ItemControl component by trying to get it.
            ItemControl item = other.gameObject.GetComponent<ItemControl>();

            // If it exists...
            if(item != null)
            {
                itemSource.pitch = Random.Range(1f, 1.25f);     // Randomize the pitch a little
                itemSource.PlayOneShot(gotItem);                // Play appropriate audio
                item.Explode();                                 // Call the item's Explode method

                gameManager.Score(item.GetValue());             // Call the GameManager's Score method and pass the item's value with it's GetValue method
                other.gameObject.SetActive(false);              // Optional: Deactivate the GameObject instead of destroying it. May be better for performance if you're using it often.
            }
            else
            {
                Debug.LogError("GameObject is missing ItemControl component.");
            }
        }
        else if (other.CompareTag("Finish"))
        {
            // If the trigger's tag is "Finish", signal the end of the game to the Game Manager
        }
        else if (other.CompareTag("Kill"))
        {
            gameManager.Lose();
        }
    }

    // When colliding with another GameObject's collider (non-trigger)
    void OnCollisionEnter(Collision coll)
    {
        // Play a sound if it's tag matches "Walls"
        if (coll.gameObject.CompareTag("Walls"))
        {
            hitSource.PlayOneShot(hitWall);
        }
    }
}
