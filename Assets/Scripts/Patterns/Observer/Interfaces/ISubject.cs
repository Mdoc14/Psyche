namespace Interfaces
{
    //Interfaz de los sujetos
    public interface ISubject<T> 
    {
        //Metodos a implementar
        public void AddObserver(IObserver<T> observer);
        public void RemoveObserver(IObserver<T> observer);
        public void NotifyObservers();
    }
}