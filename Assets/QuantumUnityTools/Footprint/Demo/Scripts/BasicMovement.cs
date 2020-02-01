using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float defaultSpeed;

    [SerializeField]
    private float rotationSpeed = 8f;


    private GameObject mainCamera;
    private Transform dogModel;

    Vector3 direction;
    void Start()
    {
        mainCamera = Camera.main.gameObject;
        //dependent on the model being the first child bad
        dogModel = transform.GetChild(0);
        //dependent on the indicator being the second child bad
    }

    void Update()
    {
        dogModel.transform.localEulerAngles = new Vector3(0, dogModel.transform.localEulerAngles.y, dogModel.transform.localEulerAngles.z);

        direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }

        direction.x = Input.GetAxis("Horizontal"); 
        direction.z = Input.GetAxis("Vertical"); 

        direction.Normalize();
        direction *= Time.deltaTime * defaultSpeed;

        if (direction != Vector3.zero)
        {
            //I hope this line of code gets Aids and dies
            Vector3 hori = direction.z * new Vector3(mainCamera.transform.forward.normalized.x, 0, mainCamera.transform.forward.normalized.z);
            Vector3 vert = direction.x * new Vector3(mainCamera.transform.right.normalized.x, 0, mainCamera.transform.right.normalized.z);
            gameObject.transform.Translate((hori + vert) * currentSpeed * Time.deltaTime);

            //Rotate to movement direction.
            dogModel.rotation = Quaternion.LookRotation(Vector3.Lerp(dogModel.forward, (hori + vert), Time.deltaTime * rotationSpeed));


            currentSpeed = defaultSpeed;
        }
    }
}
