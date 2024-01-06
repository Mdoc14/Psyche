using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    public float lightOffTimerLimit; // Limite en segundos de la duracion del parpadeo
    public float lightIntervals; // Limite en el que se cambia entre encendido y apagado de la luz
    public float minInterval; // Valor minimo de intervalo
    public float maxInterval; // Valor maximo de intervalo
    public Light lightComponent; // Componente de la luz

    private float lightOnTimerLimit; // Frecuencia en segundos en el que comienza la animación de parpadeo
    private float lightOnTimer; // Timer para controlar cuando se hace la animacion 
    private float lightOffTimer; // Timer para controlar cuanto dura la animacion
    private float timerInterval; // Timer para controlar cuando se hace el cambio entre apagado y encendido durante el parpadeo
    private float originalIntensity; // Valor original de la intensidad de la luz 

    private void Awake()
    {
        originalIntensity = lightComponent.intensity; // Se guarda la intensidad original
        lightOnTimerLimit = Random.Range(minInterval, maxInterval + 1f); // Se asigna un numero aleatorio para realizar la animacion
    }
    void Update()
    {
        //Cuando lightOnTimer llega a su limite
        if (lightOnTimer > lightOnTimerLimit) 
        {
            // Cada lightIntervals(segundos entre intervalos) se cambia la intensidad de la luz
            if (timerInterval > lightIntervals) 
            { 
                // Si esta encendida, se apaga
                if(lightComponent.intensity == originalIntensity) 
                { 
                    lightComponent.intensity = 0;
                }
                // Si esta apagada, se enciende
                else 
                { 
                    lightComponent.intensity = originalIntensity;
                }
                // Independientemente de la accion, el timer se reinicia
                timerInterval = 0;
            }

            // Se comprueba que la duración de la animacion no ha terminado
            if (lightOffTimer > lightOffTimerLimit)
            {
                // Al haber terminado la animacion, se genera otro aleatorio, se reinician los timers y se le devuelve la intensidad original a la luz
                lightOnTimerLimit = Random.Range(minInterval, maxInterval + 1f);
                lightOnTimer = 0;
                lightOffTimer = 0;
                lightComponent.intensity = originalIntensity;
            }

            timerInterval += Time.deltaTime;
            lightOffTimer += Time.deltaTime;

        }

        lightOnTimer += Time.deltaTime;

    }
}
