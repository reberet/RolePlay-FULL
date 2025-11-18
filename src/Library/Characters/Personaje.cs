namespace Library.Personajes; 
{
    // Esta clase va a aplicar para todos los personajes del juego, todos los personajes la van a usar de base
    public abstract class Personaje
    {
        public string Nombre { get; set; }
        private int SaludActual; // como lo dice el nombre
        public int SaludMaxima { get; set; }
        private List<IItem> items;

        public int SaludActual
        {
            get => SaludActual;
        }
        
    
        
        
        
        
        
        
        
        
        
        
        
        
    }
}