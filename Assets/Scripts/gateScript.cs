using UnityEngine;

public class gateScript : MonoBehaviour
{
    public int gateID;

    private TrackPositionController tps;
    
    void Start()
    {
        string[] p = gameObject.name.Split('_');
        gateID = int.Parse(p[1]);

        tps = gameObject.GetComponentInParent<TrackPositionController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        tps.currentGate = gateID;
    }
}
