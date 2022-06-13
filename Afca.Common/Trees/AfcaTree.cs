using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkForge.Common.Generics;
using FrameworkForge.Common.DataStructures;
using dO = FrameworkForge.Common.DataOperations;
using FrameworkForge.Common;

namespace Afca.Common
{
    public static class Trees
    {
        static ItemMetaData explorerTree;
        public static ItemMetaData ExplorerTree
        {
            get
            {
                if (explorerTree == null)
                {
                    Trees.LoadExplorerTree();
                }
                return Trees.explorerTree;
            }
        }
        static void LoadExplorerTree()
        {
            ItemMetaData root = CreateTreeRootNode();
            root.Children.Add(CreateLecturasTree());

            string agmManagerActive = "false";

            if( !String.IsNullOrEmpty(
                agmManagerActive = ConfigurationProvider.GetConfigurationValue("AgmManagerActive")))
            {
                if(agmManagerActive == "true")
                    root.Children.Add(CreateAgmTree());
            }


            string siluetasManagerActive = "false";

            if (!String.IsNullOrEmpty(
                siluetasManagerActive = ConfigurationProvider.GetConfigurationValue("SiluetasManagerActive")))
            {
                if (siluetasManagerActive == "true")
                    root.Children.Add(CreateSiluetasTree());
            }

            ItemMetaData datosConfiguracionNode = CreateDatosConfiguracionFolderNode();
            root.Children.Add(datosConfiguracionNode);
            datosConfiguracionNode.Children.Add(CreateControladoresTree());

            Trees.explorerTree = root;
        }
        static ItemMetaData CreateTreeRootNode()
        {
            ItemMetaData root = new ItemMetaData("AfcaRoot");
            root.Text = "Afca";
            root.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            return root;
        }

        static ItemMetaData CreateLecturasTree()
        {
            ItemMetaData lecturasFolderNode = CreateLecturasNode();
            return lecturasFolderNode;
        }
        static ItemMetaData CreateLecturasNode()
        {
            ItemMetaData lecturasNode = new ItemMetaData("Lecturas");
            lecturasNode.Text = "Lecturas";
            lecturasNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });

            ItemMetaData enProgresoNode = CreateLecturasEnProgresoTree();
            lecturasNode.Children.Add(enProgresoNode);

            ItemMetaData finalizadasNode = CreateLecturasFinalizadasTree();
            lecturasNode.Children.Add(finalizadasNode);

            return lecturasNode;
        }   

        #region Lecturas en progreso
        static ItemMetaData CreateLecturasEnProgresoTree()
        {
            ItemMetaData enProgresoNode = new ItemMetaData("LecturasEnProgreso");
            enProgresoNode.Text = "En Progreso";
            enProgresoNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            enProgresoNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider", 
                                                            "ShowLecturaControlDataGrid", "Editar lecturas..."
                                                            , ItemMethodType.GridEdit, "IconoPropiedades"));
            enProgresoNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                           "ImportarLecturasControladores", "Importar todos..."
                                                           , ItemMethodType.Logic, "IconoImport"));
            enProgresoNode.ToolbarOptions.AddItemToolbarOption(new ItemToolbarOption(typeof(bool), "Validación automática",
                                                            "Habilita la validación automática al importar", "IconoCalculadora",
                                                            "Afca.UI,ControlsProvider|SetValidacionAutomaticaGlobal",
                                                            bool.Parse(ConfigurationProvider.GetConfigurationValue("ValidacionAutomatica"))));

            ItemMetaData detalleControladoresNode = CreateEnProgresoControladoresNode();
            enProgresoNode.Children.Add(detalleControladoresNode);

            return enProgresoNode;
        }
        static ItemMetaData CreateEnProgresoControladoresNode()
        {
            ItemMetaData controladorNode = new ItemMetaData("ControladorEnProgreso");
            controladorNode.ItemType = ItemType.Leaf;
            controladorNode.Icons.AddRange(new string[] { "IconoControlador" });
            controladorNode.LoadMethod = "IControladorService,LoadControladorProgresoData";
            controladorNode.DataTable = "Controlador";
            controladorNode.SortCriteria = "CodigoControlador ASC";
          
            controladorNode.ItemColumns.AddItemColumn(
                           new ItemColumn("Nombre", "Nombre", false, false));
            
            controladorNode.ItemColumns.AddItemColumn(
                            new ItemColumn("EnCurso", "CUR", false, true) { HorizontalAlign = HorizontalAlign.Centered });
            controladorNode.ItemColumns.AddItemColumn(
                           new ItemColumn("EnTerminal", "TER", false, true) { HorizontalAlign = HorizontalAlign.Centered });            
            controladorNode.ItemColumns.AddItemColumn(
                           new ItemColumn("EnBuzon", "BUZ", false, true) { HorizontalAlign = HorizontalAlign.Centered });            
            controladorNode.ItemColumns.AddItemColumn(
                    new ItemColumn("Warning", "Warning", false, false));
            controladorNode.ItemColumns.AddItemColumn(
                  new ItemColumn("Nota", "Nota", false, true));
            controladorNode.ItemColumns.AddItemColumn(
                         new ItemColumn("CodigoControlador", "Código", true, false));
            controladorNode.ItemColumns["CodigoControlador"].Data.DataType = typeof(int);
            controladorNode.ItemColumns.AddItemColumn(
                            new ItemColumn("EstablosIn", "IN", false, true) );
            controladorNode.ItemColumns.AddItemColumn(
                            new ItemColumn("EstablosOut", "OUT", false, true) );

            ItemColumnList formattedExpressionColumns = new ItemColumnList();
            formattedExpressionColumns.AddItemColumn(controladorNode.ItemColumns["CodigoControlador"]);
            formattedExpressionColumns.AddItemColumn(controladorNode.ItemColumns["Nombre"]);
            controladorNode.SetFormattedTextExpression("{0,2} - {1}", formattedExpressionColumns);

            controladorNode.IconMapper = new ItemIconMapper("Warning");
            controladorNode.IconMapper.Mapping.Add("0", "IconoControlador");
            controladorNode.IconMapper.Mapping.Add("1", "IconoControladorWarning");
            controladorNode.IconMapper.Mapping.Add("2", "IconoControladorRed");
            

            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                      "SetLecturasSiguienteLote", "Siguiente Lote..."
                                      , ItemMethodType.Properties, "IconoNext"));
            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                            "GenerarLecturasControlador", "Generar..."
                                                            , ItemMethodType.GridEdit, "IconoExport"));
            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                            "ImportarLecturasControlador", "Importar..."
                                                            , ItemMethodType.GridEdit, "IconoImport"));
            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                            "ShowLecturaControlDataGrid", "Editar lecturas..."
                                                            , ItemMethodType.GridEdit, "IconoPropiedades"));

            controladorNode.ItemMethodClickIconTrigger = ItemMethodClickTrigger.OneClick;
            controladorNode.ClickIconMethod = controladorNode.Methods
                .Where(m => m.Name == "ShowLecturaControlDataGrid").FirstOrDefault();


            ItemMetaData lecturasControladorNode = CreateLecturasEnProgresoControladorNode();
            controladorNode.Children.Add(lecturasControladorNode);

            return controladorNode;
        }
        static ItemMetaData CreateLecturasEnProgresoControladorNode()
        {
            ItemMetaData lecturaNode = new ItemMetaData("LecturaEnProgreso");
            lecturaNode.ItemType = ItemType.Leaf;
            lecturaNode.Icons.AddRange(new string[] { "IconoLecturaNoLeida" });
            lecturaNode.IconMapper = new ItemIconMapper("Estado");
            lecturaNode.IconMapper.Mapping.Add("0", "IconoLecturaLeida");
            lecturaNode.IconMapper.Mapping.Add("1", "IconoLecturaPlanificada");
            lecturaNode.IconMapper.Mapping.Add("2", "IconoLecturaEnCurso");
            lecturaNode.IconMapper.Mapping.Add("3", "IconoLecturaBorrada");
            lecturaNode.IconMapper.Mapping.Add("4", "IconoLecturaNoLeida");
            lecturaNode.IconMapper.Mapping.Add("5", "IconoLecturaEnTerminal");

            lecturaNode.LoadMethod = "ILecturaControlService,LoadLecturaControlDataByParent";
            lecturaNode.DataTable = "LecturaControl";
            lecturaNode.SortCriteria = "Estado DESC";
            lecturaNode.ShowTreeNodes = false;
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("Establo", "Establo", true, false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                           new ItemColumn("EstabloNombre", "Establo", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                new ItemColumn("Estado", "Estados", false, false));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("EstadoNombre", "Estado", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("FechaPrevista", "Fecha prevista", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                         new ItemColumn("NumLote", "Lote", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("TipoControl", "Tipo", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                      new ItemColumn("VacasControl", "Numero", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                         new ItemColumn("NumControler", "Controlador", false, false));


            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                  "SetLecturaStateToPlanificar", "Planificar..."
                                                  , ItemMethodType.Logic, "IconoLecturaPlanificada"));
            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                  "SetLecturaStateToEnCurso", "En Curso..."
                                                  , ItemMethodType.Properties, "IconoLecturaEnCurso"));
            lecturaNode.Methods.AddItemMethod(new ItemMethod(null,null,null,ItemMethodType.Separator,null));
            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                  "SetLecturasSiguienteLote", "Siguiente Lote..."
                                                  , ItemMethodType.Properties, "IconoNext"));
            lecturaNode.Methods.AddItemMethod(new ItemMethod(null, null, null, ItemMethodType.Separator, null));
            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                            "ShowLecturaPropertiesByItem", "Editar lectura..."
                                            , ItemMethodType.Properties, "IconoPropiedades"));

            //Poder establecer el ancho de la columna

            return lecturaNode;
        }
        #endregion

        #region Lecturas finalizadas
        static ItemMetaData CreateLecturasFinalizadasTree()
        {
            ItemMetaData finalizadasNode = new ItemMetaData("LecturasFinalizadas");
            finalizadasNode.Text = "Finalizadas";
            finalizadasNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            finalizadasNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                            "ShowLecturaControlDataGrid", "Editar lecturas..."
                                                            , ItemMethodType.GridEdit, "IconoPropiedades"));
           
            ItemMetaData detalleControladoresNode = CreateFinalizadasControladoresNode();
            finalizadasNode.Children.Add(detalleControladoresNode);

            return finalizadasNode;
        }
        static ItemMetaData CreateFinalizadasControladoresNode()
        {
            ItemMetaData controladorNode = new ItemMetaData("ControladorFinalizada");
            controladorNode.ItemType = ItemType.Leaf;
            controladorNode.Icons.AddRange(new string[] { "IconoControlador" });
            controladorNode.LoadMethod = "IControladorService,LoadControladorProgresoData";
            controladorNode.DataTable = "Controlador";
            controladorNode.SortCriteria = "CodigoControlador ASC";
           
            controladorNode.ItemColumns.AddItemColumn(
                           new ItemColumn("Nombre", "Nombre", false, false) );
            controladorNode.ItemColumns.AddItemColumn(
                             new ItemColumn("EnCurso", "CUR", false, true) { HorizontalAlign = HorizontalAlign.Centered });
            controladorNode.ItemColumns.AddItemColumn(
                           new ItemColumn("EnTerminal", "TER", false, true) { HorizontalAlign = HorizontalAlign.Centered });
            controladorNode.ItemColumns.AddItemColumn(
                           new ItemColumn("EnBuzon", "BUZ", false, true) { HorizontalAlign = HorizontalAlign.Centered });
            controladorNode.ItemColumns.AddItemColumn(
                    new ItemColumn("Warning", "Warning", false, false));
            controladorNode.ItemColumns.AddItemColumn(
                  new ItemColumn("Nota", "Nota", false, true));
            controladorNode.ItemColumns.AddItemColumn(
                          new ItemColumn("CodigoControlador", "Código", true, false));
            controladorNode.ItemColumns["CodigoControlador"].Data.DataType = typeof(int);

            ItemColumnList formattedExpressionColumns = new ItemColumnList();
            formattedExpressionColumns.AddItemColumn(controladorNode.ItemColumns["CodigoControlador"]);
            formattedExpressionColumns.AddItemColumn(controladorNode.ItemColumns["Nombre"]);
            controladorNode.SetFormattedTextExpression("{0,2} - {1}", formattedExpressionColumns);

            controladorNode.IconMapper = new ItemIconMapper("Warning");
            controladorNode.IconMapper.Mapping.Add("0", "IconoControlador");

            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                      "SetLecturasSiguienteLote", "Siguiente Lote..."
                                      , ItemMethodType.Properties, "IconoNext"));
            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                "GenerarLecturasControlador", "Generar..."
                                                , ItemMethodType.GridEdit, "IconoPropiedades"));
            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                            "ImportarLecturasControlador", "Importar..."
                                                            , ItemMethodType.GridEdit, "IconoImport"));
            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                           "ShowLecturaControlDataGrid", "Editar lecturas..."
                                                           , ItemMethodType.GridEdit, "IconoPropiedades"));


            controladorNode.ItemMethodClickIconTrigger = ItemMethodClickTrigger.OneClick;
            controladorNode.ClickIconMethod = controladorNode.Methods
                .Where(m => m.Name == "ShowLecturaControlDataGrid").FirstOrDefault();

            ItemMetaData lecturasControladorNode = CreateLecturasFinalizadasControladorNode();
            controladorNode.Children.Add(lecturasControladorNode);

            return controladorNode;
        }
        static ItemMetaData CreateLecturasFinalizadasControladorNode()
        {
            ItemMetaData lecturaNode = new ItemMetaData("LecturaFinalizada");
            lecturaNode.ItemType = ItemType.Leaf;
            lecturaNode.Icons.AddRange(new string[] { "IconoLecturaNoLeida" });
            lecturaNode.IconMapper = new ItemIconMapper("Estado");
            lecturaNode.IconMapper.Mapping.Add("0", "IconoLecturaLeida");
            lecturaNode.IconMapper.Mapping.Add("1", "IconoLecturaPlanificada");
            lecturaNode.IconMapper.Mapping.Add("2", "IconoLecturaEnCurso");
            lecturaNode.IconMapper.Mapping.Add("3", "IconoLecturaBorrada");
            lecturaNode.IconMapper.Mapping.Add("4", "IconoLecturaNoLeida");
            lecturaNode.IconMapper.Mapping.Add("5", "IconoLecturaEnTerminal");

            lecturaNode.LoadMethod = "ILecturaControlService,LoadLecturaControlDataByParent";
            lecturaNode.DataTable = "LecturaControl";
            lecturaNode.SortCriteria = "Estado DESC";
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("Establo", "Establo", true, false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                           new ItemColumn("EstabloNombre", "Establo", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                new ItemColumn("Estado", "Estados", false, false));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("EstadoNombre", "Estado", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("FechaPrevista", "Fecha prevista", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                         new ItemColumn("NumLote", "Lote", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("TipoControl", "Tipo", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                      new ItemColumn("VacasControl", "Numero", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                         new ItemColumn("NumControler", "Controlador", false, false));


            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                       "SetLecturaStateToPlanificar", "Planificar..."
                                       , ItemMethodType.Logic, "IconoLecturaPlanificada"));
            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                  "SetLecturaStateToEnCurso", "En Curso..."
                                                  , ItemMethodType.Properties, "IconoLecturaEnCurso"));
            lecturaNode.Methods.AddItemMethod(new ItemMethod(null, null, null, ItemMethodType.Separator, null));
            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                  "SetLecturasSiguienteLote", "Siguiente Lote..."
                                                  , ItemMethodType.Properties, "IconoNext"));
            lecturaNode.Methods.AddItemMethod(new ItemMethod(null, null, null, ItemMethodType.Separator, null));
            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                            "ShowLecturaPropertiesByItem", "Editar lectura..."
                                            , ItemMethodType.Properties, "IconoPropiedades"));

            return lecturaNode;
        }
        #endregion

        #region Lecturas Detalle
        static ItemMetaData CreateLecturasTodasNode()
        {
            ItemMetaData todasNode = new ItemMetaData("Todas");
            todasNode.Text = "Todas";
            todasNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            todasNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                            "ShowLecturaControlDataGrid", "Editar lecturas..."
                                                            , ItemMethodType.GridEdit, "IconoPropiedades"));

            ItemMetaData detalleTodasNode = CreateDetalleLecturasTodasNode();
            todasNode.Children.Add(detalleTodasNode);

            return todasNode;
        }
        static ItemMetaData CreateDetalleLecturasTodasNode()
        {
            ItemMetaData lecturaNode = new ItemMetaData("Lectura");
            lecturaNode.ItemType = ItemType.Leaf;
            lecturaNode.Icons.AddRange(new string[] { "IconoLecturaNoLeida" });
            lecturaNode.IconMapper = new ItemIconMapper("Estado");
            lecturaNode.IconMapper.Mapping.Add("0", "IconoLecturaLeida");
            lecturaNode.IconMapper.Mapping.Add("1", "IconoLecturaPlanificada");
            lecturaNode.IconMapper.Mapping.Add("2", "IconoLecturaEnCurso");
            lecturaNode.IconMapper.Mapping.Add("3", "IconoLecturaBorrada");
            lecturaNode.IconMapper.Mapping.Add("4", "IconoLecturaNoLeida");
            lecturaNode.IconMapper.Mapping.Add("5", "IconoLecturaEnTerminal");

            lecturaNode.LoadMethod = "ILecturaControlService,LoadLecturaControlDataByParent";
            lecturaNode.DataTable = "LecturaControl";
            lecturaNode.SortCriteria = "NumControler ASC,FechaPrevista DESC";
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("Establo", "Establo", true, false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                           new ItemColumn("EstabloNombre", "Establo", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                new ItemColumn("Estado", "Estados", false, false));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("EstadoNombre", "Estado", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("FechaPrevista", "Fecha prevista", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                         new ItemColumn("NumLote", "Lote", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("TipoControl", "Tipo", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                      new ItemColumn("VacasControl", "Numero", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                         new ItemColumn("NumControler", "Controlador", false, true));
            lecturaNode.ItemColumns["NumControler"].DropDownClass = "Afca.UI,Afca.UI.DropDowns.ControladorDropDown";
            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                        "ShowLecturaPropertiesByItem", "Editar lectura..."
                                                        , ItemMethodType.Properties, "IconoPropiedades"));
            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                        "ShowLecturaProperties1", "Planificar..."
                                                        , ItemMethodType.Properties, "IconoPropiedades"));
            //Poder cambiar el icono de color
            //Poder establecer el ancho de la columna

            return lecturaNode;
        }
        static ItemMetaData CreateLecturasPorEstadoNode()
        {
            ItemMetaData porEstadoNode = new ItemMetaData("PorEstado");
            porEstadoNode.Text = "Por Estado";
            porEstadoNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            porEstadoNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                            "ShowLecturaControlDataGrid", "Editar lecturas..."
                                                            , ItemMethodType.GridEdit, "IconoPropiedades"));

            //ItemMetaData detallePorEstadoNode = CreateDetalleLecturasPorEstadoNode();
            //porEstadoNode.Children.Add(detallePorEstadoNode);

            return porEstadoNode;
        }
        static ItemMetaData CreateLecturasPorControladorNode()
        {
            ItemMetaData porContNode = new ItemMetaData("PorControlador");
            porContNode.Text = "Por Controlador";
            porContNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            porContNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                            "ShowLecturaControlDataGrid", "Editar lecturas..."
                                                            , ItemMethodType.GridEdit, "IconoPropiedades"));

            //ItemMetaData detallePorContNode = CreateDetalleLecturasPorControladorNode();
            //porContNode.Children.Add(detallePorContNode);

            return porContNode;
        }

        static ItemMetaData CreateDetalleLecturasNode()
        {
            ItemMetaData lecturaNode = new ItemMetaData("Lectura");
            lecturaNode.ItemType = ItemType.Leaf;
            lecturaNode.Icons.AddRange(new string[] { "IconoLecturaNoLeida" });
            lecturaNode.IconMapper = new ItemIconMapper("Estado");
            lecturaNode.IconMapper.Mapping.Add("0", "IconoLecturaLeida");
            lecturaNode.IconMapper.Mapping.Add("1", "IconoLecturaPlanificada");
            lecturaNode.IconMapper.Mapping.Add("2", "IconoLecturaEnCurso");
            lecturaNode.IconMapper.Mapping.Add("3", "IconoLecturaBorrada");
            lecturaNode.IconMapper.Mapping.Add("4", "IconoLecturaNoLeida");
            lecturaNode.IconMapper.Mapping.Add("5", "IconoLecturaEnTerminal");

            lecturaNode.LoadMethod = "ILecturaControlService,LoadLecturaControlData";
            lecturaNode.DataTable = "LecturaControl";
            lecturaNode.SortCriteria = "NumControler ASC,FechaPrevista DESC";
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("Establo", "Establo", true, false,true));
            lecturaNode.ItemColumns.AddItemColumn(
                           new ItemColumn("EstabloNombre", "Establo", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                new ItemColumn("Estado", "Estados", false, false));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("EstadoNombre", "Estado", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("FechaPrevista", "Fecha prevista", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                         new ItemColumn("NumLote", "Lote", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("TipoControl", "Tipo", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                      new ItemColumn("VacasControl", "Numero", false, true));
            lecturaNode.ItemColumns.AddItemColumn(
                         new ItemColumn("NumControler", "Controlador", false, true));
            lecturaNode.ItemColumns["NumControler"].DropDownClass = "Afca.UI,Afca.UI.DropDowns.ControladorDropDown";
            lecturaNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                        "ShowLecturaProperties", "Editar lectura..."
                                                        , ItemMethodType.Properties, "IconoPropiedades"));
            //Poder cambiar el icono de color
            //Poder establecer el ancho de la columna

            return lecturaNode;
        }
        #endregion

        #region AGM
        static ItemMetaData CreateAgmTree()
        {
            ItemMetaData agmNode = new ItemMetaData("AgmRoot");
            agmNode.Text = "AGM Manager";
            agmNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            //agmNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
            //                                                "ShowLecturaControlDataGrid", "Editar lecturas..."
            //                                                , ItemMethodType.GridEdit, "IconoPropiedades"));

            ItemMetaData agmEnviosNode = CreateAgmEnviosNode();
            agmNode.Children.Add(agmEnviosNode);

            return agmNode;
        }
        static ItemMetaData CreateAgmEnviosNode()
        {
            ItemMetaData agmEnviosNode = new ItemMetaData("AgmEnvios");
            agmEnviosNode.Text = "Envios";
            agmEnviosNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });

            ItemMetaData principalesNode = CreateAgmEnviosPrincipalesTree();
            agmEnviosNode.Children.Add(principalesNode);

            ItemMetaData secundariosNode = CreateAgmEnviosSecundariosTree();
            agmEnviosNode.Children.Add(secundariosNode);

            return agmEnviosNode;
        }
        static ItemMetaData CreateAgmEnviosPrincipalesTree()
        {
            ItemMetaData envPrincipalesNode = new ItemMetaData("AgmEnviosPrincipales");
            envPrincipalesNode.Text = "Principales";
            envPrincipalesNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            envPrincipalesNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                            "PrepareAgmEnviosPrincipales", "Preparar envios..."
                                                            , ItemMethodType.GridEdit, "IconoGenerador"));
            envPrincipalesNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                          "AgmExecuteEnviosPrincipales", "Enviar..."
                                                          , ItemMethodType.GridEdit, "IconoPlay"));
            ItemMetaData detallePrincipalesNode = CreateAgmEnviosPrincipalesNode();
            envPrincipalesNode.Children.Add(detallePrincipalesNode);

            return envPrincipalesNode;
        }
        static ItemMetaData CreateAgmEnviosPrincipalesNode()
        {
            ItemMetaData envPrincipalNode = new ItemMetaData("AgmEnvioPrincipal");
            envPrincipalNode.ItemType = ItemType.Leaf;
            envPrincipalNode.Icons.AddRange(new string[] { "IconoLecturaNoLeida" });

            envPrincipalNode.LoadMethod = "IAgmService,GetAgmEnvioDataByParent";
            envPrincipalNode.DataTable = "Ganaderia";
            envPrincipalNode.SortCriteria = "GanaderiaID ASC";
            envPrincipalNode.ItemColumns.AddItemColumn(
                            new ItemColumn("AsociacionID", "AsociacionID", true, false, false));
            envPrincipalNode.ItemColumns.AddItemColumn(
                           new ItemColumn("GanaderiaID", "Establo", true, true,true));
            envPrincipalNode.ItemColumns.AddItemColumn(
                new ItemColumn("Lecturas", "Lecturas", false, true));
            envPrincipalNode.ItemColumns.AddItemColumn(
                            new ItemColumn("Controles", "Controles", false, true));
            envPrincipalNode.ItemColumns.AddItemColumn(
                           new ItemColumn("Lactaciones", "Lactaciones", false, true));
            envPrincipalNode.ItemColumns.AddItemColumn(
                            new ItemColumn("Inseminaciones", "Inseminaciones", false, true));
            envPrincipalNode.ItemColumns.AddItemColumn(
                           new ItemColumn("Parametros", "Parametros", false, true));
            envPrincipalNode.ItemColumns.AddItemColumn(
                           new ItemColumn("Partos", "Partos", false, true));
            envPrincipalNode.ItemColumns.AddItemColumn(
                          new ItemColumn("Censo", "Censo", false, true));



            return envPrincipalNode;
        }

        static ItemMetaData CreateAgmEnviosSecundariosTree()
        {
            ItemMetaData envSecundariosNode = new ItemMetaData("AgmEnviosSecundarios");
            envSecundariosNode.Text = "Secundarios";
            envSecundariosNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });

            //Pensar en colgar subcarpetas:
            // Censo
            // Recibos
            // Calificaciones
            // RCS
            // Genómica

            ItemMetaData recibosNodeTree = CreateAgmEnviosRecibosTree();
            envSecundariosNode.Children.Add(recibosNodeTree);

            ItemMetaData rcsNodeTree = CreateAgmEnviosRCSTree();
            envSecundariosNode.Children.Add(rcsNodeTree);

            ItemMetaData calificacionesNodeTree = CreateAgmEnvioCalificacionesTree();
            envSecundariosNode.Children.Add(calificacionesNodeTree);

            //ItemMetaData censoNodeTree = CreateAgmEnviosCensoTree();
            //envSecundariosNode.Children.Add(censoNodeTree);

            ItemMetaData genomicaNodeTree = CreateAgmEnviosGenomicaTree();
            envSecundariosNode.Children.Add(genomicaNodeTree);

            return envSecundariosNode;
        }
        static ItemMetaData CreateAgmEnviosRecibosTree()
        {
            ItemMetaData envRecibosNode = new ItemMetaData("AgmEnviosRecibos");
            envRecibosNode.Text = "Recibos";
            envRecibosNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });

            envRecibosNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                           "PrepareAgmEnviosRecibos", "Preparar envios..."
                                                           , ItemMethodType.GridEdit, "IconoGenerador"));
            envRecibosNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                          "AgmExecuteEnviosSecundarios", "Enviar..."
                                                          , ItemMethodType.GridEdit, "IconoPlay"));

            ItemMetaData recibosNode = CreateAgmEnviosRecibosNode();
            envRecibosNode.Children.Add(recibosNode);
            return envRecibosNode;
        }
        static ItemMetaData CreateAgmEnviosRecibosNode()
        {

            ItemMetaData envRecibolNode = new ItemMetaData("AgmEnvioRecibo");
            envRecibolNode.ItemType = ItemType.Leaf;
            envRecibolNode.Icons.AddRange(new string[] { "IconoDocumento" });

            envRecibolNode.LoadMethod = "IAgmService,GetAgmEnvioDataByParent";
            envRecibolNode.DataTable = "Ganaderia";
            envRecibolNode.SortCriteria = "GanaderiaID ASC";
            envRecibolNode.ItemColumns.AddItemColumn(
                            new ItemColumn("AsociacionID", "AsociacionID", true, false, false));
            envRecibolNode.ItemColumns.AddItemColumn(
                           new ItemColumn("GanaderiaID", "Establo", true, true, true));
            envRecibolNode.ItemColumns.AddItemColumn(
                new ItemColumn("Recibos", "Recibos", false, true));
          
            return envRecibolNode;
        }
        static ItemMetaData CreateAgmEnviosRCSTree()
        {
            ItemMetaData envRcsNode = new ItemMetaData("AgmEnviosRCS");
            envRcsNode.Text = "RCS";
            envRcsNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });

            envRcsNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                           "PrepareAgmEnviosRCS", "Preparar envios..."
                                                           , ItemMethodType.GridEdit, "IconoGenerador"));
            envRcsNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                          "AgmExecuteEnviosSecundarios", "Enviar..."
                                                          , ItemMethodType.GridEdit, "IconoPlay"));

            ItemMetaData rcsNode = CreateAgmEnviosRCSNode();
            envRcsNode.Children.Add(rcsNode);
            return envRcsNode;
        }
        static ItemMetaData CreateAgmEnviosRCSNode()
        {

            ItemMetaData envRcsNode = new ItemMetaData("AgmEnvioRCS");
            envRcsNode.ItemType = ItemType.Leaf;
            envRcsNode.Icons.AddRange(new string[] { "IconoMatrazRojo" });

            envRcsNode.LoadMethod = "IAgmService,GetAgmEnvioDataByParent";
            envRcsNode.DataTable = "Ganaderia";
            envRcsNode.SortCriteria = "GanaderiaID ASC";
            envRcsNode.ItemColumns.AddItemColumn(
                            new ItemColumn("AsociacionID", "AsociacionID", true, false, false));
            envRcsNode.ItemColumns.AddItemColumn(
                           new ItemColumn("GanaderiaID", "Establo", true, true, true));
            envRcsNode.ItemColumns.AddItemColumn(
                new ItemColumn("RCS", "RCS", false, true));

            return envRcsNode;
        }
        static ItemMetaData CreateAgmEnvioCalificacionesTree()
        {
            ItemMetaData envCalificacionesNode = new ItemMetaData("AgmEnviosCalificaciones");
            envCalificacionesNode.Text = "Calificaciones";
            envCalificacionesNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });

            envCalificacionesNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                           "PrepareAgmEnviosCalificaciones", "Preparar envios..."
                                                           , ItemMethodType.GridEdit, "IconoGenerador"));
            envCalificacionesNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                          "AgmExecuteEnviosSecundarios", "Enviar..."
                                                          , ItemMethodType.GridEdit, "IconoPlay"));

            ItemMetaData calificacionNode = CreateAgmEnviosCalificacionesNode();
            envCalificacionesNode.Children.Add(calificacionNode);
            return envCalificacionesNode;
        }
        static ItemMetaData CreateAgmEnviosCalificacionesNode()
        {

            ItemMetaData envCalificacionNode = new ItemMetaData("AgmEnvioCalificacion");
            envCalificacionNode.ItemType = ItemType.Leaf;
            envCalificacionNode.Icons.AddRange(new string[] { "IconoCalificacion" });

            envCalificacionNode.LoadMethod = "IAgmService,GetAgmEnvioDataByParent";
            envCalificacionNode.DataTable = "Ganaderia";
            envCalificacionNode.SortCriteria = "GanaderiaID ASC";
            envCalificacionNode.ItemColumns.AddItemColumn(
                            new ItemColumn("AsociacionID", "AsociacionID", true, false, false));
            envCalificacionNode.ItemColumns.AddItemColumn(
                           new ItemColumn("GanaderiaID", "Establo", true, true, true));
            envCalificacionNode.ItemColumns.AddItemColumn(
                new ItemColumn("Calificaciones", "Calificaciones", false, true));

            return envCalificacionNode;
        }
        static ItemMetaData CreateAgmEnviosCensoTree()
        {
            ItemMetaData envCensoNode = new ItemMetaData("AgmEnviosCenso");
            envCensoNode.Text = "Censo";
            envCensoNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });

            envCensoNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                           "PrepareAgmEnviosCenso", "Preparar envios..."
                                                           , ItemMethodType.GridEdit, "IconoGenerador"));
            envCensoNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                          "AgmExecuteEnviosSecundarios", "Enviar..."
                                                          , ItemMethodType.GridEdit, "IconoPlay"));

            ItemMetaData censoNode = CreateAgmEnviosCensoNode();
            envCensoNode.Children.Add(censoNode);
            return envCensoNode;
        }
        static ItemMetaData CreateAgmEnviosCensoNode()
        {

            ItemMetaData envCensoNode = new ItemMetaData("AgmEnvioCenso");
            envCensoNode.ItemType = ItemType.Leaf;
            envCensoNode.Icons.AddRange(new string[] { "IconoCenso" });

            envCensoNode.LoadMethod = "IAgmService,GetAgmEnvioDataByParent";
            envCensoNode.DataTable = "Ganaderia";
            envCensoNode.SortCriteria = "GanaderiaID ASC";
            envCensoNode.ItemColumns.AddItemColumn(
                            new ItemColumn("AsociacionID", "AsociacionID", true, false, false));
            envCensoNode.ItemColumns.AddItemColumn(
                           new ItemColumn("GanaderiaID", "Establo", true, true, true));
            envCensoNode.ItemColumns.AddItemColumn(
                new ItemColumn("Censo", "Censo", false, true));

            return envCensoNode;
        }
        static ItemMetaData CreateAgmEnviosGenomicaTree()
        {
            ItemMetaData envRcsNode = new ItemMetaData("AgmEnviosGenomica");
            envRcsNode.Text = "Genomica";
            envRcsNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });

            envRcsNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                           "PrepareAgmEnviosGenomica", "Preparar envios..."
                                                           , ItemMethodType.GridEdit, "IconoGenerador"));
            envRcsNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                          "AgmExecuteEnviosSecundarios", "Enviar..."
                                                          , ItemMethodType.GridEdit, "IconoPlay"));

            ItemMetaData genomicaNode = CreateAgmEnviosGenomicaNode();
            envRcsNode.Children.Add(genomicaNode);
            return envRcsNode;
        }
        static ItemMetaData CreateAgmEnviosGenomicaNode()
        {

            ItemMetaData envRcsNode = new ItemMetaData("AgmEnvioGenomica");
            envRcsNode.ItemType = ItemType.Leaf;
            envRcsNode.Icons.AddRange(new string[] { "IconoGanado" });

            envRcsNode.LoadMethod = "IAgmService,GetAgmEnvioDataByParent";
            envRcsNode.DataTable = "Ganaderia";
            envRcsNode.SortCriteria = "GanaderiaID ASC";
            envRcsNode.ItemColumns.AddItemColumn(
                            new ItemColumn("AsociacionID", "AsociacionID", true, false, false));
            envRcsNode.ItemColumns.AddItemColumn(
                           new ItemColumn("GanaderiaID", "Establo", true, true, true));
            envRcsNode.ItemColumns.AddItemColumn(
                new ItemColumn("Genomica", "Genomica", false, true));

            return envRcsNode;
        }
        #endregion

        #region Gestor Cartas Nacimiento
        static ItemMetaData CreateSiluetasTree()
        {
            ItemMetaData siluetasFolderNode = CreateSiluetasFolderNode();
            return siluetasFolderNode;
        }
        static ItemMetaData CreateSiluetasFolderNode()
        {
            ItemMetaData siluetasNode = new ItemMetaData("Siluetas");
            siluetasNode.Text = "Nac.Cartas (30 dias)";
            siluetasNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            siluetasNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                          "ShowSiluetasManagerControl", "Exportar cartas..."
                                                          , ItemMethodType.GridEdit, "IconoExport"));

            ItemMetaData pendienteNode = CreateSiluetaNode();
            siluetasNode.Children.Add(pendienteNode);

            return siluetasNode;
        }
        static ItemMetaData CreateSiluetaNode()
        {
            ItemMetaData siluetaNode = new ItemMetaData("Silueta");
            siluetaNode.ItemType = ItemType.Leaf;
            siluetaNode.Icons.AddRange(new string[] { "IconoImprimida" });
            siluetaNode.IconMapper = new ItemIconMapper("Imprimida");
            siluetaNode.IconMapper.Mapping.Add(String.Empty, "IconoNoImprimida");

            siluetaNode.LoadMethod = "INacimientoSiluetasService,LoadNacimientoSiluetasDataLastMonth";
            siluetaNode.DataTable = "NacimientoSiluetas";
            siluetaNode.SortCriteria = "FechaCreado DESC";
            siluetaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("CodigoGanaderia", "Establo", true, false, true));
            siluetaNode.ItemColumns.AddItemColumn(
                           new ItemColumn("CodigoCrotal", "Cod.Crotal", true, true));
            siluetaNode.ItemColumns.AddItemColumn(
                new ItemColumn("FechaNacimiento", "Nacimiento", false, true));
            siluetaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("Controlador", "Controlador", false, true));
            siluetaNode.ItemColumns.AddItemColumn(
                            new ItemColumn("FechaCreado", "Creada", false, true));
            siluetaNode.ItemColumns.AddItemColumn(
                         new ItemColumn("Imprimida", "Imprimida", false, true));

            siluetaNode.ItemColumns["Controlador"].DropDownClass = "Afca.UI,Afca.UI.DropDowns.ControladorDropDown";

            return siluetaNode;
        }
        #endregion


        #region Datos Configuracion
        static ItemMetaData CreateDatosConfiguracionFolderNode()
        {
            ItemMetaData configNode = new ItemMetaData("DataConfiguration");
            configNode.Text = "Datos de configuración";
            configNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            return configNode;
        }
        static ItemMetaData CreateControladoresTree()
        {
            //agc arbol izq
            ItemMetaData controladoresFolderNode = CreateControladoresFolderNode();
                    //agc grid derecha
                    ItemMetaData controladorNode = CreateControladorNode();
                    controladoresFolderNode.Children.Add(controladorNode);

            //agc otro controlador
            ItemMetaData controladoresFolderNode2 = CreateControladoresFolderNode2();
            controladoresFolderNode.Children.Add(controladorNode);

            return controladoresFolderNode;
        }
        static ItemMetaData CreateControladoresFolderNode()
        {
            ItemMetaData configNode = new ItemMetaData("Controladores");
            configNode.Text = "Controlaaadores";
            configNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            return configNode;
        }
        static ItemMetaData CreateControladoresFolderNode2()
        {
            ItemMetaData configNode = new ItemMetaData("Controladooooores");
            configNode.Text = "Controlaaaaaadores";
            configNode.Icons.AddRange(new string[] { "IconoCarpeta", "IconoCarpetaOpen" });
            return configNode;
        }
        static ItemMetaData CreateControladorNode()
        {   //agc. creo que este es el nodo padre
            ItemMetaData controladorNode = new ItemMetaData("Controlador");
            controladorNode.ItemType = ItemType.Leaf;
            controladorNode.Icons.AddRange(new string[] { "IconoControlador" });
            controladorNode.LoadMethod = "IControladorService,LoadControladorData";
            controladorNode.DataTable = "Controlador";
            controladorNode.Text = "CCCCCCCCCC";
            controladorNode.SortCriteria = "Nombre ASC";
            //agc ItemColumns = definicion de grid y de panel inferior derecho????

            controladorNode.ItemColumns.AddItemColumn(
                               new ItemColumn("Id", "Código",  true, true));
                controladorNode.ItemColumns.AddItemColumn(
                               new ItemColumn("Nombre", "Nombre", false, true,true));
                controladorNode.ItemColumns.AddItemColumn(
                                new ItemColumn("Cargo", "Cargoo", false, true));

            //agc definición de botón derecho ????
            controladorNode.Methods.AddItemMethod(new ItemMethod( "Afca.UI,ControlsProvider",
                                                               "ShowControladorProperties", "Editar ficha..."
                                                               , ItemMethodType.Properties, "IconoControlador"));
            controladorNode.Methods.AddItemMethod(new ItemMethod("None", "", "", ItemMethodType.Separator));
            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.Common,AfcaServiceProvider",
                                                               "GetControladorService|DeletControladorDataByItem", "Eliminar ..."
                                                               , ItemMethodType.Delete, "IconoControlador"));

            return controladorNode;
        }
        static ItemMetaData CreateControladorNode2()
        {   //agc. creo que este es el nodo padre
            ItemMetaData controladorNode = new ItemMetaData("Controladorrr");
            controladorNode.ItemType = ItemType.Leaf;
            controladorNode.Icons.AddRange(new string[] { "IconoControlador" });
            controladorNode.LoadMethod = "IControladorService,LoadControladorData";
            controladorNode.DataTable = "Controlador";
            controladorNode.Text = "CCCCCCCCCC";
            controladorNode.SortCriteria = "Nombre ASC";
            //agc ItemColumns = definicion de grid y de panel inferior derecho????

            controladorNode.ItemColumns.AddItemColumn(
                               new ItemColumn("Id", "Código", true, true));
            controladorNode.ItemColumns.AddItemColumn(
                           new ItemColumn("Nombre", "Nombre", false, true, true));
            

            //agc definición de botón derecho ????
            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.UI,ControlsProvider",
                                                               "ShowControladorProperties", "Editar fichaaa..."
                                                               , ItemMethodType.Properties, "IconoControlador"));
            controladorNode.Methods.AddItemMethod(new ItemMethod("None", "", "", ItemMethodType.Separator));
            controladorNode.Methods.AddItemMethod(new ItemMethod("Afca.Common,AfcaServiceProvider",
                                                               "GetControladorService|DeletControladorDataByItem", "Eliminar ..."
                                                               , ItemMethodType.Delete, "IconoControlador"));

            return controladorNode;
        }
        #endregion
    }
}
