using System.Collections;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [SerializeField]
    private EnemySettings m_Settings;

    [SerializeField]
    private Rigidbody Rigidbody;

    private EnemyState enemyState;
    private float currentSpeed;
    private Vector3 targetRun;

    private void Awake()
    {
        MakeRandomRotate();
        currentSpeed = m_Settings.IdleSpeed;
    }

    public void Catch(PlayerCatcher player)
    {
        if(enemyState == EnemyState.Run) return;

        enemyState = EnemyState.Run;
        currentSpeed = m_Settings.RunSpeed;

        FindDirection(player);
    }

    public void Free(PlayerCatcher player)
    {
        enemyState = EnemyState.Free;
    }

    public void Idle()
    {
        enemyState = EnemyState.Idle;
        currentSpeed = m_Settings.IdleSpeed;
    }

    public void FixedUpdate()
    {
        Processing();
    }

    private void Processing()
    {
        if (GetDistance(transform.forward) < 2f)
        {
            MakeRandomRotate();

            if (enemyState == EnemyState.Free)
                Idle();
        }
        else
        {
            MoveForward();
        }


    }

    private Coroutine prevCoroutine;
    private void FindDirection(PlayerCatcher player)
    {
        var moveDir = transform.position - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        transform.rotation = rotation;

        if (prevCoroutine != null)
            StopCoroutine(prevCoroutine);

        prevCoroutine = StartCoroutine(FindRunDir());
    }

    IEnumerator FindRunDir()
    {
        while (GetDistance(transform.forward) < 2f)
        {
            Debug.Log("Rotate");
            MakeRandomRotate();

            yield return null;
        }
    }

    private void MakeRandomRotate()
    {
        var range = Random.Range(0, 360);
        transform.rotation *= Quaternion.Euler(0f, range, 0f);
    }

    private float GetDistance(Vector3 dir)
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, dir);

        // Cast a ray straight downwards.
        if (Physics.Raycast(downRay, out hit, 10, 1 << 0))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            return hit.distance;
        }

        return 10;
    }

    private void MoveForward()
    {
        Rigidbody.AddForce(transform.forward * currentSpeed);
    }

}

public enum EnemyState
{
    
    Idle,
    Free,
    Run
}