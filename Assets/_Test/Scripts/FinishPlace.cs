using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinishPlace : MonoBehaviour
{
    [SerializeField]
    private Animator m_Aminator;

    public Action<EnemyController> OnFinished;

    private void OnTriggerEnter(Collider other)
    {
        var enemyController = other.GetComponent<EnemyController>();
        if (!enemyController) return;

     
        if (OnFinished != null)
        {
            OnFinished.Invoke(enemyController);
            Debug.Log(other.gameObject.name + " Catched ");
            m_Aminator.Play("Action");
        }
    }

}
