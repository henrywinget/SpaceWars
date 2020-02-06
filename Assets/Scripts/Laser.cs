using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // didn't quite work like I thought
        // transform.position = new Vector3(transform.position.x, transform.position.y + _speed, 0);
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }
    }
}
