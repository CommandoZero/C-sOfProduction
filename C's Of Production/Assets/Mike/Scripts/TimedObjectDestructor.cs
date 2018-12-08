using UnityEngine;

public class TimedObjectDestructor : MonoBehaviour {

    public float timeTilDestroy;
    public bool DisconnectChildren;

	void Update ()
    {

        if(timeTilDestroy <= 0)
        {
            if(DisconnectChildren)
            {
                transform.DetachChildren();
            }

            Destroy(gameObject);
        }
        else
        {
            timeTilDestroy -= Time.fixedUnscaledDeltaTime;
        }
	}
}
