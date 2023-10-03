namespace LYA_Lexico2
{
    public class Lexico : Token, IDisposable
    {
        private StreamReader archivo;
        private StreamWriter log;
        public Lexico()
        {
            archivo = new StreamReader("prueba.cpp");
            log = new StreamWriter("prueba.log");
            log.AutoFlush = true;
        }
        public Lexico(string nombre)
        {
            archivo = new StreamReader(nombre);
            log = new StreamWriter("prueba.log");
            log.AutoFlush = true;
        }
        public void Dispose()
        {
            archivo.Close();
            log.Close();
        }
        public void nextToken()
        {
            char c;
            string buffer = "";

            int estado = 0;

            const int F = -1;
            const int E = -2;

            while (estado >= 0)
            {
                c = (char)archivo.Peek();
                switch (estado)
                {
                    case 0:
                        if (char.IsWhiteSpace(c))
                            estado = 0;
                        else if (char.IsLetter(c))
                            estado = 1;
                        else if (char.IsDigit(c))
                            estado = 2;
                        else if (c == '=') // Asignacion
                            estado = 9;
                        else if (c == '!')
                            estado = 11;
                        else if (c == '<' || c == '>' || c == '!') // OpFactor
                            estado = 10;
                        else if (c == '+' || c == '-') // OpTermino
                            estado = 12;
                        else if (c == '*' || c == '%' || c == '/')
                            estado = 14;
                        else if (c == '?')
                            estado = 17;
                        else
                            estado = 8;
                        break;
                    case 1:
                        setClasificacion(Tipos.Identificador);
                        if (!char.IsLetterOrDigit(c))
                            estado = F;
                        break;
                    case 2:
                        setClasificacion(Tipos.Numero);
                        if (char.IsDigit(c))
                            estado = 2;
                        else if (c == '.')
                            estado = 3;
                        else if (char.ToLower(c) == 'e')
                            estado = 5;
                        else
                            estado = F;
                        break;
                    case 3:
                        if (char.IsDigit(c))
                            estado = 4;
                        else
                            estado = E;
                        break;
                    case 4:
                        if (char.IsDigit(c))
                            estado = 4;
                        else if (char.ToLower(c) == 'e')
                            estado = 5;
                        else
                            estado = F;
                        break;
                    case 5:
                        if (char.IsDigit(c))
                            estado = 7;
                        else if (c == '+' || c == '-')
                            estado = 6;
                        else
                            estado = E;
                        break;
                    case 6:
                        if (char.IsDigit(c))
                            estado = 7;
                        else
                            estado = E;
                        break;
                    case 7:
                        if (!char.IsDigit(c))
                            estado = F;
                        break;
                    case 8:
                        setClasificacion(Tipos.Caracter);
                        if (c == '|')
                            estado = 11;
                        else if (c == '*' || c == '/')
                            estado = 14;
                        else if (c == '\"' || char.IsLetterOrDigit(c))
                            estado = 16;
                        else
                            estado = F;
                        break;
                    case 9:
                        setClasificacion(Tipos.Asignacion);
                        if(c == '=')
                            estado = 10;
                        else
                            estado = F;
                        break;
                    case 10:
                        setClasificacion(Tipos.OpRelacional);
                        estado = F;
                        break;
                    case 11:
                        setClasificacion(Tipos.OpLogico);
                        estado = F;
                        break;
                    case 12:
                        setClasificacion(Tipos.OpTermino);
                        if(c == '+' || c == '-')
                            estado = 13;
                        else
                            estado = F;
                        break;
                    case 13:
                        setClasificacion(Tipos.InTermino);
                            estado = F;
                        break;
                    case 14:
                        setClasificacion(Tipos.OpFactor);
                        if(c == '=')
                            estado = 15;
                        else
                            estado = F;
                        break;
                    case 15:
                        setClasificacion(Tipos.InFactor);
                        estado = F;
                        break;
                    case 16:
                        setClasificacion(Tipos.Cadena);
                        if(c != '\"')
                            estado = 8;
                        else
                            estado = F;
                        break;
                        case 17:
                            setClasificacion(Tipos.OpTerneario);
                            estado = F;
                            break;
                        
                }
                if (estado >= 0)
                {
                    if (estado > 0)
                    {
                        buffer += c;    
                    }
                    archivo.Read();
                }
            }
            setContenido(buffer);
            log.WriteLine(getContenido() + " = " + getClasificacion());
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}