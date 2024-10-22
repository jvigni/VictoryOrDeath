
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Minion target;

    public void SetTarget(Minion target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, .03f);
    }
}