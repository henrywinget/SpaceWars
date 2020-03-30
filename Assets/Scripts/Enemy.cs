using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
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
            int rInt = r.Next(-8, 8); //for ints
            // solution
            // flaot rInt = Random.Range(-8f, 8f);
            transform.position = new Vector3(rInt, 8, 0);
        }

        if(!_player.IsAlive())
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is Player, destroy this and damage the player
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        // other.transform.name
        // if other is laser, destroy this and destroy laser
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
