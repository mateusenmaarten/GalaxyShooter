using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 10f;

    void Update()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
        checkToDestroy();
    }

    void checkToDestroy()
    {
        if (transform.position.y > 10f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
}
