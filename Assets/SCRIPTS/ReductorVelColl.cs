using UnityEngine;

public class ReductorVelColl : MonoBehaviour
{
    public float reduccionVel;
    public string playerTag = "Player";
    private bool _usado;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == playerTag)
            if (!_usado)
                Chocado();
        //other.transform.GetComponent<AcelerAuto>().Chocar(this);
    }

    public virtual void Chocado()
    {
        _usado = true;
    }
}