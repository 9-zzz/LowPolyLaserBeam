using UnityEngine;
using System.Collections;

public class LowPolyLaser : MonoBehaviour
{
    GameObject beamHitParticles;
    ParticleSystem bhp;
    public float beamRotationSpeed = 400.0f;
    public float beamExtendSpeed = 10.0f;
    public float zScaleFactor = 20.0f;
    public float distanceToHitPoint;

    void Awake()
    {
        // Make sure the particles system is the first child of THIS extendy beam cube.
        // And that the particle system does NOT Play on Awake!
        beamHitParticles = transform.GetChild(0).gameObject;
        bhp = beamHitParticles.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.Rotate(0, 0, (Time.deltaTime * beamRotationSpeed));

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {

            transform.localScale = Vector3.Lerp(transform.localScale,
                new Vector3(1, 1, (distanceToHitPoint * zScaleFactor)), (beamExtendSpeed * Time.deltaTime));

            beamHitParticles.transform.position = hit.point;
            beamHitParticles.transform.LookAt(transform.position);

            distanceToHitPoint = Vector3.Distance(transform.position, hit.point);
        }
        else
        {
            // Oops we hit nothing, scale the laser back and stop the particles!
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), (beamExtendSpeed * Time.deltaTime));
            bhp.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        bhp.Play();
    }

}
