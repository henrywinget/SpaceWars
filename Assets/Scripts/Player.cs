using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    [SerializeField]
    private int _health = 5;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    [SerializeField]
    private float _nextFire = -1f;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // take the current position and give it a new position (0, 0, 0) (x, y ,z)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // running movement scripts
        CalculateMovement();

        // firing the gun!!
        bool pressedSpaceAndNoCoolDown = Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire;
        if (pressedSpaceAndNoCoolDown)
        {
            FireLaser();
        }
        // a die
        if(_health == 0)
        {
            Destroy(this.gameObject);
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        // same as if y is greater that 0 or less than -3.8
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -11.2f, 11.2f), transform.position.x, 0);

        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        // my way
        _nextFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
        // answer
        // Instantiate(_laserPrefab, transform.position + new Vector3(0, 0 0.8f, 0), Quaternion.identity);
    }

    public bool IsAlive()
    {
        return _health > 0;
    }
    
    public void Damage()
    {
        _health -= 1;

        if (_health < 1)
        {
            // Communicate with spawn manager to stop spawning after death
            _spawnManager.StopSpawining();
            Destroy(this.gameObject);
        }
    }

}
