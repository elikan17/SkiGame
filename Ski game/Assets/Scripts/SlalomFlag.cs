using UnityEngine;

public class SlalomFlag : MonoBehaviour
{
    public enum Direction { Left, Right };
    [SerializeField] private Direction flagDirection;
    private bool flagPassed = false;
    [SerializeField] private Material goodMat, badMat;
    public static event GameManager.TimerEvent RacePenalty;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.playerPos != null && PlayerController.playerPos.position.z < transform.position.z && !flagPassed)
        {
            flagPassed = true;
            Direction passingDirection = Direction.Right;
            if (PlayerController.playerPos.position.x < transform.position.x)
                passingDirection = Direction.Left;
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (passingDirection == flagDirection)
            {
                renderer.material = goodMat;
            }
            else
            { 
                renderer.material = badMat;
                RacePenalty.Invoke();
            }
        }
    }
}
