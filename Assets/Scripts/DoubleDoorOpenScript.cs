using UnityEngine;

public class GateOpenScript : MonoBehaviour
{
    public AnimationCurve openSpeedCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0), new Keyframe(1, 0, 0, 0) });
    public float openSpeedMultiplier = 2.0f;
    public float FirstChildrenOpenAngle = 90.0f;
    public float SecondChildrenOpenAngle = 90.0f;

    bool open = false; 
    bool enter = false;

    float defaultRotationAngleFirst;
    float currentRotationAngleFirst;
    float defaultRotationAngleSecond;
    float currentRotationAngleSecond;
    float openTime = 0;

    void Start()
    {
        defaultRotationAngleFirst = transform.GetChild(0).eulerAngles.y;
        currentRotationAngleFirst = transform.GetChild(0).eulerAngles.y;

        defaultRotationAngleSecond = transform.GetChild(1).eulerAngles.y;
        currentRotationAngleSecond = transform.GetChild(1).eulerAngles.y;
        GetComponent<Collider>().isTrigger = true;
    }
    void Update()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeedMultiplier * openSpeedCurve.Evaluate(openTime);
        }
        transform.GetChild(0).eulerAngles = new Vector3(transform.GetChild(0).eulerAngles.x, Mathf.LerpAngle(currentRotationAngleFirst, defaultRotationAngleFirst + (open ? FirstChildrenOpenAngle : 0), openTime), transform.GetChild(0).eulerAngles.z);
        transform.GetChild(1).eulerAngles = new Vector3(transform.GetChild(1).eulerAngles.x, Mathf.LerpAngle(currentRotationAngleSecond, defaultRotationAngleSecond + (open ? SecondChildrenOpenAngle : 0), openTime), transform.GetChild(1).eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.F) && enter)
        {
            open = !open;
            currentRotationAngleFirst = transform.GetChild(0).eulerAngles.y;
            currentRotationAngleSecond = transform.GetChild(1).eulerAngles.y;
            openTime = 0;
        }
    }

    void OnGUI()
    {
        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 155, 30), "Press 'F' to " + (open ? "close" : "open") + " the door");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
        }
    }
}