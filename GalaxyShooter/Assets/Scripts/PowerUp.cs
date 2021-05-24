using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    [SerializeField]
    private int Id;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
   
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (Id)
                {
                    case 0: 
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.LogError("PowerUp does not exist");
                        break;
                }
            }

            Destroy(this.gameObject);
        } 
    }
}
