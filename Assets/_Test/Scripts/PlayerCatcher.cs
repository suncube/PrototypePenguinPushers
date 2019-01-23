using UnityEngine;

public class PlayerCatcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var enemyController = other.GetComponent<EnemyController>();
        if(!enemyController) return;

        enemyController.Catch(this);
    }

    private void OnTriggerExit(Collider other)
    {
        var enemyController = other.GetComponent<EnemyController>();
        if (!enemyController) return;

        enemyController.Free(this);
    }

}