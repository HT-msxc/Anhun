using UnityEngine;

public class Shark : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            IGetHurt getHurt = other.GetComponent<IGetHurt>();
            Debug.Log(getHurt == null);
            getHurt.GetHurt(transform);
        }
    }
}