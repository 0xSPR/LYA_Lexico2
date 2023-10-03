namespace LYA_Lexico2
{
    public class Token
    {
        public enum Tipos
        {
            Identificador,Numero,Caracter, Inicializacion, Asignacion,
            OpRelacional, OpTermino, OpFactor, InTermino,InFactor, Cadena,
            OpTerneario, FinSentencia, OpLogico, Inicio, Fin
        }
        private string contenido;
        private Tipos  clasificacion;
        public Token()
        {
            contenido = "";
            clasificacion = Tipos.Identificador;
        }
        public void setContenido(string contenido)
        {
            this.contenido = contenido;
        }
        public void setClasificacion(Tipos clasificacion)
        {
            this.clasificacion = clasificacion;
        }
        public string getContenido()
        {
            return this.contenido;
        }
        public Tipos getClasificacion()
        {
            return this.clasificacion;
        }
    }
}