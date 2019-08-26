
namespace Afca.Services
{
    using System.Data;
    using Afca.Common.DataAccessor;
    using Afca.Common.ServicesInterfaces;
    using Afca.Common.DataSets;
    using FrameworkForge.Common;
    using FrameworkForge.Common.DataStructures;
    using Afca.Common;
    using Afca.Services.Clases;
    using dO = FrameworkForge.Common.DataOperations;

    public class GenerarService : IGenerarService
    {
        private Result resultGanaderias;
        private Result resultControles;

        private ClsGenerar ObjGenerar = new ClsGenerar();
        private IXmlsDataAccessor iXmlDA;
        private ILecturaControlDataAccessor ilecturasDA;
       public GenerarService()
        {
            iXmlDA = Afca.Common.AfcaDataAccessorProvider.GetXmlDataAccessor();
            ilecturasDA = Afca.Common.AfcaDataAccessorProvider.GetLecturaControlDataAccessor();
        }
        public bool GenerarXMLs(int argControlador)

        {
            //GANADERIAS.... Podría poner directamente las func de más abajo pero prefiero así hasta que haga todo en procdtos.
            resultGanaderias= SetXmlGanaderiasData(argControlador);
            //Recorro GANADERIAS para Generar Tablas
            LecturaControlData lecturaControlData = ilecturasDA.LoadLecturaControlDataByControladorEstado(argControlador, EstadoLectura.Planificado.ToString());
            foreach (DataRow drLectura in lecturaControlData.LecturaControl.Rows)
            {
                //CONTROLES
                resultControles = SetXmlControlesData(drLectura["ID"].ToString());
                if (resultControles.value == "0") resultControles.success = false;
                //TOROS
                //ObjGenerar.SetGanaderiasToXmlGanaderias(argControlador);
                //INSEMINACIONES
                //ObjGenerar.SetGanaderiasToXmlGanaderias(argControlador);
                //REGISTRADAS
                //ObjGenerar.SetGanaderiasToXmlGanaderias(argControlador);
            }




            return true;
        }
        public Result SetXmlGanaderiasData(int argControlador)
        {
            return ObjGenerar.SetXmlGanaderiasData( iXmlDA, argControlador);
        }
        public Result SetXmlControlesData(string argGanaderia)
        {
            return ObjGenerar.SetXmlControlesData(iXmlDA, argGanaderia);
        }

       


    }
}

