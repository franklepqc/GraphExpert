namespace GraphExpert.Wpf.Models
{
    public class Stop
    {
        private Stop()
        {
            
        }

        public Stop(byte id)
            : this(id, id.ToString())
        {
        }

        public Stop(byte id, string etiquette)
        {
            Id = id;
            Etiquette = etiquette;
        }

        public byte Id { get; set; }

        public string Etiquette { get; set; }
    }
}
