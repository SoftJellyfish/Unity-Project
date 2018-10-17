using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour {

    float rotationSpeed = 100.0f;
    float thrustForce = 3f;

    public AudioClip crash;
    public AudioClip shoot;

    public GameObject bullet;

    private GameController gameController;

	// Use this for initialization
	void Start () {

        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();
	}

    void FixedUpdate() {

        transform.Rotate(0, 0, - Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        GetComponent<Rigidbody2D>().
            AddForce(transform.up * thrustForce * Input.GetAxis("Vertical"));

        if (Input.GetMouseButtonDown(0))
            ShootBullet();
    }

    void OnTriggerEnter2D(Collider2D c) {
        
        if (c.gameObject.tag != "Bullet") {

            AudioSource.PlayClipAtPoint(crash, Camera.main.transform.position);

            transform.position = new Vector3(0, 0, 0);

            GetComponent<Rigidbody2D>().
                velocity = new Vector3(0, 0, 0);

            gameController.DecrementLives();

        }
    }

    void ShootBullet() {

        Instantiate(bullet,
            new Vector3(transform.position.x, transform.position.y, 0),
                transform.rotation);

        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }

}
