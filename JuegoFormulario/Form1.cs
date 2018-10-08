using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoFormulario
{
    public partial class Form1 : Form
    {
        Juego jugando; // Uso la clase con la logica del juego
        List<int> secActual; // Guardare la ultima secuencia generada
        Color[] colores = new Color[] { Color.FromArgb(128, 128, 255),
                                        Color.FromArgb(255, 128, 128),
                                        Color.FromArgb(255, 192, 128),
                                        Color.FromArgb(255, 255, 128),
                                        Color.FromArgb(128, 255, 128),
                                        Color.MediumPurple}; // Contiene la lista de colores
        int tiempoDeRespuesta; // Es el tiempo que tiene el usuario para responder

        #region Constructores
        public Form1()
        {
            InitializeComponent();
            CambioBotones(false); 
        }

        public void Incializador()
        {
            jugando = new Juego();
            secActual = new List<int>();
            tiempoDeRespuesta = 10;
            HacerColoresAleatorios();
        }
        #endregion

        #region MenuStrip
        /** Estas funciones contienen el desarrollo de las opciones del menu strip **/

        // Inciar el juego con parametros por defecto y comienza con una serie
        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TomarDecision("INICIAR"))
            {
                Incializador();
                HacerSerie();
                label1.Text = jugando.MostrarListaSecuencias();
            }
        }

        // Termina el juego saliendo del formulario
        private void terminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TomarDecision("TERMINAR"))
            {
                Application.Exit();
            }
        }
        #endregion

        #region Funcion Auxiliar
        /* TomarDecision: es una funcion que de acuerdo al string correspondiente devuelve
         * true o false para Inciar o Salir del juego según la decisión del dialogo result
         */
        public bool TomarDecision(string decision)
        {
            DialogResult dr = MessageBox.Show("¿Quiere " + decision + " el juego?", decision + " juego", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.Yes == dr)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Colores de Botones
        /** Estas funciones permiten cambiar el color de los botones y habiliatarlos**/

        /* CambioBotones: habilita los botones segun el parametro bool que se le pase y
         * en ambos casos de que sea true o false los pinta de blanco a todos los botones
         */
        public void CambioBotones(bool activar)
        {
            foreach(Button btn in Controls.OfType<Button>())
            {
                if (activar)
                {
                    btn.Enabled = true;
                }
                else
                {
                    btn.Enabled = false;
                }
                btn.BackColor = Color.White;
            }
        }

        /* HacerColoresAleatorios: permite que cada vez que se inicie el juego
         * se cambie de lugar los colores de los botones.
         */
        public void HacerColoresAleatorios()
        {
            Random rnd = new Random();
            List<int> coloresElegidos = new List<int>();
            int numero = rnd.Next(0, 6);
            coloresElegidos.Add(numero);
            for(int i = 0; i < 5; i++)
            {
                do 
                { 
                    numero = rnd.Next(0, 6);
                } while(coloresElegidos.Contains(numero));
                coloresElegidos.Add(numero);
            }
            Color aux = new Color();
            int cont = 0;
            foreach(int item in coloresElegidos)
            {
                aux = colores[cont];
                colores[cont] = colores[item];
                colores[item] = aux;
            }
        }

        /* PintarBotones: pinta el boton segun el numero de indice que se le pase y ademas
         * llama a un timer para despintarlos segun sea necesario
         */
        public void PintarBotones(int item)
        {
            switch (item)
            {
                case 1:
                    button1.BackColor = colores[0];
                    timerApagarBtns.Start();
                    break;
                case 2:
                    button2.BackColor = colores[1];
                    timerApagarBtns.Start();
                    break;
                case 3:
                    button3.BackColor = colores[2];
                    timerApagarBtns.Start();
                    break;
                case 4:
                    button4.BackColor = colores[3];
                    timerApagarBtns.Start();
                    break;
                case 5:
                    button5.BackColor = colores[4];
                    timerApagarBtns.Start();
                    break;
                case 6:
                    button6.BackColor = colores[5];
                    timerApagarBtns.Start();
                    break;
            }
        }

        /*
        public void PintarBotones(int item)
        {

            switch (item)
            {
                case 1:
                    button1.BackColor = Color.FromArgb(128, 128, 255);
                    timerApagarBtns.Start();
                    break;
                case 2:
                    button2.BackColor = Color.FromArgb(255, 128, 128);
                    timerApagarBtns.Start();
                    break;
                case 3:
                    button3.BackColor = Color.FromArgb(255, 192, 128);
                    timerApagarBtns.Start();
                    break;
                case 4:
                    button4.BackColor = Color.FromArgb(255, 255, 128);
                    timerApagarBtns.Start();
                    break;
                case 5:
                    button5.BackColor = Color.FromArgb(128, 255, 128);
                    timerApagarBtns.Start();
                    break;
                case 6:
                    button6.BackColor = Color.MediumPurple;
                    timerApagarBtns.Start();
                    break;
            }
        }*/
        #endregion

        #region Logica del Juego en el Formulario
        /** Estas funciones van a utilizar la logica de la clase juego y de la variable
            jugando, influyendo en los mensajes al usuario y activando los timer y Message
            Boxs correspondientes. **/

        /* HacerSerie: permite armar la secuencia siempre y cuando el juego no llegue al
         * nivel 7. Estas secuencias aleatorias al guardarse en una lista de secuencias, 
         * siempre es conveniente ver y poder utilizar la ultima secuencia entonces a la
         * secuencia actual guardamos la ultima secuencia de lista de secuencias del jugando.
         * Además inicializa el timer de Mostrar Secuencias para pintar los botones correctos.
         */
        public void HacerSerie()
        {
            if (jugando.Niveles < 7)
            {
                jugando.ArmarSecuencia();
                secActual = jugando.ListaDeSecuencia[jugando.CantSec() - 1];
                timerMostrarSec.Start();
            }    
        }

        /* VerificarBotones: si la cantidad de enteros en la secuencia generada es distinta
         * a la del usuario entonces espero hasta que sea igual. Cuando sean iguales entonces
         * guardo en una variable auxiliar el resultado de la comparacion de estas secuencias
         * si este es true y los niveles son menor a 6 entonces el usuario acerto y continua, 
         * pero si el aux es falso entonces el usuario no acerto y pierde, y en el caso que
         * se haya llegado a los 6 niveles acertando se muestra el mensaje de Ganador.
         * Frena el tiempo de respuesta y resetea su label
         */
        public void VerificarBotones()
        {
            if (jugando.CantSec()==jugando.CantUsuar())
            {
                bool band = jugando.CompararSecuencias();
                timerRespuesta.Stop();
                labelSegundero.Text = "-";

                if (band && jugando.Niveles < 6)
                {
                    MessageBox.Show("Has acertado, continuemos...", "Acertado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HacerSerie();
                    jugando.LimpiarSecuencias();
                    label1.Text = jugando.MostrarListaSecuencias();
                    label2.Text = "-";
                    tiempoDeRespuesta = 10;
                }
                else if(!band)
                {
                    MessageBox.Show("Has perdido, lo siento :(", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CambioBotones(false);
                    label1.Text = "-";
                    label2.Text = "-";
                }
                else
                {
                    MessageBox.Show("Has ganado, felicitaciones :)", "Ganaste", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CambioBotones(false);
                    label1.Text = "-";
                    label2.Text = "-";
                }     
            }
        }


        #endregion

        #region Decisiones sobre los Botones de Colores
        /** Cada uno de los botones tiene la logica de agregar segun le corresponda su
            numero entero a la secuencia del usuario, mostrar (opcionalmente) dicha 
            secuencia y verificar que la secuencia sea la misma que la generada.**/
        private void buttonAzul_Click(object sender, EventArgs e)
        {
            jugando.SecuenciaUsuario(1);
            label2.Text = jugando.MostrarListaUsuario();
            VerificarBotones();
        }

        private void buttonRojo_Click(object sender, EventArgs e)
        {
            jugando.SecuenciaUsuario(2);
            label2.Text = jugando.MostrarListaUsuario();
            VerificarBotones();
        }

        private void buttonNaranja_Click(object sender, EventArgs e)
        {
            jugando.SecuenciaUsuario(3);
            label2.Text = jugando.MostrarListaUsuario();
            VerificarBotones();
        }

        private void buttonAmarillo_Click(object sender, EventArgs e)
        {
            jugando.SecuenciaUsuario(4);
            label2.Text = jugando.MostrarListaUsuario();
            VerificarBotones();
        }

        private void buttonVerde_Click(object sender, EventArgs e)
        {
            jugando.SecuenciaUsuario(5);
            label2.Text = jugando.MostrarListaUsuario();
            VerificarBotones();
        }

        private void buttonVioleta_Click(object sender, EventArgs e)
        {
            jugando.SecuenciaUsuario(6);
            label2.Text = jugando.MostrarListaUsuario();
            VerificarBotones();
        }
        #endregion

        #region Logica de timers
        int numeroSec = 0; // Para avanzar segun el tiempo

        /* El tick del timerMostrarSec verifica que si el numero de secuencia es menor
         * a la cantidad de la secuencia actual pinte los botones segun la posicion de
         * dicho numero, incremente ese numero y haga que el intervalo de tiempo se aumente
         * en 100 ms. Pero si no es menor el numero de secuencia a la cantidad de la secuencia
         * actual entonces debera iniciarse en cero el numero, se reestablece el valor del
         * intervalo en 1000 ms y se frena el tick.
         */
        private void timerMostrarSec_Tick(object sender, EventArgs e)
        {
            if(numeroSec < secActual.Count())
            {
                PintarBotones(secActual[numeroSec]);
                numeroSec++;
                timerMostrarSec.Interval += 100;
            }
            else
            {
                numeroSec = 0;
                timerMostrarSec.Interval = 1000;
                timerMostrarSec.Stop();
            }
        }

        /* El tick del timerApagarBtns verifica que si el numero de secuencia es igual
         * a la cantidad de la secuencia actual, habilite los botones para tocar y sino 
         * no deja que los botones queden habilitados. En ambos casos se detiene el tick.
         * Cuando se ha concretado la secuencia comienza el tercer timer de respuesta,
         * para que el usuario tenga la dificultad al responder.
         */
        private void timerApagarBtns_Tick(object sender, EventArgs e)
        {
            if(numeroSec == secActual.Count())
            {
                CambioBotones(true);
                timerRespuesta.Start();
                timerApagarBtns.Stop();
            }
            else
            {
                CambioBotones(false);
                timerApagarBtns.Stop();
            }
        }
        
        /* El tick de timerRespuesta me sirve para contar 10 segundos en cada respuesta
         * en caso de no cumplir, mandar un mensaje de tiempo acabado
         */
        private void timerRespuesta_Tick(object sender, EventArgs e)
        {
            if (tiempoDeRespuesta == 0)
            {
                labelSegundero.Text = "-"; 
                timerRespuesta.Stop();     
                MessageBox.Show("Se le acabó el tiempo", "Tiempo fuera", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CambioBotones(false);
            }
            else
            {
                labelSegundero.Text = tiempoDeRespuesta.ToString();
                tiempoDeRespuesta--;
            }
        }
        #endregion
    }
}
