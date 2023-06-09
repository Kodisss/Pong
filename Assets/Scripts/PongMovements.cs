using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PongMovements : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Moving Variables")]
    [SerializeField] private float speed = 5f;

    private Vector3 input;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // gets the input from whatever controller
    private void GatherInput()
    {
        input = new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
    }

    private void Move()
    {
        rb.MovePosition(transform.position + speed * Time.deltaTime * input);
    }
}
