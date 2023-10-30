namespace Interfaces
{
    //Interfaz de los observadores
    public interface IObserver<T>   
    {

    //Metodos a implementar
        public void UpdateObserver(T info); 
    }
}