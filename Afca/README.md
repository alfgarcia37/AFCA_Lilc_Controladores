

ControlsProvider.cs:
public static DataSet ImportarLecturasControladorByID(int codigoControlador,.....)

    iSvrImportar.PasarDownToXml(codigoControlador);
        "SP_PasarDownToXML"

    resultado1 = iSvrImportar.ImportarXMLs(codigoControlador);
	//Recorro GANADERIAS para Generar Tablas
    XmlData xmldatosData = iXmlDA.LoadXMLDatosDataByControlador(argControlador);
		[sp_XMLDATOS_selectByControlador] ->obtengo ganaderias descargadas

		foreach (DataRow drGanaderias in xmldatosData.XMLDATOS.Rows)
        {
            //GANADERIAS.... 
            resultGanaderias = GetXmlGanaderiasData(drGanaderias["GanaderiaCodigo"].ToString());
            ////CONTROLES
            resultControles = GetXmlControlesData(drGanaderias["GanaderiaCodigo"].ToString(), ..... 
                        "sp_ControlesFromXMLByEstablo"
            ////INSEMINACIONES
            resultInseminaciones = GetXmlInseminacionesData(drGanaderias["GanaderiaCodigo"].ToString(), argControlador);
                        sp_InseminacionesFromXMLByEstablo
            ////NACIMIENTOS....
            resultNacimientos = GetXmlNacimientosData(drGanaderias["GanaderiaCodigo"].ToString(), argControlador);
                        sp_NacimientosFromXMLByEstablo
            ////ENCUESTAS....
            resultEncuestas = GetXmlEncuestasData(drGanaderias["GanaderiaCodigo"].ToString(), argControlador);
                        sp_EncuestasFromXMLByEstablo
            ////RCS....
            resultRcs = GetXmlRcsData( argControlador);
                sp_RcsFromXMLByControlador

            if (resultRcs.value == "0") { resultRcs.success = false; break; }
        }

        -> Si todo va bien: 
            resultado1 = iSvrImportar.GetXmlActualizacionesGlobalData(codigoControlador);
                "sp_ActualizacionesglobalesFromXMLBycontrolador" -> paso a CLM2_AUDIT

        -> Si marco Actualizacion Automatica: 
             bool.Parse(ConfigurationProvider.GetConfigurationValue("ValidacionAutomatica"));
             resultado1 = iSvrImportar.GetLaunchAutoERP();
                ->"sp_LaunchAutoERP" :
                    ... 
                    exec reggen..SP_REG_SELECCION_Nacimientos 'ErpUser','55555',0,0 
                    ....