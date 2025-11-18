namespace Library.Characters
{
    /// <summary>
    /// Clase base abstracta para todos los personajes de la Tierra Media
    /// Aplica principio SRP: maneja vida, items y cálculos de combate
    /// </summary>
    public abstract class Personaje
    {
        private int vidaActual;
        private List<IItem> items;

        public string Nombre { get; set; }
        public int VidaMaxima { get; private set; }
        
        public int VidaActual 
        { 
            get => vidaActual;
            protected set => vidaActual = Math.Max(0, Math.Min(value, VidaMaxima)); 
        }

        protected Personaje(string nombre, int vidaMaxima)
        {
            Nombre = nombre;
            VidaMaxima = vidaMaxima;
            VidaActual = vidaMaxima;
            items = new List<IItem>();
        }

        // Gestión de items (Patrón Composite)
        public void AgregarItem(IItem item)
        {
            if (item != null && !items.Contains(item))
            {
                items.Add(item);
            }
        }

        public void QuitarItem(IItem item)
        {
            items.Remove(item);
        }

        public IReadOnlyList<IItem> ObtenerItems()
        {
            return items.AsReadOnly();
        }

        // Parte 3: Cálculo de ataque total
        /// <summary>
        /// Suma el valor de ataque de todos los items equipados
        /// Decisión de diseño: delegamos el cálculo a los items (principio Tell, Don't Ask)
        /// </summary>
        public int ObtenerAtaqueTotal()
        {
            int ataqueTotal = 0;
            foreach (var item in items)
            {
                ataqueTotal += item.ValorAtaque;
            }
            return ataqueTotal;
        }

        // Parte 3: Cálculo de defensa total
        /// <summary>
        /// Suma el valor de defensa de todos los items equipados
        /// </summary>
        public int ObtenerDefensaTotal()
        {
            int defensaTotal = 0;
            foreach (var item in items)
            {
                defensaTotal += item.ValorDefensa;
            }
            return defensaTotal;
        }

        // Parte 3: Atacar a otro personaje
        /// <summary>
        /// Ataca a otro personaje considerando la defensa del objetivo
        /// Decisión: el daño final = ataque del atacante - defensa del defensor
        /// Si la defensa es mayor, el daño mínimo es 0
        /// </summary>
        public void Atacar(Personaje objetivo)
        {
            if (objetivo == null) return;

            int miAtaque = this.ObtenerAtaqueTotal();
            int suDefensa = objetivo.ObtenerDefensaTotal();
            int dañoFinal = Math.Max(0, miAtaque - suDefensa);

            objetivo.RecibirDaño(dañoFinal);
        }

        // Método auxiliar para recibir daño
        protected void RecibirDaño(int daño)
        {
            VidaActual -= daño;
        }

        // Parte 3: Curar al personaje
        /// <summary>
        /// Restaura la vida del personaje a su máximo
        /// </summary>
        public void Curar()
        {
            VidaActual = VidaMaxima;
        }

        public bool EstaVivo()
        {
            return VidaActual > 0;
        }
    }
}