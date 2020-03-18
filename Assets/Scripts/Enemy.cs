using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // if bottom of screen, respawn at top wityh a new random x position
        if (transform.position.y < -8f)
        {
            System.Random r = new System.Random();
            int rInt = r.Next(-11, 11); //for ints
            // solution
            // flaot rInt = Random.Range(-8f, 8f);
            transform.position = new Vector3(rInt, 8, 0);
        }
    }
}
