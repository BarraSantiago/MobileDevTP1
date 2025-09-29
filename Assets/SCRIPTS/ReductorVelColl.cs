using UnityEngine;

public class ReductorVelColl : MonoBehaviour
{
    public float ReduccionVel;
    public string PlayerTag = "Player";
    private bool Usado;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == PlayerTag)
            if (!Usado)
                Chocado();
        //other.transform.GetComponent<AcelerAuto>().Chocar(this);
    }

    public virtual void Chocado()
    {
        Usado = true;
    }
}