<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Capturas.aspx.cs" Inherits="Forms_Capturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="<%= ResolveUrl("~/content/css/plugins/dataTables/datatables.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/iCheck/custom.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/sweetalert/sweetalert.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/chosen/bootstrap-chosen.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/datapicker/datepicker3.css") %>" rel="stylesheet">

    <style>
        .btne {
            margin-top: 27px;
        }

        .upper-case {
            text-transform: uppercase
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <asp:HiddenField runat="server" ID="Hd_TipoUsr"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="Hd_Idarea"></asp:HiddenField>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="row wrapper page-heading animated fadeInRight">
        <div class="col-md-10 col-sm-10 col-xs-12">
            <h2>Solicitudes</h2>
        </div>
        <div class="col-md-2 col-sm-2 col-xs-2 hidden-xs text-right">
            <i class="mdi text-danger   mdi-library-plus"></i>
        </div>
    </div>
    <p class="alert alert-danger animated fadeInRight">
        <label class="col-form-label">Area:</label>
        <asp:Label ID="lblArea" runat="server"></asp:Label>
    </p>

    <div class="ibox" id="ibox-index">
        <div class="ibox-content animated fadeInRight">
            <div class="sk-spinner sk-spinner-double-bounce">
                <div class="sk-double-bounce1"></div>
                <div class="sk-double-bounce2"></div>
            </div>
            <div class="ibox-filters">

                <div class="ibox-content">
                    <div id="Form1" runat="server" class="row">

                        <asp:UpdatePanel runat="server" ID="UP_agregar" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="col-md-2 col-sm-12 col-xs-12">
                                    <asp:DropDownList ID="DD_Jornadas" runat="server" class="chosen-select" AutoPostBack="True"
                                        DataTextField="jornada" AppendDataBoundItems="true" DataValueField="id_jornada"
                                        OnSelectedIndexChanged="ActivaBoton">
                                        <asp:ListItem Value="" Text="--Selecciona Jornada--"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12">
                                    <asp:LinkButton runat="server" ID="btn_agregar" Text="<i class='fa fa-plus-circle'></i> Agregar" CssClass="btn btn-block btn-lg btn-default" OnClick="AbreCaptura" Enabled="false" />
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>



                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <asp:UpdatePanel runat="server" ID="UP_grid" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="GridSol" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered table-hover dataTables-example"
                                DataKeyNames="id_persona" OnRowDataBound="Pintate1"
                                OnPreRender="GridSol_PreRender"
                                OnRowCommand="Soli_Grid_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="No." ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="completo" HeaderText="Nombre" />
                                    <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                    <asp:BoundField DataField="colonia" HeaderText="Colonia" />
                                    <asp:BoundField DataField="celular" HeaderText="Celular" />
                                    <asp:BoundField DataField="email" HeaderText="Correo/Facebook" />
                                    <asp:BoundField DataField="apoyo" HeaderText="Apoyo" />
                                    <asp:BoundField DataField="estatus" HeaderText="Seguimiento" />
                                    <asp:ButtonField Text="<i class='fa fa-edit' data-toggle='tooltip' data-placement='bottom' title='Editar Registro'></i>" CommandName="editar"
                                        ButtonType="Link" ControlStyle-CssClass="btn btn-success" ItemStyle-Width="2%" />
                                    <asp:ButtonField Text="<i class='mdi mdi-arrow-up-bold' data-toggle='tooltip' data-placement='bottom' title='Seguimiento'></i>" CommandName="segui"
                                        ButtonType="Link" ControlStyle-CssClass="btn btn-success" ItemStyle-Width="2%" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="ap_paterno" HeaderText="Paterno" />
                                    <asp:BoundField DataField="ap_materno" HeaderText="Materno" />
                                    <asp:BoundField DataField="id_colonia" HeaderText="id col" />
                                    <asp:BoundField DataField="id_estatus" HeaderText="id estatus" />
                                    <asp:BoundField DataField="descripcion" HeaderText="descripcion" />
                                    <asp:BoundField DataField="fecha_nacimiento" HeaderText="fecha_nacimiento" />
                                    <asp:BoundField DataField="sexo" HeaderText="sexo" />
                                    <asp:BoundField DataField="ApoyoId" HeaderText="ApoyoId" />
                                    <%--<asp:BoundField DataField="tipo_apoyo" HeaderText="tipo_apoyo" />--%>
                                    <asp:BoundField DataField="jornada" HeaderText="jornada" />
                                    <asp:BoundField DataField="id_jornada" HeaderText="id_jornada" />
                                     <asp:BoundField DataField="id_persona" HeaderText="ID" />
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>



        </div>
    </div>

    <%--VENTANA PARA AGREGAR NUEVO REGISTRO--%>

    <div id="Registro_Div" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" aria-hidden="true">
        <asp:UpdatePanel ID="UP_Nuevo" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div role="form" id="form2" runat="server">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lbl_registro" runat="server" Text="<i  class='fa text-info fa-vcard-o'></i> Agregar Persona"></asp:Label></h4>

                                <asp:HiddenField runat="server" ID="ID_Persona_Hidden"></asp:HiddenField>
                            </div>
                            <div class="modal-body">
                                <div class="row">

                                    <div class="col-sm-6 b-r">
                                        <div class="form-group">
                                            <label>Apoyo</label>
                                            <%-- <asp:DropDownList ID="DD_Apoyo" runat="server" Enabled="false" class="form-control chosen-select" DataTextField="Descripcion" required="true"
                                                AppendDataBoundItems="true" DataValueField="ApoyoId">
                                                <asp:ListItem Value="0" Text="---Selecciona Apoyo---"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:ListBox ID="DD_Apoyo_multi" runat="server" SelectionMode="Multiple" data-placeholder="Selecciona..." class="form-control chosen-select" DataValueField="ApoyoId"
                                                DataTextField="Descripcion"></asp:ListBox>
                                        </div>

                                        <div class="form-group">
                                            <label>Nombre</label>
                                            <asp:TextBox ID="txt_Nombre" runat="server" MaxLength="50" CssClass="form-control upper-case" autocomplete="off" required="true" />
                                            <%-- <asp:RequiredFieldValidator runat="server" ID="valFirstName" ControlToValidate="txt_Nombre"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label>Apellido Paterno</label>
                                            <asp:TextBox ID="txt_Apaterno" runat="server" MaxLength="50" CssClass="form-control upper-case" required="true" autocomplete="off" />
                                        </div>
                                        <div class="form-group">
                                            <label>Apellido Materno</label>
                                            <asp:TextBox ID="txt_Amaterno" runat="server" MaxLength="50" CssClass="form-control upper-case" autocomplete="off" />
                                        </div>
                                        <div class="form-group">
                                            <label>Fecha de Nacimiento</label>
                                            <div class="input-group m-b">
                                                <asp:TextBox ID="txt_FechaNacimiento" runat="server" onkeydown="return false;" CssClass="form-control datepicker" required="true" />
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Dirección</label>
                                            <asp:TextBox ID="txt_Direccion" runat="server" MaxLength="150" CssClass="form-control upper-case" autocomplete="off" required="true" />
                                        </div>
                                        <asp:Panel ID="Colonia_Panel" runat="server">
                                             <div  class="form-group" >
                                            <label>Colonia</label>
                                            <asp:DropDownList ID="DD_Colonia" runat="server" class="chosen-select" DataTextField="colonia" required="true"
                                                AppendDataBoundItems="true" DataValueField="id_colonia">
                                                <asp:ListItem Value="0" Text="---Selecciona Colonia---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        </asp:Panel>
                                       
                                        <asp:Panel ID="Municipio_Panel" runat="server">
                                              <div class="form-group">
                                            <label>Municipio</label>
                                            <asp:DropDownList ID="DD_Municipio" runat="server" class="chosen-select" DataTextField="colonia" required="true"
                                                AppendDataBoundItems="true" DataValueField="id_colonia">
                                                <asp:ListItem Value="0" Text="---Selecciona Municipio---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        </asp:Panel>
                                       

                                        <div class="form-group">
                                            <label>Celular</label>
                                            <asp:TextBox ID="txt_Celular" runat="server" MaxLength="10" CssClass="form-control number" autocomplete="off" />
                                        </div>


                                        <div class="form-group">
                                            <label>Correo o Facebook</label>
                                            <asp:TextBox ID="txt_email" runat="server" MaxLength="50" CssClass="form-control upper-case " autocomplete="off" />
                                        </div>

                                        <div class="form-group">
                                            <label>Sexo</label>
                                            <asp:DropDownList ID="DD_Sexo" runat="server" class="chosen-select" DataTextField="sexo" required="true"
                                                AppendDataBoundItems="true" DataValueField="id_sexo">
                                                <asp:ListItem Value="0" Text="---Selecciona Género---" Selected="True">
                                                </asp:ListItem>
                                                <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                                <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <%-- <div class="form-group">
                                    <label>Jornada</label>
                                    <asp:TextBox ID="txt_Jornada" runat="server"  MaxLength="200" CssClass="form-control upper-case" autocomplete="off" Enabled="false" />
                                </div>--%>
                                        <%-- <div class="form-group">
                                    <label>Tipo Apoyo</label>
                                    <asp:DropDownList ID="DD_TipoApoyo" runat="server" class="chosen-select" DataTextField="Nombre" disabled="disabled"
                                        AppendDataBoundItems="true" DataValueField="Id" OnSelectedIndexChanged="LlenaApoyo">
                                        <asp:ListItem Value="0" Text="---Selecciona Tipo de apoyo---"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>--%>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btn_Guardar" runat="server" Text="Guardar" CssClass="btn btn-w-m btn-primary" OnClick="Nuevo_Registro" />
                                <asp:Button ID="btn_Borrar" runat="server" Text="Borrar" CssClass="btn btn-w-m btn-danger" OnClientClick="return borrar(this);" OnClick="Borra_Registro"
                                    Visible="false" Enabled="false" />
                                <%-- <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn btn-w-m btn-default" formnovalidate="formnovalidate" value="Save"  OnClick="Cancelar" />--%>
                                <button type="button" class="btn btn-w-m btn-default" data-dismiss="modal" formnovalidate="formnovalidate" value="Save">Cancelar</button>
                            </div>
                        </div>


                    </div>
                </div>
            </ContentTemplate>
            <%--   <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DD_TipoApoyo" />
        </Triggers>--%>
        </asp:UpdatePanel>
    </div>




    <%--VENTANA PARA SEGUIMIENTO--%>

    <div id="Seguimiento_Div" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label1" runat="server" Text="<i  class='mdi text-danger mdi-checkbox-marked'></i> Agregar Seguimiento"></asp:Label></h4>
                            <asp:HiddenField runat="server" ID="ID_Persona_Hidden_segui"></asp:HiddenField>

                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <form role="form">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Nombre:</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="lblNombre2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Apoyo:</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="text_asunto2" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control diff-textarea" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="hr-line-dashed"></div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Seguimiento:</label>
                                        <div class="col-sm-10">
                                            <asp:RadioButtonList ID="RB_estatus" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="&nbsp; Positivo &nbsp;" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp; Negativo " Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Observación:</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control diff-textarea"></asp:TextBox>
                                        </div>
                                    </div>


                                </form>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btn_guardar_segui" runat="server" Text="Guardar" CssClass="btn btn-w-m btn-success" OnClick="UPSeguimiento" />
                            <button type="button" class="btn btn-w-m btn-default" data-dismiss="modal" formnovalidate="formnovalidate" value="Save">Cancelar</button>
                            <%--<asp:Button ID="btn_cancelar_segui" runat="server" Text="Cancelar" CssClass="btn btn-w-m btn-default"   OnClick="Cancelar" />--%>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>





     <asp:UpdateProgress ID="UpdateProgress1" runat="server"  Visible="true" DisplayAfter="1">
        <ProgressTemplate>
             <div id="cargando2" class="wrapper-loader fade in" style="display: inline;" >
            <div class="loader">
                <div class="loader__figure"></div>
                <p class="loader__label">Cargando...</p>
            </div>
        </div>
        </ProgressTemplate>
    </asp:UpdateProgress>



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScript" runat="Server">
    <script src="<%= ResolveUrl("~/content/js/plugins/dataTables/datatables.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/dataTables/dataTables.bootstrap4.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/iCheck/icheck.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/sweetalert/sweetalert.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/chosen/chosen.jquery.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/datapicker/bootstrap-datepicker.js") %>"></script>


    <!-- Jquery Validate -->
    <script src="<%= ResolveUrl("~/content/js/plugins/validate/jquery.validate.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/validate/validate_ES.js") %>"></script>


    <script type="text/javascript">


        //Disabled copiar texto
        function killCopy(e) {
            return false
        }
        function reEnable() {
            return true
        }
        document.onselectstart = new Function("return false")
        if (window.sidebar) {
            document.onmousedown = killCopy
            document.onclick = reEnable
        }


        $(document).ready(function () {

            ejecuta_javascript();
            fn_init();

        });


        function fn_init() {



            $("#form").validate({

                rules: {
                    '<%=txt_Nombre.UniqueID %>': {
                        required: true,
                        minlength: 3

                    },
                    '<%=DD_Sexo.UniqueID %>': {
                        required: true
                    },
                    '<%=txt_Celular.UniqueID %>': {
                        minlength: 10,
                        number: true
                    }
                }, messages: {}
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(onEachRequest);
        }

        function onEachRequest(sender, args) {
            if ($("#form").valid() == false) {
                args.set_cancel(true);
            }
        }






        function ejecuta_javascript() {
            //hideModal();
            //swal("Selecciona un empleado", "", "error");
            $(".chosen-select").chosen({ width: "100%" });


            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });

            $(".datepicker").datepicker({
                keyboardNavigation: false,
                forceParse: false,
                autoclose: true,
                format: "dd/mm/yyyy"
            });

            $('.dataTables-example').DataTable({
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No se encontraron registros",
                    "info": "Página _PAGE_ de _PAGES_",
                    "infoEmpty": "No se encontraron registros",
                    "infoFiltered": "(Filtrado de _MAX_ total de registros)",
                    "sSearch": "Buscar:",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                },
                stateSave: true,
                destroy: true,
                pageLength: 10,
                responsive: true,

            });
        }

        function despliega_aviso(tipo, titulo, mensaje, txt_btn_no, txt_btn_si, txt_msj_no, txt_msj_si) {
            if (tipo == 'warning') {
                swal({ title: titulo, text: mensaje, type: tipo, showCancelButton: false });
            }
            else if (tipo == 'pregunta') {
                swal({ title: titulo, text: mensaje, type: "warning", showCancelButton: "true", confirmButtonColor: "#DD6B55", confirmButtonText: txt_btn_si, cancelButtonText: txt_btn_no, closeOnConfirm: true, closeOnCancel: true },
                    function (isConfirm) {
                        if (isConfirm) { despliega_aviso("normal", txt_msj_si, ""); return true; }
                        else { despliega_aviso("normal", txt_msj_no, ""); return false; }
                    });
            }
            else if (tipo == 'success') { swal({ title: titulo, text: mensaje, type: tipo }); }
            else { swal({ title: titulo, text: mensaje }); }
        }

        function hideModal() {
            $('.modal-backdrop').remove(); //Hide the backdrop
        }



        var obj = { status: false, ele: null };
        function borrar(me) {
            if (obj.status) return true;
            swal({
                title: "¿Quieres Eliminar Registro?",
                text: "",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "SI",
                cancelButtonText: "NO",
                closeOnConfirm: false,
                closeOnCancel: true,
                showLoaderOnConfirm: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        obj.status = true;
                        obj.ele = me;
                        obj.ele.click();
                        //swal("Deleted!", "Your imaginary file has been deleted.", "success");
                    } else {
                        //swal("Cancelled", "Your imaginary file is safe :)", "error");
                    }
                });

            return false;
        }





    </script>
</asp:Content>
