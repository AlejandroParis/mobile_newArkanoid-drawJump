using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public GameObject Platform;

    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 3f;

    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            float arc = Mathf.Rad2Deg * Mathf.Atan2(endPosition.y - startPosition.y, endPosition.x - startPosition.x);
            GameObject platform = Instantiate(Platform, new Vector3((endPosition.x + startPosition.x) / 2, (endPosition.y + startPosition.y) / 2, 0), Quaternion.Euler(new Vector3(0, 0, arc)));
            if(Vector2.Distance(startPosition, endPosition) > 1)
                platform.transform.localScale = new Vector3(Vector2.Distance(startPosition,endPosition), 1, 1);
            else
                platform.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
