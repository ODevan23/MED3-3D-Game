using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
          
            gameManager.GameOver("You successfully escaped!");
        }
    }
}
