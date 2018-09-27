using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Auditoria_V5
{
    public class CamaraViewModel : BindableObject
    {
        public static readonly BindableProperty RutaFotoProperty = BindableProperty.Create(
                          "RutaFoto",
                          typeof(string),
                          typeof(CamaraViewModel),
                          default(string));
        public string RutaFoto
        {
            get
            {
                return (string)GetValue(RutaFotoProperty);
            }
            set
            {
                SetValue(RutaFotoProperty, value);
            }
        }

        private MediaFile Foto;

        public Command m_seleccionarFotoComand;

        public Command SeleccionarFotoComand
        {
            get
            {
                return (m_seleccionarFotoComand ?? (m_seleccionarFotoComand = new Command(async () => await SeleccionarFotoAsync())));
            }
        }

        public async Task<bool> SeleccionarFotoAsync()
        {
            int nErrores = 0;
            try
            {
                Foto = await ServicioFoto.Instancia.SeleccionarFotoAsync();
                if (Foto != null)
                {
                    RutaFoto = Foto.Path;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                nErrores++;
            }
            return (nErrores == 0);
        }

        public Command m_tomarFotoComand;

        public Command TomarFotoComand
        {
            get
            {
                return (m_tomarFotoComand ?? (m_tomarFotoComand = new Command(async () => await TomarFotoAsync())));
            }
        }

        public async Task<bool> TomarFotoAsync()
        {
            int nErrores = 0;
            try
            {
                Foto = await ServicioFoto.Instancia.CapturarFotoAsync();
                if (Foto != null)
                {
                    RutaFoto = Foto.Path;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                nErrores++;
            }
            return (nErrores == 0);
        }

        public CamaraViewModel()
        {

        }
    }
}

