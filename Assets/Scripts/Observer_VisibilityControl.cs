using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer_VisiilityControl : MonoBehaviour, IObserver<float>
{
    //Atributos de la clase

    private List<GameObject> salaActualSinParedes;
    private List<GameObject> salaActualConMuros;


    //Listas con las paredes que se desactivaran en cada sala
    public List<GameObject> habitacionParedes = new List<GameObject>();
    public List<GameObject> pasilloParedes = new List<GameObject>();
    public List<GameObject> servicioParedes = new List<GameObject>();
    public List<GameObject> oficinaParedes = new List<GameObject>();
    public List<GameObject> pasilloP0Paredes = new List<GameObject>();
    public List<GameObject> cocinaParedes = new List<GameObject>();
    public List<GameObject> comedorParedes = new List<GameObject>();
    public List<GameObject> salonParedes = new List<GameObject>();

    //Listas con los muros que apareceran en la base del suelo
    public List<GameObject> habitacionMuros = new List<GameObject>();
    public List<GameObject> pasilloMuros = new List<GameObject>();
    public List<GameObject> servicioMuros = new List<GameObject>();
    public List<GameObject> oficinaMuros = new List<GameObject>();
    public List<GameObject> pasilloP0Muros = new List<GameObject>();
    public List<GameObject> cocinaMuros = new List<GameObject>();
    public List<GameObject> comedorMuros = new List<GameObject>();
    public List<GameObject> salonMuros = new List<GameObject>();

    //Listas con todos los objetos de cada piso, incluyendo suelos y paredes
    public List<GameObject> piso1GameObjects = new List<GameObject>();
    public List<GameObject> piso0GameObjects = new List<GameObject>();

    private void Awake()
    {
        //Buscamos al jugador y suscribimos esta clase a su sujeto
        GameObject.FindWithTag("Player").GetComponent<Subject_ActualRoom>().AddObserver(this);

        quitRoom(habitacionParedes);
        chargeRoom(habitacionMuros);
        salaActualSinParedes = habitacionParedes;
        salaActualConMuros = habitacionMuros;

    }

    //Recorre la lista y pone en false el atributo "enabled" del MeshRenderer. Ocultando los GameObject que no queremos que se vean 
    public void quitRoom(List<GameObject> lista)
    {
        foreach (GameObject obj in lista)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    //Recorre la lista y pone en true el atributo "enabled" del MeshRenderer. Mostrando los GameObject que queremos que se vean
    public void chargeRoom(List<GameObject> lista)
    {
        foreach (GameObject obj in lista)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    
    public void updateEnviroment(List<GameObject> salaSinParedes, List<GameObject> salaConMuros, List<GameObject> nextSalaSinParedes, List<GameObject> nextSalaConMuros)
    {
        chargeRoom(salaSinParedes);
        quitRoom(salaConMuros);

        salaActualSinParedes = nextSalaSinParedes;
        salaActualConMuros = nextSalaConMuros;

        quitRoom(nextSalaSinParedes);
        chargeRoom(nextSalaConMuros);
    }
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

                quitRoom(piso1GameObjects);
                quitRoom(salaActualConMuros);

                salaActualSinParedes = pasilloP0Paredes;
                salaActualConMuros = pasilloP0Muros;

                chargeRoom(salaActualSinParedes);
                chargeRoom(salaActualConMuros);
                break;

            case 10: //Cambio de piso, piso actual: piso 1
                break;

            default:
                break;

        }
    }


}
