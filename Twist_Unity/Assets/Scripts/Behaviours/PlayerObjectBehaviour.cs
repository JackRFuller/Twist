using UnityEngine;
using System.Collections;

public class PlayerObjectBehaviour : MonoBehaviour {

    public static PlayerObjectBehaviour instance;

    [Header("Movement Attributes")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private AnimationCurve movementCurve;
    private int currentRotationID;
    private bool isMoving;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float timeStartedLerping;
        
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isMoving)
            TurnShape();
	
	}

    public void InitiateTurning(string _movementDirection)
    {

        if (!isMoving)
        {
            float _changeInRotation = transform.eulerAngles.y;

            if(_movementDirection == "Right")
            {
                _changeInRotation += 90;
            }
            else if(_movementDirection == "Left")
            {
                _changeInRotation -= 90;
            }

            SetupTurn(_changeInRotation);
        }
    }

    private void SetupTurn(float _changeInRotation)
    {
        startPosition = transform.rotation.eulerAngles;
        endPosition = new Vector3(transform.rotation.eulerAngles.x,
                                   _changeInRotation,
                                   transform.rotation.eulerAngles.z);

        timeStartedLerping = Time.time;
        isMoving = true;
    } 

    private void TurnShape()
    {
        float _timeSinceStarted = Time.time - timeStartedLerping;
        float _percentageComplete = _timeSinceStarted / movementSpeed;

        Vector3 _shapeRotation = Vector3.Lerp(startPosition, endPosition, movementCurve.Evaluate(_percentageComplete));
        transform.rotation = Quaternion.Euler(_shapeRotation);

        if(_percentageComplete >= 1.0f)
        {
            isMoving = false;
        }
    }
}
