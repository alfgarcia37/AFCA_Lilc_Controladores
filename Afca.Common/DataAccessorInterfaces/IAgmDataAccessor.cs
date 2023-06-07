using Afca.Common.DataSets;
using Afca.Common.Models;
using FrameworkForge.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.DataAccessor
{
    public interface IAgmDataAccessor:IDataAccessor
    {
        //este es el sp_GDM_CL_Controles que ahora lanza todos los SP's, por lo que
        // los lanzamientos del los SPs de abajo son innecesarios
        void PrepareAgmEnviosPrincipales();


        AgmEnvioData GetAgmEnviosData();        
        AgmEnvioData GetEnvioMessageData(string ganaderiaID, ArrayList entidadesIncluded);
        int MarkAgmEnviosAsSended(string ganaderiaID, ArrayList entidadesIncluded);
        int MarkAgmEnvioAsNotSended(string ganaderiaID,string entidad,string error);

        void PrepareAgmEnviosRecibos(string remesaID);
        void PrepareAgmEnviosRCS();

        void PrepareAgmEnviosCalificaciones(string codigoGanaderia);
        void PrepareAgmEnviosCenso();
        void PreparaAgmEnviosGenomica();
        AgmEnvioData GetAgmEnviosSecundariosData(string entidad);
        void PreparaAgmEnviosLactacionesHistorico();

        int SaveDowloadedData(string entidad, DateTime fechaDescarga,string message);
        AgmDownloadData GetDownloadedData(string entidad = null, bool includeMessage = false);

        Task<Result> InsertDownloadedData();
    }
}
