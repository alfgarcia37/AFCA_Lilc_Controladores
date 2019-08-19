using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkForge.Common.Generics;
using FrameworkForge.Common.DataStructures;
using dO = FrameworkForge.Common.DataOperations;

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

            ItemMetaData lecturasTodasNode = CreateLecturasTodasNode();
            ItemMetaData lecturasPorEstadoNode = CreateLecturasPorEstadoNode();
            ItemMetaData lecturasPorControladorNode = CreateLecturasPorControladorNode();

            lecturasNode.Children.Add(lecturasTodasNode);
            lecturasNode.Children.Add(lecturasPorEstadoNode);
            lecturasNode.Children.Add(lecturasPorControladorNode);

            return lecturasNode;
        }
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
            lecturaNode.IconMapper.Mapping.Add("3", "IconoLecturaReplanificada");
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
            lecturaNode.IconMapper.Mapping.Add("3", "IconoLecturaReplanificada");
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

    }
}
