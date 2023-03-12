using UnityEngine;

public class DoorAutoClose : MonoBehaviour
{
    public AnimationCurve closeSpeedCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0), new Keyframe(1, 0, 0, 0) });
    public float closeSpeedMultiplier = 2.0f;
    public float doorcloseAngle = 90.0f;

    bool close = false;
    bool inArea = false;
    bool runOnece = false; 

    float defaultRotationAngle;
    float currentRotationAngle;
    float closeTime = 0;

    void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        if (closeTime < 1)
        {
            closeTime += Time.deltaTime * closeSpeedMultiplier * closeSpeedCurve.Evaluate(closeTime);
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (close ? doorcloseAngle : 0), closeTime), transform.localEulerAngles.z);

        if (runOnece == false && inArea)
        {
            close = !close;
            currentRotationAngle = transform.localEulerAngles.y;
            closeTime = 0;
            runOnece = true;
        }
    }

    void OnGUI()
    {
        if (inArea)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 155, 30), "uderzenie drzwiami");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
        }
    }
}