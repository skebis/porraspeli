using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushController : MonoBehaviour {

    public int state;
    public GameObject player;
    public GameObject cam;
    public GameObject stairs;
    public Canvas canv;
    public Text pointsText;
    public int points;

    private GameObject colObj;
    private Rigidbody[] rbs;
    private Rigidbody rb;
    private Vector3 startingCameraPos = new Vector3(0.6f, 14.5f, -15.5f);
    private Vector3 endingCameraPos = new Vector3(-8f,3.5f,6f);
    private Vector3 pushForce = new Vector3(0.0f, 0.0f, 300.0f);
    private RigidbodyConstraints[] originalConstrains = new RigidbodyConstraints[100];
    private Transform playerPos;

    // Use this for initialization
    void Start () {

        canv = GetComponent<Canvas>();
        pointsText = Canvas.FindObjectOfType<Text>();
        cam = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        stairs = GameObject.FindWithTag("Portaat");
        rbs = GetComponents<Rigidbody>();

        state = 0;
        points = 0;
        pointsText.text = "Points: " + points.ToString();
        cam.transform.position = startingCameraPos;

        //Physics.

        for (int i = 0; i < rbs.Length; i++)
        {
            originalConstrains[i] = rbs[i].constraints;
            rbs[i].constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    // Update is called once per frame
    void Update() {
        switch (state)
        {
            case 0:
                if (Input.GetButtonDown("Fire1"))
                {
                    state = 1;
                    rbs = GetComponents<Rigidbody>();
                    for (int i = 0; i < rbs.Length; i++)
                    {
                        rbs[i].constraints = originalConstrains[i];
                    }
                    rb = player.GetComponent<Rigidbody>();
                    rb.AddForce(pushForce);
                    MoveCamera();
                }
                break;
            case 1:
                UpdateCamera();
                break;
        }
    }

    // Checks every collision.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Porras")
        {
            string hitter = collision.collider.tag;
        }
        //Collider collider = collision.contacts[0].thisCollider;
        //pointsText.text = collider.name;
    }

    // Moves the camera to a different position once the push-impact is done.
    private void MoveCamera() {
        cam.transform.position = endingCameraPos;
    }

    // Changes the direction of the camera towards the player.
    private void UpdateCamera() {
        playerPos = player.transform;
        cam.transform.LookAt(playerPos);
    }
}
