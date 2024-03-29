using UnityEngine;
 using System.Collections;

 public class UnscaledTimeParticle : MonoBehaviour
 {
    ParticleSystem particleSystem;
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
     {
         if (Time.timeScale < 0.01f)
         {
             particleSystem.Simulate(Time.unscaledDeltaTime, true, false);

         }
     }
 }