using UnityEngine;
using System.Collections;

public class BuildingBlockBehaviour : MonoBehaviour {

    [HideInInspector] public Material currentColour;

    [Header("Movement Attributes")]
    [SerializeField] private float speed;
    private CharacterController cc;
    private bool inPlace;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!inPlace)
            Move();
    }

    void Move()
    {
        Vector3 _movementDirection = new Vector3(0, 0, 0);
        _movementDirection = transform.TransformDirection(_movementDirection);

        _movementDirection.y -= speed * Time.deltaTime;
        cc.Move(_movementDirection * Time.deltaTime);

        DetectGround();
    }

    void DetectGround()
    {
        
        Vector3 down = transform.TransformDirection(Vector3.down);
        Ray ray = new Ray(transform.position, down);
        //Debug.DrawRay(ray.origin, ray.direction, Color.green);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 10f))
        {
            float _dist = Vector3.Distance(transform.position, hit.transform.position);
            if(_dist < 1f)
            {
                inPlace = true;                
                transform.parent = hit.transform.parent.root;
                //transform.localPosition = new Vector3(hit.transform.position.x,
                //                                      hit.transform.position.y + 1f,
                //                                      hit.transform.position.z);

            }
        }

    }
}
