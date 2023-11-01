using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer_VisiilityControl : MonoBehaviour, IObserver<float>
{
    //Atributos de la clase

    //Nos serviran de punteros a las salas actuales donde este el jugador
    private List<GameObject> salaActualSinParedes;
    private List<GameObject> salaActualConMuros;


    //Listas con las paredes y objetos que se desactivaran en cada sala para permitir una buena vision de la misma
    public List<GameObject> habitacionParedes = new List<GameObject>();
    public List<GameObject> pasilloParedes = new List<GameObject>();
    public List<GameObject> servicioParedes = new List<GameObject>();
    public List<GameObject> oficinaParedes = new List<GameObject>();
    public List<GameObject> pasilloP0Paredes = new List<GameObject>();
    public List<GameObject> cocinaParedes = new List<GameObject>();
    public List<GameObject> comedorParedes = new List<GameObject>();
    public List<GameObject> salonParedes = new List<GameObject>();

    //Listas con los muros que apareceran en la base del suelo de la sala donde este el jugador, permitiendo una buena vision
    public List<GameObject> habitacionMuros = new List<GameObject>();
    public List<GameObject> pasilloMuros = new List<GameObject>();
    public List<GameObject> servicioMuros = new List<GameObject>();
    public List<GameObject> oficinaMuros = new List<GameObject>();
    public List<GameObject> pasilloP0Muros = new List<GameObject>();
    public List<GameObject> cocinaMuros = new List<GameObject>();
    public List<GameObject> comedorMuros = new List<GameObject>();
    public List<GameObject> salonMuros = new List<GameObject>();

    //Listas con todos los objetos de cada piso, incluyendo suelos y paredes, esto para hacer los cambios de un piso a otro
    public List<GameObject> piso1GameObjects = new List<GameObject>();
    public List<GameObject> piso0GameObjects = new List<GameObject>();

    private void Awake()
    {
        //Buscamos al jugador y suscribimos esta clase a su sujeto
        GameObject.FindWithTag("Player").GetComponent<Subject_ActualRoom>().AddObserver(this);

        //Ocultamos la planta baja para que esta no se vea desde arriba
        visibilityOff(piso0GameObjects);

        //Ocultamos las paredes de la habitacion donde aparece el jugador
        visibilityOff(habitacionParedes);

        //Mostramos los muros que ocupan el lugar de las paredes ocultas
        visibilityOn(habitacionMuros);

        //Apuntamos a estas listas con los "punteros"
        salaActualSinParedes = habitacionParedes;
        salaActualConMuros = habitacionMuros;

    }

    //Recorre la lista y pone en false el atributo "enabled" del MeshRenderer. Ocultando los GameObject/Paredes que no queremos que se vean 
    public void visibilityOff(List<GameObject> lista)
    {
        foreach (GameObject obj in lista)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    //Recorre la lista y pone en true el atributo "enabled" del MeshRenderer. Mostrando los GameObject/Paredes que queremos que se vean
    public void visibilityOn(List<GameObject> lista)
    {
        foreach (GameObject obj in lista)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    
    //Realiza los cambios de visibilidad necesarias de las habitaciones cuando es llamada
    public void updateEnviroment(List<GameObject> salaSinParedes, List<GameObject> salaConMuros, List<GameObject> nextSalaSinParedes, List<GameObject> nextSalaConMuros)
    {
        //Muestra las paredes/GameObjects ocultadas de la sala donde estaba el jugador
        visibilityOn(salaSinParedes);

        //Oculta los muros de la sala donde estaba el jugador
        visibilityOff(salaConMuros);

        //Actualiza los "punteros"
        salaActualSinParedes = nextSalaSinParedes;
        salaActualConMuros = nextSalaConMuros;

        //Oculta las paredes/GameObjects visibles de la sala donde esta el jugador
        visibilityOff(nextSalaSinParedes);

        //Muestra los muros de la sala donde esta el jugador
        visibilityOn(nextSalaConMuros);
    }

    //Llamado desde el metodo Notify() del sujeto, en funcion del numero recibido hara la llamada a updateEnviroment con los parametros necesarios
    public void UpdateObserver(float room)
    {
        switch (room)
        {
            case 1: //Sala actual --> Habitacion

                updateEnviroment(salaActualSinParedes, salaActualConMuros, habitacionParedes, habitacionMuros);
                break;

            case 2: //Sala actual --> Pasillo

                updateEnviroment(salaActualSinParedes, salaActualConMuros, pasilloParedes, pasilloMuros);
                break;

            case 3: //Sala actual --> Servicio

                updateEnviroment(salaActualSinParedes, salaActualConMuros, servicioParedes, servicioMuros);
                break;

            case 4: //Sala actual --> Oficina

                updateEnviroment(salaActualSinParedes, salaActualConMuros, oficinaParedes, oficinaMuros);
                break;

            case 5: //Sala actual --> Pasillo planta baja

                updateEnviroment(salaActualSinParedes, salaActualConMuros, pasilloP0Paredes, pasilloP0Muros);
                break;

            case 6: //Sala actual --> Cocina

                updateEnviroment(salaActualSinParedes, salaActualConMuros, cocinaParedes, cocinaMuros);
                break;

            case 7: //Sala actual --> Comedor

                updateEnviroment(salaActualSinParedes, salaActualConMuros, comedorParedes, comedorMuros);
                break;

            case 8: //Sala actual --> Salon

                updateEnviroment(salaActualSinParedes, salaActualConMuros, salonParedes, salonMuros);
                break;

            case 9: //Cambio de piso, piso actual: planta baja

                //Ocultamos el piso donde se encontraba el jugador
                visibilityOff(piso1GameObjects);

                //Mostramos los GameObjects / Paredes del piso al que va el jugador
                visibilityOn(piso0GameObjects);

                //Mostramos los muros de la sala en la que estara el jugador en el nuevo piso
                visibilityOff(salaActualConMuros);

                //Actualizamos los "punteros"
                salaActualSinParedes = pasilloP0Paredes;
                salaActualConMuros = pasilloP0Muros;

                //Ocultamos los GameObject/Paredes de la sala actual y mostramos los muros del mismo
                visibilityOff(salaActualSinParedes);
                visibilityOn(salaActualConMuros);
                break;

            case 10: //Cambio de piso, piso actual: piso 1

                //Ocultamos el piso donde se encontraba el jugador
                visibilityOff(piso0GameObjects);

                //Mostramos los GameObjects / Paredes del piso al que va el jugador
                visibilityOn(piso1GameObjects);

                //Mostramos los muros de la sala en la que estara el jugador en el nuevo piso
                visibilityOff(salaActualConMuros);

                //Actualizamos los "punteros"
                salaActualSinParedes = pasilloParedes;
                salaActualConMuros = pasilloMuros;

                //Ocultamos los GameObject/Paredes de la sala actual y mostramos los muros del mismo
                visibilityOff(salaActualSinParedes);
                visibilityOn(salaActualConMuros);
                break;

            default:
                break;

        }
    }


}
